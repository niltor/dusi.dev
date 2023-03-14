import { BehaviorSubject } from "rxjs";
import { Injectable } from '@angular/core';

export interface TreeNode<T> {
    id: string;
    name: string;
    children?: T[] | null;
}

export interface TreeItemFlatNode {
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

    updateItem(node: T, name: string) {
        node.name = name;
        this.dataChange.next(this.data);
    }
}
