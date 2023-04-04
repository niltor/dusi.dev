import { Component, Inject, OnInit } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { TagsService } from 'src/app/share/client/services/tags.service';
import { TagsAddDto } from 'src/app/share/client/models/tags/tags-add-dto.model';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ActivatedRoute, Router } from '@angular/router';
import { Location } from '@angular/common';
import { CommonColors } from 'src/app/share/const';

@Component({
  selector: 'app-add',
  templateUrl: './add.component.html',
  styleUrls: ['./add.component.css']
})
export class AddComponent implements OnInit {

  formGroup!: FormGroup;
  data = {} as TagsAddDto;
  isLoading = true;
  colors = CommonColors;
  constructor(
    private service: TagsService,
    public snb: MatSnackBar,
    private router: Router,
    private route: ActivatedRoute,
    private location: Location
    // public dialogRef: MatDialogRef<AddComponent>,
    // @Inject(MAT_DIALOG_DATA) public dlgData: { id: '' }
  ) {

  }

  get name() { return this.formGroup.get('name'); }
  get color() { return this.formGroup.get('color'); }


  ngOnInit(): void {
    this.initForm();

    // TODO:获取其他相关数据后设置加载状态
    this.isLoading = false;
  }

  initForm(): void {
    this.formGroup = new FormGroup({
      name: new FormControl(null, [Validators.required, Validators.maxLength(50)]),
      color: new FormControl(null, [Validators.maxLength(20)]),

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

  add(): void {
    if (this.formGroup.valid) {
      const data = this.formGroup.value as TagsAddDto;
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
