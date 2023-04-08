import { Component, Inject, OnInit } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { EntityModelService } from 'src/app/share/client/services/entity-model.service';
import { EntityModelAddDto } from 'src/app/share/client/models/entity-model/entity-model-add-dto.model';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ActivatedRoute, Router } from '@angular/router';
import { Location } from '@angular/common';
import { AccessModifier } from 'src/app/share/client/models/enum/access-modifier.model';
import { CodeLanguage } from 'src/app/share/client/models/enum/code-language.model';
import { MarkdownService } from 'ngx-markdown';
import { EntityLibraryService } from 'src/app/share/client/services/entity-library.service';
import { EntityLibraryItemDto } from 'src/app/share/client/models/entity-library/entity-library-item-dto.model';
import { MatSelectChange } from '@angular/material/select';

@Component({
  selector: 'app-add',
  templateUrl: './add.component.html',
  styleUrls: ['./add.component.css']
})
export class AddComponent implements OnInit {
  AccessModifier = AccessModifier;
  CodeLanguage = CodeLanguage;
  isPreview = false;
  isSplitView = false;
  formGroup!: FormGroup;
  data = {} as EntityModelAddDto;
  libs: EntityLibraryItemDto[] = [];
  isLoading = true;
  isProcessing = false;
  constructor(
    // private authService: OidcSecurityService,
    private service: EntityModelService,
    private libSrv: EntityLibraryService,
    public snb: MatSnackBar,
    private router: Router,
    private route: ActivatedRoute,
    private location: Location,
    private markdownService: MarkdownService,
    // public dialogRef: MatDialogRef<AddComponent>,
    // @Inject(MAT_DIALOG_DATA) public dlgData: { id: '' }
  ) {

  }

  get name() { return this.formGroup.get('name'); }
  get comment() { return this.formGroup.get('comment'); }
  get codeContent() { return this.formGroup.get('codeContent'); }
  get codeLanguage() { return this.formGroup.get('codeLanguage'); }
  get languageVersion() { return this.formGroup.get('languageVersion'); }
  get entityLibraryId() { return this.formGroup.get('entityLibraryId'); }

  ngOnInit(): void {
    this.initForm();
    this.getLibs();
  }
  getLibs(): void {
    this.libSrv.filter({
      pageIndex: 1,
      pageSize: 50
    }).subscribe({
      next: (res) => {
        if (res) {
          this.libs = res.data!;
        } else {
        }
      },
      error: (error) => {
        this.snb.open(error.detail);
      },
      complete: () => {
        this.isLoading = false;
      }
    });
  }

  initForm(): void {
    this.formGroup = new FormGroup({
      name: new FormControl(null, [Validators.required, Validators.maxLength(60)]),
      comment: new FormControl(null, [Validators.required, Validators.maxLength(300)]),
      codeContent: new FormControl('```csharp\n\n```', [Validators.maxLength(8000)]),
      codeLanguage: new FormControl(CodeLanguage.Csharp, [Validators.required]),
      languageVersion: new FormControl('latest', [Validators.maxLength(20)]),
      entityLibraryId: new FormControl(null, [Validators.required]),
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
            this.languageVersion?.errors?.['maxlength'] ? 'LanguageVersion长度最多20位' : '';
      case 'entityLibraryId':
        return this.languageVersion?.errors?.['required'] ? '必须选择所属实体库' : '';
      default:
        return '';
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
  changeLanuage(event: MatSelectChange): void {
  }

  add(): void {
    if (this.formGroup.valid) {
      this.isProcessing = true;
      const data = this.formGroup.value as EntityModelAddDto;
      this.data = { ...data, ...this.data };
      this.service.add(this.data)
        .subscribe({
          next: (res) => {
            if (res) {
              this.snb.open('添加成功');
              this.router.navigate(['../index'], { relativeTo: this.route });
            }
          },
          error: (error) => {
            this.snb.open(error.detail);
          },
          complete: () => {
            this.isProcessing = false;
          }
        });
    } else {
      this.snb.open('表单验证不通过，请检查填写的内容!');
    }
  }
  back(): void {
    this.location.back();
  }
}
