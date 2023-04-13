import { Component, OnInit, TemplateRef, ViewChild } from '@angular/core';
import { EntityModelService } from 'src/app/share/client/services/entity-model.service';
import { ActivatedRoute, Router } from '@angular/router';
import { ConfirmDialogComponent } from 'src/app/components/confirm-dialog/confirm-dialog.component';
import { EntityModelItemDto } from 'src/app/share/client/models/entity-model/entity-model-item-dto.model';
import { EntityModelFilterDto } from 'src/app/share/client/models/entity-model/entity-model-filter-dto.model';
import { MatPaginator, PageEvent } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';
import { MatSnackBar } from '@angular/material/snack-bar';
import { MatDialog, MatDialogRef } from '@angular/material/dialog';
import { CodeLanguage } from 'src/app/share/client/models/enum/code-language.model';
import { EntityLibraryService } from 'src/app/share/client/services/entity-library.service';
import { EntityLibraryItemDto } from 'src/app/share/client/models/entity-library/entity-library-item-dto.model';
import { MatSelectChange } from '@angular/material/select';
import { log } from 'console';

@Component({
  selector: 'app-index',
  templateUrl: './index.component.html',
  styleUrls: ['./index.component.css']
})
export class IndexComponent implements OnInit {
  CodeLanguage = CodeLanguage;
  @ViewChild(MatPaginator, { static: true }) paginator!: MatPaginator;
  @ViewChild('previewDialog', { static: true }) previewTmpl!: TemplateRef<any>;
  dialogRef: MatDialogRef<any> | null = null;
  isLoading = true;
  isProcessing = false;
  total = 0;
  isCopied = false;
  data: EntityModelItemDto[] = [];
  libs: EntityLibraryItemDto[] = [];
  previewItem: EntityModelItemDto | null = null;
  columns: string[] = ['name', 'comment', 'codeLanguage', 'languageVersion', 'actions'];
  dataSource!: MatTableDataSource<EntityModelItemDto>;
  filter: EntityModelFilterDto;
  pageSizeOption = [12, 20, 50];
  constructor(
    private service: EntityModelService,
    private libSrv: EntityLibraryService,
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
    this.getLibs();
    this.getList();
  }

  getLibs(): void {
    this.libSrv.filter({
      pageIndex: 1,
      pageSize: 99
    }).subscribe({
      next: (res) => {
        if (res.data) {
          this.libs = res.data;
        } else {
        }
      },
      error: (error) => {
        this.snb.open(error.detail);
      }
    });
  }

  getList(event?: PageEvent): void {
    this.isLoading = true;
    if (event) {
      this.filter.pageIndex = event.pageIndex + 1;
      this.filter.pageSize = event.pageSize;
    }
    this.service.filter(this.filter)
      .subscribe({
        next: (res) => {
          if (res.data) {
            this.data = res.data;
            this.total = res.count;
            this.dataSource = new MatTableDataSource<EntityModelItemDto>(this.data);

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

  choseLibrary(event: MatSelectChange): void {
    this.filter.entityLibraryId = event.value;
    this.getList();
  }
  deleteConfirm(item: EntityModelItemDto): void {
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
  delete(item: EntityModelItemDto): void {
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

  openPreview(item: EntityModelItemDto): void {
    this.previewItem = item;
    this.dialogRef = this.dialog.open(this.previewTmpl, {
      minWidth: 600
    });
  }
  copyCode(): void {
    this.isCopied = true;
    setTimeout(() => {
      this.isCopied = false;
    }, 1500);
  }

  /**
   * 编辑
   */
  edit(id: string): void {
    this.router.navigate(['../edit/' + id], { relativeTo: this.route });
  }

}
