import { Component, Inject, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Blog } from 'src/app/share/client/models/blog/blog.model';
import { BlogService } from 'src/app/share/client/services/blog.service';
import { MatSnackBar } from '@angular/material/snack-bar';
import { BlogUpdateDto } from 'src/app/share/client/models/blog/blog-update-dto.model';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { Location } from '@angular/common';
import * as ClassicEditor from 'ng-ckeditor5-classic';
import { CKEditor5 } from '@ckeditor/ckeditor5-angular';
// import { OidcSecurityService } from 'angular-auth-oidc-client';
import { LanguageType } from 'src/app/share/client/models/enum/language-type.model';
import { Catalog } from 'src/app/share/client/models/catalog/catalog.model';
import { CatalogService } from 'src/app/share/client/services/catalog.service';
import { TagsItemDto } from 'src/app/share/client/models/tags/tags-item-dto.model';
import { forkJoin, lastValueFrom } from 'rxjs';
import { TagsService } from 'src/app/share/client/services/tags.service';

@Component({
  selector: 'app-edit',
  templateUrl: './edit.component.html',
  styleUrls: ['./edit.component.css']
})
export class EditComponent implements OnInit {
  public editorConfig!: CKEditor5.Config;
  public editor: CKEditor5.EditorConstructor = ClassicEditor;
  LanguageType = LanguageType;

  id!: string;
  isLoading = true;
  data = {} as Blog;
  catalog: Catalog[] = [];
  allTags: TagsItemDto[] = [];
  selectedTagIds: string[] = [];
  updateData = {} as BlogUpdateDto;
  formGroup!: FormGroup;
  constructor(

    // private authService: OidcSecurityService,
    private service: BlogService,
    private catalogSrv: CatalogService,
    private tagSrv: TagsService,
    private snb: MatSnackBar,
    private router: Router,
    private route: ActivatedRoute,
    private location: Location
    // public dialogRef: MatDialogRef<EditComponent>,
    // @Inject(MAT_DIALOG_DATA) public dlgData: { id: '' }
  ) {
    const id = this.route.snapshot.paramMap.get('id');
    if (id) {
      this.id = id;
    } else {
      // TODO: id为空
    }
  }

  get languageType() { return this.formGroup.get('languageType'); }
  get title() { return this.formGroup.get('title'); }
  get description() { return this.formGroup.get('description'); }
  get content() { return this.formGroup.get('content'); }
  get isPublic() { return this.formGroup.get('isPublic'); }
  get isOriginal() { return this.formGroup.get('isOriginal'); }
  get tagIds() { return this.formGroup.get('tagIds'); }
  get catalogId() { return this.formGroup.get('catalogId'); }

  ngOnInit(): void {
    forkJoin([this.getDetail(), this.getTags(), this.getCatalogs()])
      .subscribe(_ => {
        this.initForm();
        this.initEditor();
        this.isLoading = false;
      });
  }
  initEditor(): void {
    this.editorConfig = {
      placeholder: '请添加图文信息提供证据，也可以直接从Word文档中复制',
      simpleUpload: {
        uploadUrl: '/api/Blog/upload',
        headers: {
          Authorization: 'Bearer ' + localStorage.getItem("accessToken")
        }
      },
      language: 'zh-cn'
    };
  }
  onReady(editor: any) {
    editor.ui.getEditableElement().parentElement.insertBefore(
      editor.ui.view.toolbar.element,
      editor.ui.getEditableElement()
    );
  }
  async getDetail(): Promise<void> {
    const res = await lastValueFrom(this.service.getDetail(this.id));
    if (res) {
      this.data = res;
      if (res.tags) {
        this.selectedTagIds = res.tags.map(t => t.id);
      }
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

  async getCatalogs(): Promise<void> {
    const res = await lastValueFrom(this.catalogSrv.getLeaf());
    if (res) {
      this.catalog = res;
    }
  }

  initForm(): void {
    this.formGroup = new FormGroup({
      languageType: new FormControl(this.data.languageType, []),
      title: new FormControl(this.data.title, [Validators.maxLength(100)]),
      description: new FormControl(this.data.description, [Validators.maxLength(300)]),
      content: new FormControl(this.data.content, [Validators.maxLength(10000)]),
      isPublic: new FormControl(this.data.isPublic, []),
      isOriginal: new FormControl(this.data.isOriginal, []),
      tagIds: new FormControl(this.selectedTagIds, []),
      catalogId: new FormControl<string>(this.data.catalog?.id!, [Validators.required])
    });
  }
  getValidatorMessage(type: string): string {
    switch (type) {
      case 'languageType':
        return this.languageType?.errors?.['required'] ? 'LanguageType必填' :
          this.languageType?.errors?.['minlength'] ? 'LanguageType长度最少位' :
            this.languageType?.errors?.['maxlength'] ? 'LanguageType长度最多位' : '';
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
  edit(): void {
    if (this.formGroup.valid) {
      this.updateData = this.formGroup.value as BlogUpdateDto;
      this.service.update(this.id, this.updateData)
        .subscribe({
          next: (res) => {
            if (res) {
              this.snb.open('修改成功');
              // this.dialogRef.close(res);
              this.router.navigate(['../../index'], { relativeTo: this.route });
            }
          },
          error: () => {
          }
        });
    }
  }

  back(): void {
    this.location.back();
  }

}
