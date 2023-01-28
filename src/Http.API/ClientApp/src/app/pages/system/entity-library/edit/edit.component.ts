import { Component, Inject, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { EntityLibrary } from 'src/app/share/models/entity-library/entity-library.model';
import { EntityLibraryService } from 'src/app/share/services/entity-library.service';
import { MatSnackBar } from '@angular/material/snack-bar';
import { EntityLibraryUpdateDto } from 'src/app/share/models/entity-library/entity-library-update-dto.model';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { Location } from '@angular/common';

@Component({
  selector: 'app-edit',
  templateUrl: './edit.component.html',
  styleUrls: ['./edit.component.css']
})
export class EditComponent implements OnInit {
  
  id!: string;
  isLoading = true;
  data = {} as EntityLibrary;
  updateData = {} as EntityLibraryUpdateDto;
  formGroup!: FormGroup;
    constructor(
    
    private service: EntityLibraryService,
    private snb: MatSnackBar,
    private router: Router,
    private route: ActivatedRoute,
    private location: Location
    // public dialogRef: MatDialogRef<EditComponent>,
    // @Inject(MAT_DIALOG_DATA) public dlgData: { id: '' }
  ) {
    const id = this.route.snapshot.paramMap.get('id');
    if (id) {
      this.id = id;
    } else {
      // TODO: id为空
    }
  }

    get name() { return this.formGroup.get('name'); }
    get description() { return this.formGroup.get('description'); }
    get isPublic() { return this.formGroup.get('isPublic'); }


  ngOnInit(): void {
    this.getDetail();
    
    // TODO:等待数据加载完成
    // this.isLoading = false;
  }
  
  getDetail(): void {
    this.service.getDetail(this.id)
      .subscribe(res => {
        this.data = res;
        this.initForm();
        this.isLoading = false;
      }, error => {
        this.snb.open(error);
      })
  }

  initForm(): void {
    this.formGroup = new FormGroup({
      name: new FormControl(this.data.name, []),
      description: new FormControl(this.data.description, [Validators.maxLength(200)]),
      isPublic: new FormControl(this.data.isPublic, []),

    });
  }
  getValidatorMessage(type: string): string {
    switch (type) {
      case 'name':
        return this.name?.errors?.['required'] ? 'Name必填' :
          this.name?.errors?.['minlength'] ? 'Name长度最少位' :
            this.name?.errors?.['maxlength'] ? 'Name长度最多位' : '';
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
  edit(): void {
    if(this.formGroup.valid) {
      this.updateData = this.formGroup.value as EntityLibraryUpdateDto;
      this.service.update(this.id, this.updateData)
        .subscribe({
          next: (res) => {
            if(res){
              this.snb.open('修改成功');
              // this.dialogRef.close(res);
              this.router.navigate(['../../index'], { relativeTo: this.route });
            }
          },
          error:()=>{
          }
        });
    }
  }

  back(): void {
    this.location.back();
  }

}
