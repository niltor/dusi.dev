import { Component } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ThirdNewsFilterDto } from 'src/app/share/client/models/third-news/third-news-filter-dto.model';
import { ThirdNewsItemDto } from 'src/app/share/client/models/third-news/third-news-item-dto.model';
import { ThirdNewsOptionsDto } from 'src/app/share/client/models/third-news/third-news-options-dto.model';
import { ThirdNewsService } from 'src/app/share/client/services/third-news.service';


@Component({
  selector: 'app-news',
  templateUrl: './news.component.html',
  styleUrls: ['./news.component.css']
})
export class NewsComponent {
  isLoading = true;
  news: ThirdNewsItemDto[] = [];
  options: ThirdNewsOptionsDto | null = null;
  filter: ThirdNewsFilterDto;
  pageIndex = 1;
  count = 0;

  constructor(
    private service: ThirdNewsService,
    private snb: MatSnackBar
  ) {
    this.filter = {
      pageIndex: 1,
      pageSize: 100
    }
  }

  ngOnInit(): void {
    this.getOptions();
    this.getNews();
  }

  getNews(): void {
    this.service.filter(this.filter)
      .subscribe({
        next: (res) => {
          if (res) {
            this.news = res.data!;
            this.count = res.count;
          } else {

          }
          this.isLoading = false;
        },
        error: (error) => {
          this.snb.open(error.detail);
          this.isLoading = false;
        }
      });
  }

  getOptions(): void {
    this.service.getEnumOptions()
      .subscribe({
        next: (res) => {
          if (res) {
            this.options = res;
          } else {
          }
        },
        error: (error) => {
          this.snb.open(error.detail);
        }
      });
  }
}
