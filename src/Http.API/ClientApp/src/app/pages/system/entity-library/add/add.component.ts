import { Component, Inject, OnInit } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { EntityLibraryService } from 'src/app/share/services/entity-library.service';
import { EntityLibrary } from 'src/app/share/models/entity-library/entity-library.model';
import { EntityLibraryAddDto } from 'src/app/share/models/entity-library/entity-library-add-dto.model';
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
    data = {} as EntityLibraryAddDto;
    isLoading = true;
    constructor(
        
        private service: EntityLibraryService,
        public snb: MatSnackBar,
        private router: Router,
        private route: ActivatedRoute,
        private location: Location
        // public dialogRef: MatDialogRef<AddComponent>,
        // @Inject(MAT_DIALOG_DATA) public dlgData: { id: '' }
    ) {

    }

    get name() { return this.formGroup.get('name'); }
    get description() { return this.formGroup.get('description'); }
    get isPublic() { return this.formGroup.get('isPublic'); }


  ngOnInit(): void {
    this.initForm();
    
    // TODO:获取其他相关数据后设置加载状态
    this.isLoading = false;
  }
  
  initForm(): void {
    this.formGroup = new FormGroup({
      name: new FormControl(null, [Validators.required,Validators.maxLength(60)]),
      description: new FormControl(null, [Validators.maxLength(200)]),
      isPublic: new FormControl(null, []),

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

  add(): void {
    if(this.formGroup.valid) {
    const data = this.formGroup.value as EntityLibraryAddDto;
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
