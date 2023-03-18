import { Component, OnInit, TemplateRef, ViewChild } from '@angular/core';
import { ThirdNewsService } from 'src/app/share/admin/services/third-news.service';
import { ActivatedRoute, Router } from '@angular/router';
import { ConfirmDialogComponent } from 'src/app/components/confirm-dialog/confirm-dialog.component';
import { ThirdNewsItemDto } from 'src/app/share/admin/models/third-news/third-news-item-dto.model';
import { ThirdNewsFilterDto } from 'src/app/share/admin/models/third-news/third-news-filter-dto.model';
import { MatPaginator, PageEvent } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';
import { MatSnackBar } from '@angular/material/snack-bar';
import { MatDialog, MatDialogRef } from '@angular/material/dialog';
import { FormGroup } from '@angular/forms';
import { TechType } from 'src/app/share/admin/models/enum/tech-type.model';
import { NewsType } from 'src/app/share/admin/models/enum/news-type.model';
import { SelectionModel } from '@angular/cdk/collections';

@Component({
  selector: 'app-index',
  templateUrl: './index.component.html',
  styleUrls: ['./index.component.css']
})
export class IndexComponent implements OnInit {
  NewsType = NewsType;
  TechType = TechType
  @ViewChild(MatPaginator, { static: true }) paginator!: MatPaginator;
  isLoading = true;
  total = 0;
  data: ThirdNewsItemDto[] = [];
  columns: string[] = ['select', 'title', 'authorName', 'newsType', 'techType', 'actions'];
  dataSource!: MatTableDataSource<ThirdNewsItemDto>;
  dialogRef!: MatDialogRef<{}, any>;
  @ViewChild('myDialog', { static: true })
  myTmpl!: TemplateRef<{}>;

  mydialogForm!: FormGroup;
  filter: ThirdNewsFilterDto;
  selection = new SelectionModel<ThirdNewsItemDto>(true, []);

  pageSizeOption = [12, 20, 50];
  constructor(
    private service: ThirdNewsService,
    private snb: MatSnackBar,
    private dialog: MatDialog,
    private route: ActivatedRoute,
    private router: Router,
  ) {

    this.filter = {
      pageIndex: 1,
      pageSize: 12
    };
  }

  ngOnInit(): void {
    this.getList();
  }

  getList(event?: PageEvent): void {
    if (event) {
      this.filter.pageIndex = event.pageIndex + 1;
      this.filter.pageSize = event.pageSize;
    }
    this.service.filter(this.filter)
      .subscribe(res => {
        if (res.data) {
          this.data = res.data;
          this.total = res.count;
          this.dataSource = new MatTableDataSource<ThirdNewsItemDto>(this.data);
        }
        this.isLoading = false;
      });
  }

  isAllSelected() {
    const numSelected = this.selection.selected.length;
    const numRows = this.dataSource.data.length;
    return numSelected === numRows;
  }

  toggleAllRows() {
    if (this.isAllSelected()) {
      this.selection.clear();
      return;
    }

    this.selection.select(...this.dataSource.data);
  }
  checkboxLabel(row?: ThirdNewsItemDto): string {
    if (!row) {
      return `${this.isAllSelected() ? 'deselect' : 'select'} all`;
    }
    return `${this.selection.isSelected(row) ? 'deselect' : 'select'} row ${row.id}`;
  }

  deleteConfirm(item: ThirdNewsItemDto): void {
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
  delete(item: ThirdNewsItemDto): void {
    this.service.delete(item.id)
      .subscribe(res => {
        if (res) {
          this.data = this.data.filter(_ => _.id !== item.id);
          this.dataSource.data = this.data;
          this.snb.open('删除成功');
        } else {
          this.snb.open('删除失败');
        }
      });
  }

  /*
  * 弹窗示例
  openMyDialog(): void {
    this.dialogRef = this.dialog.open(myTmpl, {
      hasBackdrop: true,
      minWidth: 300,
      disableClose: false,
      data: {
      }
    });
    this.dialogRef.afterClosed().subscribe(res => {
      if (res) {
        
      }
    });
  }
  }*/

  /**
   * 编辑
   */
  edit(id: string): void {
    this.router.navigate(['../edit/' + id], { relativeTo: this.route });
  }

}
