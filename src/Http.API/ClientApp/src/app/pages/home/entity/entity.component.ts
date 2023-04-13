import { Component, TemplateRef, ViewChild } from '@angular/core';
import { MatDialogRef, MatDialog } from '@angular/material/dialog';
import { MatSelectionListChange } from '@angular/material/list';
import { MatPaginator, PageEvent } from '@angular/material/paginator';
import { MatSnackBar } from '@angular/material/snack-bar';
import { MatTableDataSource } from '@angular/material/table';
import { EntityLibraryItemDto } from 'src/app/share/client/models/entity-library/entity-library-item-dto.model';
import { EntityModelFilterDto } from 'src/app/share/client/models/entity-model/entity-model-filter-dto.model';
import { EntityModelItemDto } from 'src/app/share/client/models/entity-model/entity-model-item-dto.model';
import { CodeLanguage } from 'src/app/share/client/models/enum/code-language.model';
import { EntityLibraryService } from 'src/app/share/client/services/entity-library.service';
import { EntityModelService } from 'src/app/share/client/services/entity-model.service';

@Component({
  selector: 'app-entity',
  templateUrl: './entity.component.html',
  styleUrls: ['./entity.component.css']
})
export class EntityComponent {
  CodeLanguage = CodeLanguage;
  isLoading = true;
  filter = {} as EntityModelFilterDto;
  environmentId: string | null = null;
  libraries: EntityLibraryItemDto[] = [];
  columns: string[] = ['name', 'comment', 'codeLanguage'];
  pageSizeOption = [20, 50];
  total = 0;
  previewItem: EntityModelItemDto | null = null;
  data: EntityModelItemDto[] = [];
  dataSource!: MatTableDataSource<EntityModelItemDto>;
  @ViewChild(MatPaginator, { static: true }) paginator!: MatPaginator;
  @ViewChild('previewDialog', { static: true }) addTmpl!: TemplateRef<any>;
  dialogRef: MatDialogRef<any> | null = null;
  isCopied = false;
  constructor(
    private service: EntityModelService,
    private LibSrv: EntityLibraryService,
    private snb: MatSnackBar,
    private dialog: MatDialog,
  ) {
    this.filter = {
      pageIndex: 1,
      pageSize: 20
    };
  }
  ngOnInit(): void {
    this.getLibraries();
    this.getList();
  }

  getLibraries(): void {
    this.LibSrv.filter({
      pageIndex: 1,
      pageSize: 99
    }).subscribe({
      next: (res) => {
        if (res) {
          this.libraries = res.data!;
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
  choseLibrary(id: string | null): void {
    this.filter.entityLibraryId = id;
    this.getList();
  }

  applyFilter(event: Event) {
    const filterValue = (event.target as HTMLInputElement).value;
    this.dataSource.filter = filterValue.trim().toLowerCase();
  }
  openPreviewDialog(item: EntityModelItemDto): void {
    this.previewItem = item;
    this.dialogRef = this.dialog.open(this.addTmpl, {
      minWidth: 650,
      minHeight: 300
    });
  }
  copyCode(): void {
    this.isCopied = true;
    setTimeout(() => {
      this.isCopied = false;
    }, 1500);
  }
}
