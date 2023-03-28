import { Component } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { BlogFilterDto } from 'src/app/share/client/models/blog/blog-filter-dto.model';
import { BlogItemDto } from 'src/app/share/client/models/blog/blog-item-dto.model';
import { EnumDictionary } from 'src/app/share/client/models/enum-dictionary.model';
import { ThirdNewsFilterDto } from 'src/app/share/client/models/third-news/third-news-filter-dto.model';
import { ThirdNewsItemDto } from 'src/app/share/client/models/third-news/third-news-item-dto.model';
import { ThirdNewsOptionsDto } from 'src/app/share/client/models/third-news/third-news-options-dto.model';
import { BlogService } from 'src/app/share/client/services/blog.service';
import { ThirdNewsService } from 'src/app/share/client/services/third-news.service';

@Component({
  selector: 'app-blog',
  templateUrl: './blog.component.html',
  styleUrls: ['./blog.component.css']
})
export class BlogComponent {
  isLoading = true;
  blogs: BlogItemDto[] = [];
  options: ThirdNewsOptionsDto | null = null;
  newsTypeOptions: EnumDictionary[] | null = [];
  techTypeOptions: EnumDictionary[] | null = [];

  filter: BlogFilterDto;
  pageIndex = 1;
  count = 0;

  constructor(
    private service: BlogService,
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
            this.blogs = res.data!;
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

  }


}
