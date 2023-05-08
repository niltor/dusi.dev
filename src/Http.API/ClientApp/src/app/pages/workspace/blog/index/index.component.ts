import { Component, OnInit, ViewChild } from '@angular/core';
import { BlogService } from 'src/app/share/client/services/blog.service';
import { ActivatedRoute, Router } from '@angular/router';
import { ConfirmDialogComponent } from 'src/app/components/confirm-dialog/confirm-dialog.component';
import { BlogItemDto } from 'src/app/share/client/models/blog/blog-item-dto.model';
import { BlogFilterDto } from 'src/app/share/client/models/blog/blog-filter-dto.model';
import { MatPaginator, PageEvent } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';
import { MatSnackBar } from '@angular/material/snack-bar';
import { MatDialog } from '@angular/material/dialog';
import { LanguageType } from 'src/app/share/client/models/enum/language-type.model';

@Component({
  selector: 'app-index',
  templateUrl: './index.component.html',
  styleUrls: ['./index.component.css']
})
export class IndexComponent implements OnInit {
  @ViewChild(MatPaginator, { static: true }) paginator!: MatPaginator;
  LanguageType = LanguageType;
  isLoading = true;
  total = 0;
  data: BlogItemDto[] = [];
  columns: string[] = ['title', 'languageType', 'authors', 'createdTime', 'actions'];
  dataSource!: MatTableDataSource<BlogItemDto>;
  filter: BlogFilterDto;
  pageSizeOption = [12, 20, 50];
  searchKey: string | null = null;
  constructor(
    private service: BlogService,
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
    this.service.getMyBlogs(this.filter)
      .subscribe({
        next: (res) => {
          if (res) {
            if (res.data) {
              this.data = res.data;
              this.total = res.count;
              this.dataSource = new MatTableDataSource<BlogItemDto>(this.data);
            }
          } else {
            this.snb.open('');
          }
        },
        error: (error) => {
          this.snb.open(error.detail);
        },
        complete: () => {
          this.isLoading = false;
        }
      });
  }

  deleteConfirm(item: BlogItemDto): void {
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
  delete(item: BlogItemDto): void {
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

  /**
   * 编辑
   */
  edit(id: string): void {
    this.router.navigate(['../edit/' + id], { relativeTo: this.route });
  }

}
