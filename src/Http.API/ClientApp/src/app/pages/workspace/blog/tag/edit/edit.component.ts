import { Component, Inject, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Tags } from 'src/app/share/models/tags/tags.model';
import { TagsService } from 'src/app/share/services/tags.service';
import { MatSnackBar } from '@angular/material/snack-bar';
import { TagsUpdateDto } from 'src/app/share/models/tags/tags-update-dto.model';
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
  data = {} as Tags;
  updateData = {} as TagsUpdateDto;
  formGroup!: FormGroup;
    constructor(
    
    private service: TagsService,
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
    get color() { return this.formGroup.get('color'); }


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
      name: new FormControl(this.data.name, [Validators.maxLength(50)]),
      color: new FormControl(this.data.color, [Validators.maxLength(20)]),

    });
  }
  getValidatorMessage(type: string): string {
    switch (type) {
      case 'name':
        return this.name?.errors?.['required'] ? 'Name必填' :
          this.name?.errors?.['minlength'] ? 'Name长度最少位' :
            this.name?.errors?.['maxlength'] ? 'Name长度最多50位' : '';
      case 'color':
        return this.color?.errors?.['required'] ? 'Color必填' :
          this.color?.errors?.['minlength'] ? 'Color长度最少位' :
            this.color?.errors?.['maxlength'] ? 'Color长度最多20位' : '';

      default:
        return '';
    }
  }
  edit(): void {
    if(this.formGroup.valid) {
      this.updateData = this.formGroup.value as TagsUpdateDto;
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
