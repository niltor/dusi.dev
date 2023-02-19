import { Component, Inject, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { User } from 'src/app/share/models/user/user.model';
import { UserService } from 'src/app/share/services/user.service';
import { MatSnackBar } from '@angular/material/snack-bar';
import { UserUpdateDto } from 'src/app/share/models/user/user-update-dto.model';
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
  data = {} as User;
  updateData = {} as UserUpdateDto;
  formGroup!: FormGroup;
  constructor(

    private service: UserService,
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

  get userName() { return this.formGroup.get('userName'); }
  get password() { return this.formGroup.get('password'); }
  get email() { return this.formGroup.get('email'); }
  get emailConfirmed() { return this.formGroup.get('emailConfirmed'); }


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
      userName: new FormControl(this.data.userName, []),
      password: new FormControl('', []),
      email: new FormControl(this.data.email, [Validators.maxLength(100)]),
      emailConfirmed: new FormControl(this.data.emailConfirmed, []),

    });
  }
  getValidatorMessage(type: string): string {
    switch (type) {
      case 'userName':
        return this.userName?.errors?.['required'] ? 'UserName必填' :
          this.userName?.errors?.['minlength'] ? 'UserName长度最少位' :
            this.userName?.errors?.['maxlength'] ? 'UserName长度最多位' : '';
      case 'password':
        return this.password?.errors?.['required'] ? 'Password必填' :
          this.password?.errors?.['minlength'] ? 'Password长度最少位' :
            this.password?.errors?.['maxlength'] ? 'Password长度最多位' : '';
      case 'email':
        return this.email?.errors?.['required'] ? 'Email必填' :
          this.email?.errors?.['minlength'] ? 'Email长度最少位' :
            this.email?.errors?.['maxlength'] ? 'Email长度最多100位' : '';
      case 'emailConfirmed':
        return this.emailConfirmed?.errors?.['required'] ? 'EmailConfirmed必填' :
          this.emailConfirmed?.errors?.['minlength'] ? 'EmailConfirmed长度最少位' :
            this.emailConfirmed?.errors?.['maxlength'] ? 'EmailConfirmed长度最多位' : '';

      default:
        return '';
    }
  }
  edit(): void {
    if (this.formGroup.valid) {
      this.updateData = this.formGroup.value as UserUpdateDto;
      this.service.update(this.id, this.updateData)
        .subscribe({
          next: (res) => {
            if (res) {
              this.snb.open('修改成功');
              // this.dialogRef.close(res);
              this.router.navigate(['../../index'], { relativeTo: this.route });
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
