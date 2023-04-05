import { Component, OnInit, TemplateRef, ViewChild } from '@angular/core';
import { EntityLibraryService } from 'src/app/share/client/services/entity-library.service';
import { ActivatedRoute, Router } from '@angular/router';
import { ConfirmDialogComponent } from 'src/app/components/confirm-dialog/confirm-dialog.component';
import { EntityLibraryItemDto } from 'src/app/share/client/models/entity-library/entity-library-item-dto.model';
import { EntityLibraryFilterDto } from 'src/app/share/client/models/entity-library/entity-library-filter-dto.model';
import { MatPaginator, PageEvent } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';
import { MatSnackBar } from '@angular/material/snack-bar';
import { MatDialog, MatDialogRef } from '@angular/material/dialog';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { EntityLibraryAddDto } from 'src/app/share/client/models/entity-library/entity-library-add-dto.model';
import { DialogRef } from '@angular/cdk/dialog';

@Component({
  selector: 'app-index',
  templateUrl: './index.component.html',
  styleUrls: ['./index.component.css']
})
export class IndexComponent implements OnInit {
  @ViewChild(MatPaginator, { static: true }) paginator!: MatPaginator;
  @ViewChild('addDialog', { static: true }) addTmpl!: TemplateRef<any>;
  dialogRef: MatDialogRef<any> | null = null;
  isLoading = true;
  total = 0;
  data: EntityLibraryItemDto[] = [];
  columns: string[] = ['name', 'description', 'isPublic', 'createdTime', 'actions'];
  dataSource!: MatTableDataSource<EntityLibraryItemDto>;
  filter: EntityLibraryFilterDto;
  pageSizeOption = [12, 20, 50];
  formGroup!: FormGroup;
  constructor(
    private service: EntityLibraryService,
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

  get name() { return this.formGroup.get('name'); }
  get description() { return this.formGroup.get('description'); }
  get isPublic() { return this.formGroup.get('isPublic'); }

  ngOnInit(): void {
    this.initForm();
    this.getList();
  }
  initForm(): void {
    this.formGroup = new FormGroup({
      name: new FormControl(null, [Validators.required, Validators.maxLength(60)]),
      description: new FormControl(null, [Validators.maxLength(200)]),
      isPublic: new FormControl(false, []),

    });
  }
  getValidatorMessage(type: string): string {
    switch (type) {
      case 'name':
        return this.name?.errors?.['required'] ? 'Name必填' :
          this.name?.errors?.['minlength'] ? 'Name长度最少位' :
            this.name?.errors?.['maxlength'] ? 'Name长度最多60位' : '';
      case 'description':
        return this.description?.errors?.['required'] ? 'Description必填' :
          this.description?.errors?.['minlength'] ? 'Description长度最少位' :
            this.description?.errors?.['maxlength'] ? 'Description长度最多200位' : '';
      case 'isPublic':
        return this.isPublic?.errors?.['required'] ? 'IsPublic必填' :
          this.isPublic?.errors?.['minlength'] ? 'IsPublic长度最少位' :
            this.isPublic?.errors?.['maxlength'] ? 'IsPublic长度最多位' : '';

      default:
        return '';
    }
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
          this.dataSource = new MatTableDataSource<EntityLibraryItemDto>(this.data);
        }
        this.isLoading = false;
      });
  }

  openAddDialog(): void {
    this.dialogRef = this.dialog.open(this.addTmpl, {
      minWidth: 500
    });
  }
  add(): void {
    if (this.formGroup.valid) {
      const data = this.formGroup.value as EntityLibraryAddDto;
      this.service.add(data)
        .subscribe({
          next: (res) => {
            if (res) {
              this.dialogRef?.close();
              this.formGroup.reset();
              this.snb.open('添加成功');
              this.getList();
            }
          },
          error: (error) => {
            this.snb.open(error.detail);
          }
        });
    }
  }
  deleteConfirm(item: EntityLibraryItemDto): void {
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
  delete(item: EntityLibraryItemDto): void {
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
  openAddDialog(): void {
    const ref = this.dialog.open(AddComponent, {
      hasBackdrop: true,
      disableClose: false,
      data: {
      }
    });
    ref.afterClosed().subscribe(res => {
      if (res) {
        this.snb.open('添加成功');
        this.getList();
      }
    });
  }
  openDetailDialog(id: string): void {
    const ref = this.dialog.open(DetailComponent, {
      hasBackdrop: true,
      disableClose: false,
      data: { id }
    });
    ref.afterClosed().subscribe(res => {
      if (res) { }
    });
  }
  
  openEditDialog(id: string): void {
    const ref = this.dialog.open(EditComponent, {
      hasBackdrop: true,
      disableClose: false,
      data: { id }
    });
    ref.afterClosed().subscribe(res => {
      if (res) {
        this.snb.open('修改成功');
        this.getList();
      }
    });
  }*/

  /**
   * 编辑
   */
  edit(id: string): void {
    this.router.navigate(['../edit/' + id], { relativeTo: this.route });
  }

}
