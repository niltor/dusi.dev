import { Component, Inject, OnInit } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { EntityModelService } from 'src/app/share/admin/services/entity-model.service';
import { EntityModel } from 'src/app/share/admin/models/entity-model/entity-model.model';
import { EntityModelAddDto } from 'src/app/share/admin/models/entity-model/entity-model-add-dto.model';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ActivatedRoute, Router } from '@angular/router';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { Location } from '@angular/common';
import * as ClassicEditor from 'ng-ckeditor5-classic';
import { environment } from 'src/environments/environment';
import { CKEditor5 } from '@ckeditor/ckeditor5-angular';
// import { OidcSecurityService } from 'angular-auth-oidc-client';
import { AccessModifier } from 'src/app/share/admin/models/enum/access-modifier.model';
import { CodeLanguage } from 'src/app/share/admin/models/enum/code-language.model';

@Component({
    selector: 'app-add',
    templateUrl: './add.component.html',
    styleUrls: ['./add.component.css']
})
export class AddComponent implements OnInit {
    public editorConfig!: CKEditor5.Config;
  public editor: CKEditor5.EditorConstructor = ClassicEditor;
  AccessModifier = AccessModifier;
CodeLanguage = CodeLanguage;

    formGroup!: FormGroup;
    data = {} as EntityModelAddDto;
    isLoading = true;
    constructor(
        
    // private authService: OidcSecurityService,
        private service: EntityModelService,
        public snb: MatSnackBar,
        private router: Router,
        private route: ActivatedRoute,
        private location: Location
        // public dialogRef: MatDialogRef<AddComponent>,
        // @Inject(MAT_DIALOG_DATA) public dlgData: { id: '' }
    ) {

    }

    get name() { return this.formGroup.get('name'); }
    get comment() { return this.formGroup.get('comment'); }
    get accessModifier() { return this.formGroup.get('accessModifier'); }
    get codeContent() { return this.formGroup.get('codeContent'); }
    get codeLanguage() { return this.formGroup.get('codeLanguage'); }
    get languageVersion() { return this.formGroup.get('languageVersion'); }


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
        uploadUrl: environment.uploadEditorFileUrl,
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
      name: new FormControl(null, [Validators.maxLength(60)]),
      comment: new FormControl(null, [Validators.maxLength(300)]),
      accessModifier: new FormControl(null, []),
      codeContent: new FormControl(null, [Validators.maxLength(8000)]),
      codeLanguage: new FormControl(null, []),
      languageVersion: new FormControl(null, []),

    });
  }
  getValidatorMessage(type: string): string {
    switch (type) {
      case 'name':
        return this.name?.errors?.['required'] ? 'Name必填' :
          this.name?.errors?.['minlength'] ? 'Name长度最少位' :
            this.name?.errors?.['maxlength'] ? 'Name长度最多60位' : '';
      case 'comment':
        return this.comment?.errors?.['required'] ? 'Comment必填' :
          this.comment?.errors?.['minlength'] ? 'Comment长度最少位' :
            this.comment?.errors?.['maxlength'] ? 'Comment长度最多300位' : '';
      case 'accessModifier':
        return this.accessModifier?.errors?.['required'] ? 'AccessModifier必填' :
          this.accessModifier?.errors?.['minlength'] ? 'AccessModifier长度最少位' :
            this.accessModifier?.errors?.['maxlength'] ? 'AccessModifier长度最多位' : '';
      case 'codeContent':
        return this.codeContent?.errors?.['required'] ? 'CodeContent必填' :
          this.codeContent?.errors?.['minlength'] ? 'CodeContent长度最少位' :
            this.codeContent?.errors?.['maxlength'] ? 'CodeContent长度最多8000位' : '';
      case 'codeLanguage':
        return this.codeLanguage?.errors?.['required'] ? 'CodeLanguage必填' :
          this.codeLanguage?.errors?.['minlength'] ? 'CodeLanguage长度最少位' :
            this.codeLanguage?.errors?.['maxlength'] ? 'CodeLanguage长度最多位' : '';
      case 'languageVersion':
        return this.languageVersion?.errors?.['required'] ? 'LanguageVersion必填' :
          this.languageVersion?.errors?.['minlength'] ? 'LanguageVersion长度最少位' :
            this.languageVersion?.errors?.['maxlength'] ? 'LanguageVersion长度最多位' : '';

      default:
    return '';
    }
  }

  add(): void {
    if(this.formGroup.valid) {
    const data = this.formGroup.value as EntityModelAddDto;
    this.data = { ...data, ...this.data };
    this.service.add(this.data)
        .subscribe(res => {
            this.snb.open('添加成功');
            // this.dialogRef.close(res);
            this.router.navigate(['../index'],{relativeTo: this.route});
        });
    }
  }
  back(): void {
    this.location.back();
  }
}
