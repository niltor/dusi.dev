import { Component, Inject, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Blog } from 'src/app/share/models/blog/blog.model';
import { BlogService } from 'src/app/share/services/blog.service';
import { MatSnackBar } from '@angular/material/snack-bar';
import { BlogUpdateDto } from 'src/app/share/models/blog/blog-update-dto.model';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { Location } from '@angular/common';
import * as ClassicEditor from 'ng-ckeditor5-classic';
import { environment } from 'src/environments/environment';
import { CKEditor5 } from '@ckeditor/ckeditor5-angular';
// import { OidcSecurityService } from 'angular-auth-oidc-client';
import { LanguageType } from 'src/app/share/models/enum/language-type.model';

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
  updateData = {} as BlogUpdateDto;
  formGroup!: FormGroup;
    constructor(
    
    // private authService: OidcSecurityService,
    private service: BlogService,
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

    get translateTitle() { return this.formGroup.get('translateTitle'); }
    get translateContent() { return this.formGroup.get('translateContent'); }
    get languageType() { return this.formGroup.get('languageType'); }
    get title() { return this.formGroup.get('title'); }
    get description() { return this.formGroup.get('description'); }
    get content() { return this.formGroup.get('content'); }
    get isPublic() { return this.formGroup.get('isPublic'); }
    get authors() { return this.formGroup.get('authors'); }
    get tags() { return this.formGroup.get('tags'); }


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
      translateTitle: new FormControl(this.data.translateTitle, []),
      translateContent: new FormControl(this.data.translateContent, [Validators.maxLength(12000)]),
      languageType: new FormControl(this.data.languageType, []),
      title: new FormControl(this.data.title, [Validators.maxLength(100)]),
      description: new FormControl(this.data.description, [Validators.maxLength(300)]),
      content: new FormControl(this.data.content, [Validators.maxLength(10000)]),
      isPublic: new FormControl(this.data.isPublic, []),
      authors: new FormControl(this.data.authors, [Validators.maxLength(200)]),
      tags: new FormControl(this.data.tags, []),

    });
  }
  getValidatorMessage(type: string): string {
    switch (type) {
      case 'translateTitle':
        return this.translateTitle?.errors?.['required'] ? 'TranslateTitle必填' :
          this.translateTitle?.errors?.['minlength'] ? 'TranslateTitle长度最少位' :
            this.translateTitle?.errors?.['maxlength'] ? 'TranslateTitle长度最多位' : '';
      case 'translateContent':
        return this.translateContent?.errors?.['required'] ? 'TranslateContent必填' :
          this.translateContent?.errors?.['minlength'] ? 'TranslateContent长度最少位' :
            this.translateContent?.errors?.['maxlength'] ? 'TranslateContent长度最多12000位' : '';
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
      case 'authors':
        return this.authors?.errors?.['required'] ? 'Authors必填' :
          this.authors?.errors?.['minlength'] ? 'Authors长度最少位' :
            this.authors?.errors?.['maxlength'] ? 'Authors长度最多200位' : '';
      case 'tags':
        return this.tags?.errors?.['required'] ? 'Tags必填' :
          this.tags?.errors?.['minlength'] ? 'Tags长度最少位' :
            this.tags?.errors?.['maxlength'] ? 'Tags长度最多位' : '';

      default:
        return '';
    }
  }
  edit(): void {
    if(this.formGroup.valid) {
      this.updateData = this.formGroup.value as BlogUpdateDto;
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
