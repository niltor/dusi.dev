import { BehaviorSubject } from "rxjs";
import { Injectable } from '@angular/core';

export interface TreeNode<T> {
    id: string;
    name?: string | null;
    children?: T[] | null;
    level: number;
}

export interface TreeItemNode {
    children: TreeItemNode[];
    item: string;
}

export interface TreeItemFlatNode {
    item: string;
    level: number;
    expandable: boolean;
}

@Injectable()
export class ChecklistDatabase {
    dataChange = new BehaviorSubject<TreeItemNode[]>([]);

    get data(): TreeItemNode[] {
        return this.dataChange.value;
    }
    constructor() {
    }

    initialize<T extends TreeNode<T>>(source: T[]) {
        const data = this.buildFileTree(source);
        this.dataChange.next(data);
    }

    buildFileTree<T extends TreeNode<T>>(source: T[] | null): TreeItemNode[] {
        let res: TreeItemNode[] = [];
        source?.forEach(f => {
            const item: TreeItemNode = {
                item: f.name!,
                children: this.buildFileTree(f.children!)
            }
            res.push(item);
        });
        return res;
    }

    insertItem(parent: TreeItemNode, name: string) {
        if (parent.children) {
            parent.children.push({ item: name } as TreeItemNode);
            this.dataChange.next(this.data);
        }
    }

    updateItem(node: TreeItemNode, name: string) {
        node.item = name;
        this.dataChange.next(this.data);
    }
}
