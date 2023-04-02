import { Component } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { EnumDictionary } from 'src/app/share/client/models/enum-dictionary.model';
import { ThirdNewsFilterDto } from 'src/app/share/client/models/third-news/third-news-filter-dto.model';
import { ThirdNewsItemDto } from 'src/app/share/client/models/third-news/third-news-item-dto.model';
import { ThirdNewsOptionsDto } from 'src/app/share/client/models/third-news/third-news-options-dto.model';
import { ThirdVideoItemDto } from 'src/app/share/client/models/third-video/third-video-item-dto.model';
import { ThirdNewsService } from 'src/app/share/client/services/third-news.service';
import { ThirdVideoService } from 'src/app/share/client/services/third-video.service';


@Component({
  selector: 'app-news',
  templateUrl: './news.component.html',
  styleUrls: ['./news.component.css']
})
export class NewsComponent {
  isLoading = true;
  news: ThirdNewsItemDto[] = [];
  videos: ThirdVideoItemDto[] = [];
  latestVideo: ThirdVideoItemDto | null = null;
  options: ThirdNewsOptionsDto | null = null;
  newsTypeOptions: EnumDictionary[] | null = [];
  techTypeOptions: EnumDictionary[] | null = [];

  filter: ThirdNewsFilterDto;
  pageIndex = 1;
  count = 0;

  constructor(
    private service: ThirdNewsService,
    private videoSrv: ThirdVideoService,
    private snb: MatSnackBar
  ) {
    this.filter = {
      pageIndex: 1,
      pageSize: 100,
      newsType: null,
      techType: null,
      onlyWeek: false
    }
  }

  ngOnInit(): void {
    this.getOptions();
    this.getVideos();
    this.getNews();
  }

  getNews(): void {
    this.isLoading = true;
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

  getVideos(): void {
    this.videoSrv.filter({
      pageIndex: 1,
      pageSize: 10
    }).subscribe({
      next: (res) => {
        if (res) {
          this.videos = res.data!;
          if (res.data && res.data.length > 0) {
            this.latestVideo = res.data[0];
          }
        } else {
        }
      },
      error: (error) => {
        this.snb.open(error.detail);
      }
    });
  }
  getOptions(): void {
    this.service.getEnumOptions()
      .subscribe({
        next: (res) => {
          if (res) {
            this.options = res;
            this.techTypeOptions = res.techType!;
            this.newsTypeOptions = res.newsType!;
          } else {
          }
        },
        error: (error) => {
          this.snb.open(error.detail);
        }
      });
  }
  onlyWeek(): void {
    this.filter.onlyWeek = !this.filter.onlyWeek;
  }
  selectNewsType(value: number | null): void {
    this.filter.newsType = value;
    this.getNews();
  }

}
