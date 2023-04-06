import { Component, Inject, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { EntityModel } from 'src/app/share/client/models/entity-model/entity-model.model';
import { EntityModelService } from 'src/app/share/client/services/entity-model.service';
import { MatSnackBar } from '@angular/material/snack-bar';
import { EntityModelUpdateDto } from 'src/app/share/client/models/entity-model/entity-model-update-dto.model';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { Location } from '@angular/common';
import { AccessModifier } from 'src/app/share/client/models/enum/access-modifier.model';
import { CodeLanguage } from 'src/app/share/client/models/enum/code-language.model';
import { EntityLibraryService } from 'src/app/share/client/services/entity-library.service';
import { EntityLibraryItemDto } from 'src/app/share/client/models/entity-library/entity-library-item-dto.model';
import { MarkdownService } from 'ngx-markdown';

@Component({
  selector: 'app-edit',
  templateUrl: './edit.component.html',
  styleUrls: ['./edit.component.css']
})
export class EditComponent implements OnInit {
  AccessModifier = AccessModifier;
  CodeLanguage = CodeLanguage;
  isPreview = false;
  isSplitView = false;
  id!: string;
  isLoading = true;
  isProcessing = false;
  data = {} as EntityModel;
  updateData = {} as EntityModelUpdateDto;
  libs: EntityLibraryItemDto[] = [];
  formGroup!: FormGroup;
  constructor(

    // private authService: OidcSecurityService,
    private service: EntityModelService,
    private libSrv: EntityLibraryService,
    private snb: MatSnackBar,
    private router: Router,
    private route: ActivatedRoute,
    private location: Location,
    private markdownService: MarkdownService
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
  get codeContent() { return this.formGroup.get('codeContent'); }
  get codeLanguage() { return this.formGroup.get('codeLanguage'); }
  get languageVersion() { return this.formGroup.get('languageVersion'); }
  get entityLibraryId() { return this.formGroup.get('entityLibraryId'); }


  ngOnInit(): void {
    this.getLibs();
    this.getDetail();
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
      codeContent: new FormControl(this.data.codeContent, [Validators.maxLength(8000)]),
      codeLanguage: new FormControl(this.data.codeLanguage, []),
      languageVersion: new FormControl(this.data.languageVersion, []),
      entityLibraryId: new FormControl(this.data.entityLibrary?.id, [Validators.required]),
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
  edit(): void {
    if (this.formGroup.valid) {
      this.isProcessing = true;
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
          },
          complete: () => {
            this.isProcessing = false;
          }
        });
    }
  }

  back(): void {
    this.location.back();
  }

}
