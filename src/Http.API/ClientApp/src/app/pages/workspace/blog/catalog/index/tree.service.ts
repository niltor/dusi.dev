import { BehaviorSubject } from "rxjs";
import { Injectable } from '@angular/core';

export interface TreeNode<T> {
    id: string;
    name: string;
    children?: T[] | null;
}

export interface TreeItemFlatNode {
    id: string;
    name: string;
    level: number;
    expandable: boolean;
}

@Injectable()
export class ChecklistDatabase<T extends TreeNode<T>> {
    dataChange = new BehaviorSubject<T[]>([]);

    get data(): T[] {
        return this.dataChange.value;
    }
    constructor() {
    }

    initialize(source: T[]) {
        const data = this.buildFileTree(source);
        this.dataChange.next(data);
    }

    buildFileTree(source: T[] | null): T[] {
        let res: T[] = [];
        source?.forEach(f => {
            const item = {
                id: f.id,
                name: f.name!,
                children: this.buildFileTree(f.children!)
            } as T;
            res.push(item);
        });
        return res;
    }
    insertItem(parent: T, name: string) {
        if (parent.children) {
            parent.children.push({ name: name } as T);
            this.dataChange.next(this.data);
        }
    }

    deleteItem(parent: T, id: string) {
        if (parent.children) {
            console.log(parent.children);

            parent.children = parent.children.filter(v => v.id != id);
            this.dataChange.next(this.data);
        }
    }

    updateItem(node: T, name: string, id: string) {
        node.name = name;
        node.id = id;
        this.dataChange.next(this.data);
    }
}
