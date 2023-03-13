import { Component } from '@angular/core';
import { FormGroup, FormControl, Validators, ValidatorFn, AbstractControl, ValidationErrors, FormGroupDirective, NgForm } from '@angular/forms';
import { ErrorStateMatcher } from '@angular/material/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { lastValueFrom } from 'rxjs';
import { LoginService } from 'src/app/auth/login.service';
import { User } from 'src/app/share/client/models/user/user.model';
import { SystemUserService } from 'src/app/share/admin/services/system-user.service';
import { UserService } from 'src/app/share/client/services/user.service';

@Component({
  selector: 'app-password',
  templateUrl: './password.component.html',
  styleUrls: ['./password.component.css']
})
export class PasswordComponent {
  id!: string;
  userData = {} as User;
  isLoading = true;
  form!: FormGroup;
  matcher = new MyErrorStateMatcher();
  constructor(
    private service: UserService,
    private sysUserSrv: SystemUserService,

    private snb: MatSnackBar,
    private auth: LoginService
  ) {
    this.id = this.auth.id!;
  }
  get password() { return this.form.get('password'); }
  get rePassword() { return this.form.get('rePassword') }
  ngOnInit(): void {
    this.getMyInfo();
    this.initForm();
  }
  async getMyInfo() {
    let res = await lastValueFrom(this.service.getDetail(this.id))
    if (res) {
      this.userData = res;
      this.isLoading = false;
    }
  }
  initForm(): void {
    this.form = new FormGroup({
      password: new FormControl(null, [Validators.required, Validators.minLength(6), Validators.maxLength(30)]),
      rePassword: new FormControl(null, [Validators.required, Validators.minLength(6), Validators.maxLength(30)]),
    }, [this.checkPasswords]);
  }

  getValidatorMessage(type: string): string {
    switch (type) {
      case 'password':
        return this.password?.errors?.['required'] ? 'password必填' :
          this.password?.errors?.['minlength'] ? 'password长度最少6位' :
            this.password?.errors?.['maxlength'] ? 'password长度最多30位' : '';
      case 'rePassword':
        return this.rePassword?.errors?.['required'] ? 'rePassword必填' :
          this.rePassword?.errors?.['minlength'] ? 'rePassword长度最少6位' :
            this.rePassword?.errors?.['maxlength'] ? 'rePassword长度最多30位' : '';
      default:
        return '';
    }
  }
  changePassword(): void {
    if (this.form.valid) {
      if (this.auth.isAdmin) {
        this.sysUserSrv.changeMyPassword(this.password?.value)
          .subscribe(res => {
            if (res) {
              this.snb.open('修改成功');
            } else {
              this.snb.open('修改失败');
            }
          });
      } else {
        this.service.changeMyPassword(this.password?.value)
          .subscribe(res => {
            if (res) {
              this.snb.open('修改成功');
            } else {
              this.snb.open('修改失败');
            }
          });
      }
    }
  }

  checkPasswords: ValidatorFn = (group: AbstractControl): ValidationErrors | null => {
    let pass = group.get('password')?.value;
    let confirmPass = group.get('rePassword')?.value
    return pass === confirmPass ? null : { notSame: true }
  }
}

export class MyErrorStateMatcher implements ErrorStateMatcher {
  isErrorState(control: FormControl | null, form: FormGroupDirective | NgForm | null): boolean {
    const invalidCtrl = !!(control && control.invalid && control.parent?.dirty);
    const invalidParent = !!(control && control.parent && control.parent.invalid && control.parent.dirty);
    return (invalidCtrl || invalidParent);
  }
}
