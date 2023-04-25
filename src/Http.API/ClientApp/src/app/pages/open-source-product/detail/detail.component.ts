import { Component, OnInit } from '@angular/core';
import { OpenSourceProductService } from 'src/app/share/admin/services/open-source-product.service';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ActivatedRoute, Router } from '@angular/router';
import { OpenSourceProduct } from 'src/app/share/admin/models/open-source-product/open-source-product.model';
import { Location } from '@angular/common';

@Component({
  selector: 'app-detail',
  templateUrl: './detail.component.html',
  styleUrls: ['./detail.component.css']
})
export class DetailComponent implements OnInit {
  id!: string;
  isLoading = true;
  data = {} as OpenSourceProduct;
  constructor(
    private service: OpenSourceProductService,
    private snb: MatSnackBar,
    private route: ActivatedRoute,
    public location: Location,
    private router: Router,
  ) {
    const id = this.route.snapshot.paramMap.get('id');
    if (id) {
      this.id = id;
    } else {
      // TODO: id为空

    }
  }
  ngOnInit(): void {
    this.getDetail();
  }
  getDetail(): void {
    this.service.getDetail(this.id)
      .subscribe({
        next: (res) => {
          if(res) {
            this.data = res;
              this.isLoading = false;
            }
        },
        error:(error) => {
          this.snb.open(error);
        }
      })
  }
  back(): void {
    this.location.back();
  }

  edit(): void {
    this.router.navigate(['../../edit/' + this.id], { relativeTo: this.route });

  }
}
