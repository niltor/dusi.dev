import { Component, Inject, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ThirdNews } from 'src/app/share/admin/models/third-news/third-news.model';
import { ThirdNewsService } from 'src/app/share/admin/services/third-news.service';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ThirdNewsUpdateDto } from 'src/app/share/admin/models/third-news/third-news-update-dto.model';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { Location } from '@angular/common';
import * as ClassicEditor from 'ng-ckeditor5-classic';
import { environment } from 'src/environments/environment';
import { CKEditor5 } from '@ckeditor/ckeditor5-angular';
// import { OidcSecurityService } from 'angular-auth-oidc-client';
import { NewsSource } from 'src/app/share/admin/models/enum/news-source.model';
import { NewsType } from 'src/app/share/admin/models/enum/news-type.model';
import { TechType } from 'src/app/share/admin/models/enum/tech-type.model';

@Component({
  selector: 'app-edit',
  templateUrl: './edit.component.html',
  styleUrls: ['./edit.component.css']
})
export class EditComponent implements OnInit {
  public editorConfig!: CKEditor5.Config;
  public editor: CKEditor5.EditorConstructor = ClassicEditor;
  NewsSource = NewsSource;
NewsType = NewsType;
TechType = TechType;

  id!: string;
  isLoading = true;
  data = {} as ThirdNews;
  updateData = {} as ThirdNewsUpdateDto;
  formGroup!: FormGroup;
    constructor(
    
    // private authService: OidcSecurityService,
    private service: ThirdNewsService,
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

    get authorName() { return this.formGroup.get('authorName'); }
    get authorAvatar() { return this.formGroup.get('authorAvatar'); }
    get title() { return this.formGroup.get('title'); }
    get description() { return this.formGroup.get('description'); }
    get url() { return this.formGroup.get('url'); }
    get thumbnailUrl() { return this.formGroup.get('thumbnailUrl'); }
    get provider() { return this.formGroup.get('provider'); }
    get datePublished() { return this.formGroup.get('datePublished'); }
    get content() { return this.formGroup.get('content'); }
    get category() { return this.formGroup.get('category'); }
    get identityId() { return this.formGroup.get('identityId'); }
    get type() { return this.formGroup.get('type'); }
    get newsType() { return this.formGroup.get('newsType'); }
    get techType() { return this.formGroup.get('techType'); }


  ngOnInit(): void {
    this.getDetail();
    this.initEditor();
    // TODO:等待数据加载完成
    // this.isLoading = false;
  }
    initEditor(): void {
    this.editorConfig = {
      // placeholder: '请添加图文信息提供证据，也可以直接从Word文档中复制',
      simpleUpload: {
        uploadUrl: '',// set your api url like:environment.uploadEditorFileUrl,
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
  getDetail(): void {
    this.service.getDetail(this.id)
      .subscribe(res => {
        this.data = res;
        this.initForm();
        this.isLoading = false;
      }, error => {
        this.snb.open(error);
      })
  }

  initForm(): void {
    this.formGroup = new FormGroup({
      authorName: new FormControl(this.data.authorName, [Validators.maxLength(100)]),
      authorAvatar: new FormControl(this.data.authorAvatar, [Validators.maxLength(300)]),
      title: new FormControl(this.data.title, [Validators.maxLength(200)]),
      description: new FormControl(this.data.description, [Validators.maxLength(5000)]),
      url: new FormControl(this.data.url, [Validators.maxLength(300)]),
      thumbnailUrl: new FormControl(this.data.thumbnailUrl, [Validators.maxLength(300)]),
      provider: new FormControl(this.data.provider, [Validators.maxLength(50)]),
      datePublished: new FormControl(this.data.datePublished, []),
      content: new FormControl(this.data.content, [Validators.maxLength(8000)]),
      category: new FormControl(this.data.category, [Validators.maxLength(50)]),
      identityId: new FormControl(this.data.identityId, [Validators.maxLength(50)]),
      type: new FormControl(this.data.type, []),
      newsType: new FormControl(this.data.newsType, []),
      techType: new FormControl(this.data.techType, []),

    });
  }
  getValidatorMessage(type: string): string {
    switch (type) {
      case 'authorName':
        return this.authorName?.errors?.['required'] ? 'AuthorName必填' :
          this.authorName?.errors?.['minlength'] ? 'AuthorName长度最少位' :
            this.authorName?.errors?.['maxlength'] ? 'AuthorName长度最多100位' : '';
      case 'authorAvatar':
        return this.authorAvatar?.errors?.['required'] ? 'AuthorAvatar必填' :
          this.authorAvatar?.errors?.['minlength'] ? 'AuthorAvatar长度最少位' :
            this.authorAvatar?.errors?.['maxlength'] ? 'AuthorAvatar长度最多300位' : '';
      case 'title':
        return this.title?.errors?.['required'] ? 'Title必填' :
          this.title?.errors?.['minlength'] ? 'Title长度最少位' :
            this.title?.errors?.['maxlength'] ? 'Title长度最多200位' : '';
      case 'description':
        return this.description?.errors?.['required'] ? 'Description必填' :
          this.description?.errors?.['minlength'] ? 'Description长度最少位' :
            this.description?.errors?.['maxlength'] ? 'Description长度最多5000位' : '';
      case 'url':
        return this.url?.errors?.['required'] ? 'Url必填' :
          this.url?.errors?.['minlength'] ? 'Url长度最少位' :
            this.url?.errors?.['maxlength'] ? 'Url长度最多300位' : '';
      case 'thumbnailUrl':
        return this.thumbnailUrl?.errors?.['required'] ? 'ThumbnailUrl必填' :
          this.thumbnailUrl?.errors?.['minlength'] ? 'ThumbnailUrl长度最少位' :
            this.thumbnailUrl?.errors?.['maxlength'] ? 'ThumbnailUrl长度最多300位' : '';
      case 'provider':
        return this.provider?.errors?.['required'] ? 'Provider必填' :
          this.provider?.errors?.['minlength'] ? 'Provider长度最少位' :
            this.provider?.errors?.['maxlength'] ? 'Provider长度最多50位' : '';
      case 'datePublished':
        return this.datePublished?.errors?.['required'] ? 'DatePublished必填' :
          this.datePublished?.errors?.['minlength'] ? 'DatePublished长度最少位' :
            this.datePublished?.errors?.['maxlength'] ? 'DatePublished长度最多位' : '';
      case 'content':
        return this.content?.errors?.['required'] ? 'Content必填' :
          this.content?.errors?.['minlength'] ? 'Content长度最少位' :
            this.content?.errors?.['maxlength'] ? 'Content长度最多8000位' : '';
      case 'category':
        return this.category?.errors?.['required'] ? 'Category必填' :
          this.category?.errors?.['minlength'] ? 'Category长度最少位' :
            this.category?.errors?.['maxlength'] ? 'Category长度最多50位' : '';
      case 'identityId':
        return this.identityId?.errors?.['required'] ? 'IdentityId必填' :
          this.identityId?.errors?.['minlength'] ? 'IdentityId长度最少位' :
            this.identityId?.errors?.['maxlength'] ? 'IdentityId长度最多50位' : '';
      case 'type':
        return this.type?.errors?.['required'] ? 'Type必填' :
          this.type?.errors?.['minlength'] ? 'Type长度最少位' :
            this.type?.errors?.['maxlength'] ? 'Type长度最多位' : '';
      case 'newsType':
        return this.newsType?.errors?.['required'] ? 'NewsType必填' :
          this.newsType?.errors?.['minlength'] ? 'NewsType长度最少位' :
            this.newsType?.errors?.['maxlength'] ? 'NewsType长度最多位' : '';
      case 'techType':
        return this.techType?.errors?.['required'] ? 'TechType必填' :
          this.techType?.errors?.['minlength'] ? 'TechType长度最少位' :
            this.techType?.errors?.['maxlength'] ? 'TechType长度最多位' : '';

      default:
        return '';
    }
  }
  edit(): void {
    if(this.formGroup.valid) {
      this.updateData = this.formGroup.value as ThirdNewsUpdateDto;
      this.service.update(this.id, this.updateData)
        .subscribe({
          next: (res) => {
            if(res){
              this.snb.open('修改成功');
              // this.dialogRef.close(res);
              this.router.navigate(['../../index'], { relativeTo: this.route });
            }
          },
          error:()=>{
          }
        });
    }
  }

  back(): void {
    this.location.back();
  }

}
