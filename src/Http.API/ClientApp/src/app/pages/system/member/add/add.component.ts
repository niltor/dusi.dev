import { Component, Inject, OnInit } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { UserService } from 'src/app/share/admin/services/user.service';
import { User } from 'src/app/share/admin/models/user/user.model';
import { UserAddDto } from 'src/app/share/admin/models/user/user-add-dto.model';
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
  data = {} as UserAddDto;
  isLoading = true;
  constructor(

    private service: UserService,
    public snb: MatSnackBar,
    private router: Router,
    private route: ActivatedRoute,
    private location: Location
    // public dialogRef: MatDialogRef<AddComponent>,
    // @Inject(MAT_DIALOG_DATA) public dlgData: { id: '' }
  ) {

  }

  get userName() { return this.formGroup.get('userName'); }
  get password() { return this.formGroup.get('password'); }
  get email() { return this.formGroup.get('email'); }


  ngOnInit(): void {
    this.initForm();

    // TODO:获取其他相关数据后设置加载状态
    this.isLoading = false;
  }

  initForm(): void {
    this.formGroup = new FormGroup({
      userName: new FormControl(null, [Validators.minLength(3), Validators.maxLength(100)]),
      password: new FormControl(null, [Validators.minLength(6), Validators.maxLength(100)]),
      email: new FormControl(null, [Validators.minLength(6), Validators.maxLength(100)]),

    });
  }
  getValidatorMessage(type: string): string {
    switch (type) {
      case 'userName':
        return this.userName?.errors?.['required'] ? 'UserName必填' :
          this.userName?.errors?.['minlength'] ? 'UserName长度最少3位' :
            this.userName?.errors?.['maxlength'] ? 'UserName长度最多位' : '';
      case 'password':
        return this.password?.errors?.['required'] ? 'Password必填' :
          this.password?.errors?.['minlength'] ? 'Password长度最少6位' :
            this.password?.errors?.['maxlength'] ? 'Password长度最多100位' : '';
      case 'email':
        return this.email?.errors?.['required'] ? 'Email必填' :
          this.email?.errors?.['minlength'] ? 'Email长度最少6位' :
            this.email?.errors?.['maxlength'] ? 'Email长度最多100位' : '';

      default:
        return '';
    }
  }
  add(): void {
    if (this.formGroup.valid) {
      const data = this.formGroup.value as UserAddDto;
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
