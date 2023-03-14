import { Component, OnInit, ViewChild } from '@angular/core';
import { CatalogService } from 'src/app/share/client/services/catalog.service';
import { ActivatedRoute, Router } from '@angular/router';
import { ConfirmDialogComponent } from 'src/app/components/confirm-dialog/confirm-dialog.component';
import { CatalogItemDto } from 'src/app/share/client/models/catalog/catalog-item-dto.model';
import { MatPaginator, PageEvent } from '@angular/material/paginator';
import { MatSnackBar } from '@angular/material/snack-bar';
import { MatDialog } from '@angular/material/dialog';
import { FlatTreeControl } from '@angular/cdk/tree';
import { MatTreeFlattener, MatTreeFlatDataSource } from '@angular/material/tree';
import { ChecklistDatabase, TreeItemFlatNode } from './tree.service';
import { Catalog } from 'src/app/share/client/models/catalog/catalog.model';

@Component({
  selector: 'app-index',
  templateUrl: './index.component.html',
  styleUrls: ['./index.component.css']
})
export class IndexComponent implements OnInit {
  @ViewChild(MatPaginator, { static: true }) paginator!: MatPaginator;
  isLoading = true;
  total = 0;
  data: Catalog[] = [];

  // 字典映射
  flatNodeMap = new Map<TreeItemFlatNode, Catalog>();
  nestedNodeMap = new Map<Catalog, TreeItemFlatNode>();

  treeControl: FlatTreeControl<TreeItemFlatNode>;
  treeFlattener: MatTreeFlattener<Catalog, TreeItemFlatNode>;

  dataSource!: MatTreeFlatDataSource<Catalog, TreeItemFlatNode>;

  constructor(
    private service: CatalogService,
    private _database: ChecklistDatabase<Catalog>,
    private snb: MatSnackBar,
    private dialog: MatDialog,
    private route: ActivatedRoute,
    private router: Router,
  ) {
    this.treeFlattener = new MatTreeFlattener(
      this.transformer,
      this.getLevel,
      this.isExpandable,
      this.getChildren,
    );
    this.treeControl = new FlatTreeControl<TreeItemFlatNode>(this.getLevel, this.isExpandable);
  }

  ngOnInit(): void {
    this.getList();
  }

  getList(): void {
    this.service.getTree()
      .subscribe(res => {
        if (res) {
          this._database.initialize(res);
          this.dataSource = new MatTreeFlatDataSource(this.treeControl, this.treeFlattener);
          this._database.dataChange.subscribe(data => {
            this.dataSource.data = data;
          });

        }
        this.isLoading = false;
      });
  }

  getLevel = (node: TreeItemFlatNode) => node.level;

  isExpandable = (node: TreeItemFlatNode) => node.expandable;

  getChildren = (node: Catalog): Catalog[] => node.children!;

  hasChild = (_: number, _nodeData: TreeItemFlatNode) => _nodeData.expandable;

  hasNoContent = (_: number, _nodeData: TreeItemFlatNode) => _nodeData.name === '';

  /**
   * Transformer to convert nested node to flat node. Record the nodes in maps for later use.
   */
  transformer = (node: Catalog, level: number) => {
    const existingNode = this.nestedNodeMap.get(node);
    const flatNode =
      existingNode && existingNode.name === node.name ? existingNode : {} as TreeItemFlatNode;
    flatNode.name = node.name!;
    flatNode.level = level;
    flatNode.expandable = !!node.children?.length;
    this.flatNodeMap.set(flatNode, node);
    this.nestedNodeMap.set(node, flatNode);
    return flatNode;
  };


  /* Get the parent node of a node */
  getParentNode(node: TreeItemFlatNode): TreeItemFlatNode | null {
    const currentLevel = this.getLevel(node);

    if (currentLevel < 1) {
      return null;
    }

    const startIndex = this.treeControl.dataNodes.indexOf(node) - 1;

    for (let i = startIndex; i >= 0; i--) {
      const currentNode = this.treeControl.dataNodes[i];

      if (this.getLevel(currentNode) < currentLevel) {
        return currentNode;
      }
    }
    return null;
  }

  /** Select the category so we can insert the new item. */
  addNewItem(node: TreeItemFlatNode) {
    const parentNode = this.flatNodeMap.get(node);
    this._database.insertItem(parentNode!, '');
    this.treeControl.expand(node);
  }

  /** Save the node to database */
  saveNode(node: TreeItemFlatNode, itemValue: string) {
    const nestedNode = this.flatNodeMap.get(node);
    this._database.updateItem(nestedNode!, itemValue);
  }

  deleteConfirm(item: Catalog): void {
    const ref = this.dialog.open(ConfirmDialogComponent, {
      hasBackdrop: true,
      disableClose: false,
      data: {
        title: '删除',
        content: '是否确定删除?'
      }
    });

    ref.afterClosed().subscribe(res => {
      if (res) {
        this.delete(item);
      }
    });
  }
  delete(item: Catalog): void {
    this.service.delete(item.id)
      .subscribe(res => {
        if (res) {
          this.data = this.data.filter(_ => _.id !== item.id);
          // this.dataSource.data = this.data;
          this.snb.open('删除成功');
        } else {
          this.snb.open('删除失败');
        }
      });
  }

  /**
   * 编辑
   */
  edit(id: string): void {
    this.router.navigate(['../edit/' + id], { relativeTo: this.route });
  }
}
