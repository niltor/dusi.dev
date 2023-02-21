import { Component, Inject, OnInit } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { BlogService } from 'src/app/share/services/blog.service';
import { Blog } from 'src/app/share/models/blog/blog.model';
import { BlogAddDto } from 'src/app/share/models/blog/blog-add-dto.model';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ActivatedRoute, Router } from '@angular/router';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { Location } from '@angular/common';
import * as ClassicEditor from 'ng-ckeditor5-classic';
import { environment } from 'src/environments/environment';
import { CKEditor5 } from '@ckeditor/ckeditor5-angular';
// import { OidcSecurityService } from 'angular-auth-oidc-client';
import { LanguageType } from 'src/app/share/models/enum/language-type.model';

@Component({
    selector: 'app-add',
    templateUrl: './add.component.html',
    styleUrls: ['./add.component.css']
})
export class AddComponent implements OnInit {
    public editorConfig!: CKEditor5.Config;
  public editor: CKEditor5.EditorConstructor = ClassicEditor;
  LanguageType = LanguageType;

    formGroup!: FormGroup;
    data = {} as BlogAddDto;
    isLoading = true;
    constructor(
        
    // private authService: OidcSecurityService,
        private service: BlogService,
        public snb: MatSnackBar,
        private router: Router,
        private route: ActivatedRoute,
        private location: Location
        // public dialogRef: MatDialogRef<AddComponent>,
        // @Inject(MAT_DIALOG_DATA) public dlgData: { id: '' }
    ) {

    }

    get translateTitle() { return this.formGroup.get('translateTitle'); }
    get translateContent() { return this.formGroup.get('translateContent'); }
    get languageType() { return this.formGroup.get('languageType'); }
    get title() { return this.formGroup.get('title'); }
    get description() { return this.formGroup.get('description'); }
    get content() { return this.formGroup.get('content'); }
    get isPublic() { return this.formGroup.get('isPublic'); }
    get isOriginal() { return this.formGroup.get('isOriginal'); }
    get tags() { return this.formGroup.get('tags'); }


  ngOnInit(): void {
    this.initForm();
    this.initEditor();
    // TODO:获取其他相关数据后设置加载状态
    this.isLoading = false;
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
  initForm(): void {
    this.formGroup = new FormGroup({
      translateTitle: new FormControl(null, []),
      translateContent: new FormControl(null, [Validators.maxLength(12000)]),
      languageType: new FormControl(null, []),
      title: new FormControl(null, [Validators.required,Validators.maxLength(100)]),
      description: new FormControl(null, [Validators.maxLength(300)]),
      content: new FormControl(null, [Validators.required,Validators.maxLength(10000)]),
      isPublic: new FormControl(null, []),
      isOriginal: new FormControl(null, []),
      tags: new FormControl(null, []),

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
      case 'isOriginal':
        return this.isOriginal?.errors?.['required'] ? 'IsOriginal必填' :
          this.isOriginal?.errors?.['minlength'] ? 'IsOriginal长度最少位' :
            this.isOriginal?.errors?.['maxlength'] ? 'IsOriginal长度最多位' : '';
      case 'tags':
        return this.tags?.errors?.['required'] ? 'Tags必填' :
          this.tags?.errors?.['minlength'] ? 'Tags长度最少位' :
            this.tags?.errors?.['maxlength'] ? 'Tags长度最多位' : '';

      default:
    return '';
    }
  }

  add(): void {
    if(this.formGroup.valid) {
    const data = this.formGroup.value as BlogAddDto;
    this.data = { ...data, ...this.data };
    this.service.add(this.data)
      .subscribe({
        next: (res) => {
          if (res) {
            this.snb.open('添加成功');
            // this.dialogRef.close(res);
            this.router.navigate(['../index'], { relativeTo: this.route });
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
