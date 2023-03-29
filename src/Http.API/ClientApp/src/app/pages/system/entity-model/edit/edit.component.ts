import { Component, Inject, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { EntityModel } from 'src/app/share/admin/models/entity-model/entity-model.model';
import { EntityModelService } from 'src/app/share/admin/services/entity-model.service';
import { MatSnackBar } from '@angular/material/snack-bar';
import { EntityModelUpdateDto } from 'src/app/share/admin/models/entity-model/entity-model-update-dto.model';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { Location } from '@angular/common';
import { AccessModifier } from 'src/app/share/admin/models/enum/access-modifier.model';
import { CodeLanguage } from 'src/app/share/admin/models/enum/code-language.model';

@Component({
  selector: 'app-edit',
  templateUrl: './edit.component.html',
  styleUrls: ['./edit.component.css']
})
export class EditComponent implements OnInit {
  AccessModifier = AccessModifier;
  CodeLanguage = CodeLanguage;
  id!: string;
  isLoading = true;
  data = {} as EntityModel;
  updateData = {} as EntityModelUpdateDto;
  formGroup!: FormGroup;
  constructor(

    // private authService: OidcSecurityService,
    private service: EntityModelService,
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

  get name() { return this.formGroup.get('name'); }
  get comment() { return this.formGroup.get('comment'); }
  get accessModifier() { return this.formGroup.get('accessModifier'); }
  get codeContent() { return this.formGroup.get('codeContent'); }
  get codeLanguage() { return this.formGroup.get('codeLanguage'); }
  get languageVersion() { return this.formGroup.get('languageVersion'); }


  ngOnInit(): void {
    this.getDetail();
    // TODO:等待数据加载完成
    // this.isLoading = false;
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
      name: new FormControl(this.data.name, [Validators.maxLength(60)]),
      comment: new FormControl(this.data.comment, [Validators.maxLength(300)]),
      accessModifier: new FormControl(this.data.accessModifier, []),
      codeContent: new FormControl(this.data.codeContent, [Validators.maxLength(8000)]),
      codeLanguage: new FormControl(this.data.codeLanguage, []),
      languageVersion: new FormControl(this.data.languageVersion, []),

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
  edit(): void {
    if (this.formGroup.valid) {
      this.updateData = this.formGroup.value as EntityModelUpdateDto;
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
