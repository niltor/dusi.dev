import { Component, OnInit, TemplateRef, ViewChild, ViewEncapsulation } from '@angular/core';
import { TagsService } from 'src/app/share/client/services/tags.service';
import { ActivatedRoute, Router } from '@angular/router';
import { ConfirmDialogComponent } from 'src/app/components/confirm-dialog/confirm-dialog.component';
import { TagsItemDto } from 'src/app/share/client/models/tags/tags-item-dto.model';
import { TagsFilterDto } from 'src/app/share/client/models/tags/tags-filter-dto.model';
import { MatPaginator, PageEvent } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';
import { MatSnackBar } from '@angular/material/snack-bar';
import { MatDialog, MatDialogRef } from '@angular/material/dialog';
import { TagsAddDto } from 'src/app/share/client/models/tags/tags-add-dto.model';
import { MatChipInputEvent, MatChipEditedEvent } from '@angular/material/chips';
import { COMMA, ENTER } from '@angular/cdk/keycodes'
import { CommonColors } from 'src/app/share/const';

@Component({
  selector: 'app-index',
  templateUrl: './index.component.html',
  styleUrls: ['./index.component.css'],
  encapsulation: ViewEncapsulation.None,
})
export class IndexComponent implements OnInit {
  @ViewChild(MatPaginator, { static: true }) paginator!: MatPaginator;
  @ViewChild('addTagsDialog', { static: true }) addTagTmp!: TemplateRef<any>;
  dialogRef: MatDialogRef<any, any> | null = null;
  addOnBlur = true;
  colors = CommonColors;
  readonly separatorKeysCodes = [ENTER, COMMA] as const;
  isLoading = true;
  isProcessing = false;
  total = 0;
  data: TagsItemDto[] = [];
  newTags: TagsAddDto[] = [];

  columns: string[] = ['name', 'color', 'createdTime', 'actions'];
  dataSource!: MatTableDataSource<TagsItemDto>;
  filter: TagsFilterDto;
  pageSizeOption = [12, 20, 50];
  constructor(
    private service: TagsService,
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
          this.dataSource = new MatTableDataSource<TagsItemDto>(this.data);
        }
        this.isLoading = false;
      });
  }

  deleteConfirm(item: TagsItemDto): void {
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
  delete(item: TagsItemDto): void {
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
  openAddDialog(): void {
    this.dialogRef = this.dialog.open(this.addTagTmp, {
      minWidth: 600
    });
  }

  addTags(): void {
    if (this.newTags && this.newTags.length > 0) {
      this.newTags.forEach(value => {
        var randIndex = Math.floor(Math.random() * this.colors.length);
        value.color = this.colors[randIndex];
      });
      this.isProcessing = true;
      this.service.batchAdd(this.newTags)
        .subscribe({
          next: (res) => {
            if (res) {
              this.dialogRef?.close();
              this.snb.open('添加成功' + res + '个标签');
              this.newTags = [];
              this.isProcessing = false;
              this.getList();
            } else {
            }
          },
          error: (error) => {
            this.snb.open(error.detail);
            this.isProcessing = false;
          }
        });
    }
  }

  add(event: MatChipInputEvent): void {
    const value = (event.value || '').trim();
    if (value) {
      this.newTags.push({ name: value });
    }
    event.chipInput!.clear();
  }

  remove(tag: TagsAddDto): void {
    const index = this.newTags.indexOf(tag);

    if (index >= 0) {
      this.newTags.splice(index, 1);
    }
  }

  editTag(tag: TagsAddDto, event: MatChipEditedEvent) {
    const value = event.value.trim();

    // Remove fruit if it no longer has a name
    if (!value) {
      this.remove(tag);
      return;
    }

    // Edit existing fruit
    const index = this.newTags.indexOf(tag);
    if (index >= 0) {
      this.newTags[index].name = value;
    }
  }

  /**
   * 编辑
   */
  edit(id: string): void {
    this.router.navigate(['../edit/' + id], { relativeTo: this.route });
  }

}
