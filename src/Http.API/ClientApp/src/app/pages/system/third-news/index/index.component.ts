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
import { ThirdNewsBatchUpdateDto } from "../../../../share/admin/models/third-news/third-news-batch-update-dto.model";
import { NewsStatus } from 'src/app/share/admin/models/enum/news-status.model';

@Component({
  selector: 'app-index',
  templateUrl: './index.component.html',
  styleUrls: ['./index.component.css']
})
export class IndexComponent implements OnInit {
  NewsType = NewsType;
  TechType = TechType;
  NewsStatus = NewsStatus;
  @ViewChild(MatPaginator, { static: true }) paginator!: MatPaginator;
  isLoading = true;
  total = 0;
  data: ThirdNewsItemDto[] = [];
  previewNews: ThirdNewsItemDto | null = null;
  columns: string[] = ['select', 'title', 'datetime','newsType', 'techType', 'actions'];
  dataSource!: MatTableDataSource<ThirdNewsItemDto>;
  dialogRef!: MatDialogRef<{}, any>;
  @ViewChild('previewDialog', { static: true })
  previewTmpl!: TemplateRef<{}>;

  mydialogForm!: FormGroup;
  filter: ThirdNewsFilterDto;
  selection = new SelectionModel<ThirdNewsItemDto>(true, []);
  pageSizeOption = [20, 50];

  constructor(
    private service: ThirdNewsService,
    private snb: MatSnackBar,
    private dialog: MatDialog,
    private route: ActivatedRoute,
    private router: Router,
  ) {

    this.filter = {
      pageIndex: 1,
      pageSize: this.pageSizeOption[0]
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

  preview(data: ThirdNewsItemDto): void {
    this.previewNews = data;
    this.dialogRef = this.dialog.open(this.previewTmpl, {
      maxWidth: 800
    });
  }

  setTechType(value: any): void {
    if (this.previewNews) {
      let data: ThirdNewsBatchUpdateDto = {
        ids: [this.previewNews.id],
        techType: value,
      };
      this.service.batchUpdate(data)
        .subscribe({
          next: (res) => {
            if (res) {
              this.getList();
              this.dialogRef.close();
              this.snb.open('设置成功');
            } else {
              this.snb.open('');
            }
          },
          error: (error) => {
            this.snb.open(error.detail);
          }
        })
    } else {
      this.snb.open('未选择元素');
    }
  }

  /**
   * 批量操作
   */
  batchUpdate(type: string, value: any): void {
    if (this.selection.selected.length > 0) {
      const ids = this.selection.selected.map(v => v.id);
      let data: ThirdNewsBatchUpdateDto = {
        ids: ids,
      }
      switch (type) {
        case 'delete':
          data.isDelete = true;
          break;
        case 'newsStatus':
          data.newsStatus = value;
          break;
        case 'newsType':
          data.newsType = value;
          break;
        case 'techType':
          data.techType = value;
          break;
        default:
          break;
      }
      this.service.batchUpdate(data)
        .subscribe({
          next: (res) => {
            if (res) {
              this.getList();
              this.snb.open('操作成功');
            } else {
              this.snb.open('操作失败');
            }
          },
          error: (error) => {
            this.snb.open(error.detail);
          }
        })

    } else {
      this.snb.open('未选择任何元素');
    }
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
          if (this.dialogRef) {
            this.dialogRef.close();
          }
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
