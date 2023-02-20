import { Component } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ActivatedRoute, Router } from '@angular/router';
import { LoginService } from 'src/app/auth/login.service';
import { User } from 'src/app/share/models/user/user.model';
import { SystemUserService } from 'src/app/share/services/system-user.service';
import { UserService } from 'src/app/share/services/user.service';
@Component({
  selector: 'app-info',
  templateUrl: './info.component.html',
  styleUrls: ['./info.component.css']
})
export class InfoComponent {
  id!: string;
  isLoading = true;
  data = {} as User;

  constructor(
    private service: UserService,
    private sysSrv: SystemUserService,
    private snb: MatSnackBar,
    private route: ActivatedRoute,
    private auth: LoginService,
    private router: Router,
  ) {
    this.id = this.auth.id!;
  }

  ngOnInit(): void {
    this.getDetail();
  }
  getDetail(): void {
    this.service.getDetail(this.id)
      .subscribe({
        next: (res) => {
          if (res) {
            this.data = res;
            this.isLoading = false;
          }
        },
        error: (error) => {
          this.snb.open(error.detail);
        }
      });
  }
}
