import { Component } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { OpenSourceProductFilterDto } from 'src/app/share/client/models/open-source-product/open-source-product-filter-dto.model';
import { OpenSourceProductItemDto } from 'src/app/share/client/models/open-source-product/open-source-product-item-dto.model';
import { OpenSourceProductService } from 'src/app/share/client/services/open-source-product.service';

@Component({
  selector: 'app-about',
  templateUrl: './about.component.html',
  styleUrls: ['./about.component.css']
})
export class AboutComponent {
  data: OpenSourceProductItemDto[] = []
  isLoading = true;
  filter: OpenSourceProductFilterDto;
  constructor(
    private service: OpenSourceProductService,
    private snb: MatSnackBar,
  ) {

    this.filter = {
      pageIndex: 1,
      pageSize: 12
    };
  }

  ngOnInit(): void {
    this.getList();
  }

  getList(): void {
    this.service.filter(this.filter)
      .subscribe({
        next: (res) => {
          if (res) {
            if (res.data) {
              this.data = res.data;
            }
          } else {
            this.snb.open('');
          }
        },
        error: (error) => {
          this.snb.open(error.detail);
        },
        complete: () => {
          this.isLoading = false;
        }
      });
  }
  splitTags(tags: string | null): string[] {
    if (tags === null) return [];
    return tags.split(',');
  }
}
