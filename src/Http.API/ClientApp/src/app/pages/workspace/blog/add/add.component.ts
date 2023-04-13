import { Component, Inject, OnInit } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { BlogService } from 'src/app/share/client/services/blog.service';
import { BlogAddDto } from 'src/app/share/client/models/blog/blog-add-dto.model';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ActivatedRoute, Router } from '@angular/router';
import { Location } from '@angular/common';
import { LanguageType } from 'src/app/share/client/models/enum/language-type.model';
import { Catalog } from 'src/app/share/admin/models/catalog.model';
import { CatalogService } from 'src/app/share/client/services/catalog.service';
import { TagsService } from 'src/app/share/client/services/tags.service';
import { forkJoin, lastValueFrom } from 'rxjs';
import { TagsItemDto } from 'src/app/share/client/models/tags/tags-item-dto.model';
import { BlogType } from 'src/app/share/client/models/enum/blog-type.model';

import 'prismjs/plugins/line-numbers/prism-line-numbers.js';
import 'prismjs/components/prism-typescript.min.js';
import 'prismjs/components/prism-powershell.min.js';
import 'prismjs/components/prism-csharp.min.js';
import 'prismjs/components/prism-markup.min.js';
import 'prismjs/components/prism-yaml.min.js';
import 'prismjs/components/prism-docker.min.js';
import { MarkdownService } from 'ngx-markdown';

@Component({
  selector: 'app-add',
  templateUrl: './add.component.html',
  styleUrls: ['./add.component.css']
})
export class AddComponent implements OnInit {
  LanguageType = LanguageType;
  BlogType = BlogType;
  formGroup!: FormGroup;
  data = {} as BlogAddDto;
  catalogs: Catalog[] = [];
  allTags: TagsItemDto[] = [];
  isLoading = true;
  isPreview = false;
  isSplitView = false;
  isProcessing = false;
  constructor(
    private service: BlogService,
    private catalogSrv: CatalogService,
    private tagSrv: TagsService,
    private markdownService: MarkdownService,
    public snb: MatSnackBar,
    private router: Router,
    private route: ActivatedRoute,
    private location: Location
    // public dialogRef: MatDialogRef<AddComponent>,
    // @Inject(MAT_DIALOG_DATA) public dlgData: { id: '' }
  ) {

  }
  get languageType() { return this.formGroup.get('languageType'); }
  get title() { return this.formGroup.get('title'); }
  get description() { return this.formGroup.get('description'); }
  get content() { return this.formGroup.get('content'); }
  get isPublic() { return this.formGroup.get('isPublic'); }
  get isOriginal() { return this.formGroup.get('isOriginal'); }
  get tagIds() { return this.formGroup.get('tagIds'); }
  get catalogId() { return this.formGroup.get('catalogId'); }
  get blogType() { return this.formGroup.get('blogType'); }

  ngOnInit(): void {
    this.initForm();
    forkJoin([this.getCatalogs(), this.getTags()])
      .subscribe(_ => {
        this.isLoading = false;
      });
  }

  async getCatalogs(): Promise<void> {
    const res = await lastValueFrom(this.catalogSrv.getLeaf());
    if (res) {
      this.catalogs = res;
    }
  }

  async getTags(): Promise<void> {
    let res = await lastValueFrom(this.tagSrv.filter({
      pageIndex: 1, pageSize: 99
    }));
    if (res) {
      this.allTags = res.data!;
    }
  }

  toggleEditor(preview: boolean): void {
    this.isPreview = preview;
    this.isSplitView = false;
    if (preview) {
      // this.previewContent = this.content?.value;
      this.markdownService.reload();
    }
  }
  splitView(): void {
    this.isSplitView = !this.isSplitView;
  }
  initForm(): void {
    this.formGroup = new FormGroup({
      languageType: new FormControl(LanguageType.CN, []),
      title: new FormControl(null, [Validators.required, Validators.maxLength(100)]),
      description: new FormControl(null, [Validators.maxLength(300)]),
      content: new FormControl(null, [Validators.required, Validators.maxLength(10000)]),
      isPublic: new FormControl(true, []),
      isOriginal: new FormControl(true, []),
      tagIds: new FormControl(null, []),
      blogType: new FormControl(0, [Validators.required]),
      catalogId: new FormControl<string>('', [Validators.required])

    });
  }
  getValidatorMessage(type: string): string {
    switch (type) {
      case 'languageType':
        return this.languageType?.errors?.['required'] ? 'LanguageType必填' :
          this.languageType?.errors?.['minlength'] ? 'LanguageType长度最少位' :
            this.languageType?.errors?.['maxlength'] ? 'LanguageType长度最多位' : '';
      case 'blogType':
        return this.blogType?.errors?.['required'] ? 'BlogType必填' :
          this.blogType?.errors?.['minlength'] ? 'BlogType长度最少位' :
            this.blogType?.errors?.['maxlength'] ? 'BlogType长度最多位' : '';
      case 'title':
        return this.title?.errors?.['required'] ? 'Title必填' :
          this.title?.errors?.['minlength'] ? 'Title长度最少位' :
            this.title?.errors?.['maxlength'] ? 'Title长度最多100位' : '';
      case 'description':
        return this.description?.errors?.['required'] ? 'Description必填' :
          this.description?.errors?.['minlength'] ? 'Description长度最少位' :
            this.description?.errors?.['maxlength'] ? 'Description长度最多300位' : '';
      case 'content':
        return this.content?.errors?.['required'] ? 'Content必填' :
          this.content?.errors?.['minlength'] ? 'Content长度最少位' :
            this.content?.errors?.['maxlength'] ? 'Content长度最多10000位' : '';
      case 'isPublic':
        return this.isPublic?.errors?.['required'] ? 'IsPublic必填' :
          this.isPublic?.errors?.['minlength'] ? 'IsPublic长度最少位' :
            this.isPublic?.errors?.['maxlength'] ? 'IsPublic长度最多位' : '';
      case 'isOriginal':
        return this.isOriginal?.errors?.['required'] ? 'IsOriginal必填' :
          this.isOriginal?.errors?.['minlength'] ? 'IsOriginal长度最少位' :
            this.isOriginal?.errors?.['maxlength'] ? 'IsOriginal长度最多位' : '';
      case 'catalogId':
        return this.catalogId?.errors?.['required'] ? '分类必填' :
          this.catalogId?.errors?.['minlength'] ? 'Tags长度最少位' :
            this.catalogId?.errors?.['maxlength'] ? 'Tags长度最多位' : '';
      default:
        return '';
    }
  }

  add(): void {
    if (this.formGroup.valid) {
      this.isProcessing = true;
      const data = this.formGroup.value as BlogAddDto;
      this.service.add(data)
        .subscribe({
          next: (res) => {
            if (res) {
              this.snb.open('添加成功');
              // this.dialogRef.close(res);
              this.router.navigate(['../index'], { relativeTo: this.route });
            }
            this.isProcessing = false;

          },
          error: (error) => {
            this.snb.open(error.detail);
            this.isProcessing = false;
          }
        });
    }
  }
  back(): void {
    this.location.back();
  }
}
