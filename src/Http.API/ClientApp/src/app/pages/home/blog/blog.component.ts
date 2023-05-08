import { Component, ViewEncapsulation } from '@angular/core';
import { MatChipListboxChange } from '@angular/material/chips';
import { MatSnackBar } from '@angular/material/snack-bar';
import { BlogFilterDto } from 'src/app/share/client/models/blog/blog-filter-dto.model';
import { BlogItemDto } from 'src/app/share/client/models/blog/blog-item-dto.model';
import { EnumDictionary } from 'src/app/share/client/models/enum-dictionary.model';
import { BlogService } from 'src/app/share/client/services/blog.service';

@Component({
  selector: 'app-blog',
  templateUrl: './blog.component.html',
  styleUrls: ['./blog.component.css'],
  encapsulation: ViewEncapsulation.None
})
export class BlogComponent {
  isLoading = true;
  blogs: BlogItemDto[] = [];
  typeOptions: EnumDictionary[] | null = null;

  filter: BlogFilterDto;
  pageIndex = 1;
  count = 0;
  searchKey: string | null = null;
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
    this.getTypes();
    this.getBlogs();
  }

  getBlogs(): void {
    this.service.publicBlogs(this.filter)
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

  getTypes(): void {
    this.service.getTypes()
      .subscribe({
        next: (res) => {
          if (res) {
            this.typeOptions = res;
          } else {
          }
        },
        error: (error) => {
          this.snb.open(error.detail);
        }
      });
  }

  selectblogsType(type: number | null): void {
    this.filter.blogType = type;
    this.getBlogs();
  }
  selectTag(tag: string): void {
    this.filter.tag = tag;
    this.getBlogs();
  }

  cancelTag(): void {
    this.filter.tag = null;
    this.getBlogs();
  }
  clearFilter() {
    this.filter.title = null;
    this.getBlogs();
  }
}
