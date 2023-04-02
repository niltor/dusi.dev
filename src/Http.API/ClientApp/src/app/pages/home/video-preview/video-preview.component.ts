import { Component } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { DomSanitizer, SafeResourceUrl } from '@angular/platform-browser';
import { ActivatedRoute } from '@angular/router';
import { ThirdVideo } from 'src/app/share/client/models/third-video/third-video.model';
import { ThirdVideoService } from 'src/app/share/client/services/third-video.service';

@Component({
  selector: 'app-video-preview',
  templateUrl: './video-preview.component.html',
  styleUrls: ['./video-preview.component.css']
})
export class VideoPreviewComponent {
  id: string | null = null;
  url: SafeResourceUrl = '';
  video: ThirdVideo | null = null;
  isLoading = true;
  constructor(
    private route: ActivatedRoute,
    private sanitizer: DomSanitizer,
    private service: ThirdVideoService,
    private snb: MatSnackBar
  ) {
    const id = this.route.snapshot.paramMap.get('id');
    if (id) {
      this.id = id;
    }
  }

  ngOnInit(): void {
    this.getDetail();
  }

  getDetail(): void {
    if (this.id) {
      this.service.getDetail(this.id)
        .subscribe({
          next: (res) => {
            if (res) {
              this.video = res;
              this.url = this.sanitizer.bypassSecurityTrustResourceUrl(`https://player.bilibili.com/player.html?bvid=${res.identity}&page=1`);
            } else {
            }
          },
          error: (error) => {
            this.snb.open(error.detail);
          }
        });
    }
  }
}
