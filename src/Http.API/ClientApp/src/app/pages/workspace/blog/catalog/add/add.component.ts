import { Component, Inject, OnInit } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { CatalogService } from 'src/app/share/client/services/catalog.service';
import { CatalogAddDto } from 'src/app/share/client/models/catalog/catalog-add-dto.model';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ActivatedRoute, Router } from '@angular/router';
import { Location } from '@angular/common';
import { CatalogItemDto } from 'src/app/share/client/models/catalog/catalog-item-dto.model';

@Component({
  selector: 'app-add',
  templateUrl: './add.component.html',
  styleUrls: ['./add.component.css']
})
export class AddComponent implements OnInit {
  formGroup!: FormGroup;
  data = {} as CatalogAddDto;
  catalogs: CatalogItemDto[] = [];
  selectedCatalog = {} as CatalogItemDto;
  isLoading = true;
  constructor(
    private service: CatalogService,
    public snb: MatSnackBar,
    private router: Router,
    private route: ActivatedRoute,
    private location: Location
    // public dialogRef: MatDialogRef<AddComponent>,
    // @Inject(MAT_DIALOG_DATA) public dlgData: { id: '' }
  ) {
  }

  get name() { return this.formGroup.get('name'); }
  get parentId() { return this.formGroup.get('parentId'); }

  ngOnInit(): void {
    this.initForm();
    this.getCatalog();

  }

  initForm(): void {
    this.formGroup = new FormGroup({
      name: new FormControl(null, [Validators.required, Validators.maxLength(50)]),
      parentId: new FormControl(null, []),

    });
  }

  getCatalog(): void {
    this.service.filter({
      pageIndex: 1,
      pageSize: 100
    }).subscribe({
      next: (res) => {
        if (res) {
          this.catalogs = res.data!;
        }
        this.isLoading = false;
      },
      error: (error) => {
        this.isLoading = false;
      }
    })
  }
  getValidatorMessage(type: string): string {
    switch (type) {
      case 'name':
        return this.name?.errors?.['required'] ? 'Name必填' :
          this.name?.errors?.['minlength'] ? 'Name长度最少位' :
            this.name?.errors?.['maxlength'] ? 'Name长度最多50位' : '';
      default:
        return '';
    }
  }

  add(): void {
    if (this.formGroup.valid) {
      const data = this.formGroup.value as CatalogAddDto;
      console.log(data);

      this.service.add(data)
        .subscribe({
          next: (res) => {
            if (res) {
              this.snb.open('添加成功');
              // this.dialogRef.close(res);
              this.router.navigate(['../index'], { relativeTo: this.route });
            }
          },
          error: () => {

          }
        });
    }
  }
  back(): void {
    this.location.back();
  }
}
