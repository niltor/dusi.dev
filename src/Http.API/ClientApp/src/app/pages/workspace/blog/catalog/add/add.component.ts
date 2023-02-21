import { Component, Inject, OnInit } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { CatalogService } from 'src/app/share/services/catalog.service';
import { Catalog } from 'src/app/share/models/catalog/catalog.model';
import { CatalogAddDto } from 'src/app/share/models/catalog/catalog-add-dto.model';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ActivatedRoute, Router } from '@angular/router';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { Location } from '@angular/common';

@Component({
    selector: 'app-add',
    templateUrl: './add.component.html',
    styleUrls: ['./add.component.css']
})
export class AddComponent implements OnInit {
    
    formGroup!: FormGroup;
    data = {} as CatalogAddDto;
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
    get level() { return this.formGroup.get('level'); }


  ngOnInit(): void {
    this.initForm();
    
    // TODO:获取其他相关数据后设置加载状态
    this.isLoading = false;
  }
  
  initForm(): void {
    this.formGroup = new FormGroup({
      name: new FormControl(null, [Validators.required,Validators.maxLength(50)]),
      level: new FormControl(null, []),

    });
  }
  getValidatorMessage(type: string): string {
    switch (type) {
      case 'name':
        return this.name?.errors?.['required'] ? 'Name必填' :
          this.name?.errors?.['minlength'] ? 'Name长度最少位' :
            this.name?.errors?.['maxlength'] ? 'Name长度最多50位' : '';
      case 'level':
        return this.level?.errors?.['required'] ? 'Level必填' :
          this.level?.errors?.['minlength'] ? 'Level长度最少位' :
            this.level?.errors?.['maxlength'] ? 'Level长度最多位' : '';

      default:
    return '';
    }
  }

  add(): void {
    if(this.formGroup.valid) {
    const data = this.formGroup.value as CatalogAddDto;
    this.data = { ...data, ...this.data };
    this.service.add(this.data)
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
