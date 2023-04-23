import { AfterViewInit, Component } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ActivatedRoute, Router } from '@angular/router';
import { Blog } from 'src/app/share/client/models/blog/blog.model';
import { BlogService } from 'src/app/share/client/services/blog.service';

import 'prismjs/plugins/line-numbers/prism-line-numbers.js';
import 'prismjs/components/prism-typescript.min.js';
import 'prismjs/components/prism-powershell.min.js';
import 'prismjs/components/prism-csharp.min.js';
import 'prismjs/components/prism-markup.min.js';
import 'prismjs/components/prism-yaml.min.js';
import 'prismjs/components/prism-docker.min.js';
import { Meta, Title } from '@angular/platform-browser';

@Component({
  selector: 'app-blog-detail',
  templateUrl: './blog-detail.component.html',
  styleUrls: ['./blog-detail.component.css'],
})
export class BlogDetailComponent implements AfterViewInit {
  id!: string;
  isLoading = true;
  data = {} as Blog;
  testContent: string = '';
  isCopied = false;
  constructor(
    private service: BlogService,
    private snb: MatSnackBar,
    private route: ActivatedRoute,
    private router: Router,
    private meta: Meta,
    private title: Title
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
  ngAfterViewInit(): void {

  }
  getDetail(): void {
    this.service.getDetail(this.id)
      .subscribe({
        next: (res) => {
          if (res) {
            this.data = res;
            this.isLoading = false;
            this.setMeta();
          }

        },
        error: (error) => {
          this.snb.open(error.detail);
        }
      })
  }
  setMeta(): void {
    this.meta.addTags([
      { name: 'description', content: this.data.description ?? '' },
      { name: 'author', content: this.data.authors ?? '' },
      { name: 'keywords', content: this.data.tags?.map(t => t.name).join(',') ?? '' }
    ]);
    this.title.setTitle(this.data.title ?? this.title);
  }

  copyCode(): void {
    this.isCopied = true;
    setTimeout(() => {
      this.isCopied = false;
    }, 1500);
  }
}
