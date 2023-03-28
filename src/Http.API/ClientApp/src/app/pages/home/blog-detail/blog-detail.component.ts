import { Component, ViewEncapsulation } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ActivatedRoute, Router } from '@angular/router';
import { Blog } from 'src/app/share/client/models/blog/blog.model';
import { BlogService } from 'src/app/share/client/services/blog.service';
import { marked } from 'marked';
import * as prism from 'prismjs';

import 'prismjs/components/prism-typescript.min.js';
import 'prismjs/components/prism-css.min.js';
import 'prismjs/components/prism-javascript.min.js';
import 'prismjs/components/prism-csharp.min.js';
import 'prismjs/components/prism-markup.min.js';




@Component({
  selector: 'app-blog-detail',
  templateUrl: './blog-detail.component.html',
  styleUrls: ['./blog-detail.component.css']
})
export class BlogDetailComponent {
  id!: string;
  isLoading = true;
  data = {} as Blog;
  constructor(
    private service: BlogService,
    private snb: MatSnackBar,
    private route: ActivatedRoute,
    private router: Router,
  ) {

    marked.setOptions({
      highlight: (code, lang) => {
        if (prism.languages[lang]) {
          return prism.highlight(code, prism.languages[lang], lang);
        } else {
          return code;
        }
      },
    });

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
          if (res) {
            this.data = res;
            this.isLoading = false;
            this.data.content = this.renderMarkdown(`# Title
\`single row of text\`
\`\`\`csharp
public function void Main(string[] args){}
\`\`\`
[link](https://www.google.com)`);

          }
        },
        error: (error) => {
          this.snb.open(error);
        }
      })
  }

  renderMarkdown(text: string): string {
    let res = marked.parse(text);
    console.log(res);

    return res;
  }
}
