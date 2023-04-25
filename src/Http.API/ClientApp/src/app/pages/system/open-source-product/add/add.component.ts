import { Component, Inject, OnInit } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { OpenSourceProductService } from 'src/app/share/admin/services/open-source-product.service';
import { OpenSourceProductAddDto } from 'src/app/share/admin/models/open-source-product/open-source-product-add-dto.model';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ActivatedRoute, Router } from '@angular/router';
import { Location } from '@angular/common';
import { ENTER, COMMA } from '@angular/cdk/keycodes';
import { MatChipEditedEvent, MatChipInputEvent } from '@angular/material/chips';
import { TagsAddDto } from 'src/app/share/client/models/tags/tags-add-dto.model';
import { CommonColors } from 'src/app/share/const';

@Component({
  selector: 'app-add',
  templateUrl: './add.component.html',
  styleUrls: ['./add.component.css']
})
export class AddComponent implements OnInit {

  formGroup!: FormGroup;
  data = {} as OpenSourceProductAddDto;
  isLoading = false;
  isProcessing = false;
  readonly separatorKeysCodes = [ENTER, COMMA] as const;
  addOnBlur = true;
  tagChips: string[] = [];
  constructor(

    private service: OpenSourceProductService,
    public snb: MatSnackBar,
    private router: Router,
    private route: ActivatedRoute,
    private location: Location
    // public dialogRef: MatDialogRef<AddComponent>,
    // @Inject(MAT_DIALOG_DATA) public dlgData: { id: '' }
  ) {

  }

  get title() { return this.formGroup.get('title'); }
  get projectUrl() { return this.formGroup.get('projectUrl'); }
  get description() { return this.formGroup.get('description'); }
  get thumbnail() { return this.formGroup.get('thumbnail'); }
  get authors() { return this.formGroup.get('authors'); }
  get tags() { return this.formGroup.get('tags'); }


  ngOnInit(): void {
    this.initForm();
  }

  initForm(): void {
    this.formGroup = new FormGroup({
      title: new FormControl(null, [Validators.required, Validators.maxLength(100)]),
      projectUrl: new FormControl(null, [Validators.required, Validators.maxLength(200)]),
      description: new FormControl(null, [Validators.required, Validators.maxLength(500)]),
      thumbnail: new FormControl(null, [Validators.maxLength(200)]),
      authors: new FormControl(null, [Validators.maxLength(200)]),
      tags: new FormControl(null, [Validators.maxLength(300)]),

    });
  }
  getValidatorMessage(type: string): string {
    switch (type) {
      case 'title':
        return this.title?.errors?.['required'] ? 'Title必填' :
          this.title?.errors?.['minlength'] ? 'Title长度最少位' :
            this.title?.errors?.['maxlength'] ? 'Title长度最多100位' : '';
      case 'projectUrl':
        return this.projectUrl?.errors?.['required'] ? 'ProjectUrl必填' :
          this.projectUrl?.errors?.['minlength'] ? 'ProjectUrl长度最少位' :
            this.projectUrl?.errors?.['maxlength'] ? 'ProjectUrl长度最多200位' : '';
      case 'description':
        return this.description?.errors?.['required'] ? 'Description必填' :
          this.description?.errors?.['minlength'] ? 'Description长度最少位' :
            this.description?.errors?.['maxlength'] ? 'Description长度最多500位' : '';
      case 'thumbnail':
        return this.thumbnail?.errors?.['required'] ? 'Thumbnail必填' :
          this.thumbnail?.errors?.['minlength'] ? 'Thumbnail长度最少位' :
            this.thumbnail?.errors?.['maxlength'] ? 'Thumbnail长度最多200位' : '';
      case 'authors':
        return this.authors?.errors?.['required'] ? 'Authors必填' :
          this.authors?.errors?.['minlength'] ? 'Authors长度最少位' :
            this.authors?.errors?.['maxlength'] ? 'Authors长度最多200位' : '';
      case 'tags':
        return this.tags?.errors?.['required'] ? 'Tags必填' :
          this.tags?.errors?.['minlength'] ? 'Tags长度最少位' :
            this.tags?.errors?.['maxlength'] ? 'Tags长度最多300位' : '';

      default:
        return '';
    }
  }

  add(): void {
    if (this.formGroup.valid) {
      this.isProcessing = true;
      const data = this.formGroup.value as OpenSourceProductAddDto;
      data.tags = this.tagChips.join(',');
      this.service.add(data)
        .subscribe({
          next: (res) => {
            if (res) {
              this.snb.open('添加成功');
              // this.dialogRef.close(res);
              this.router.navigate(['../index'], { relativeTo: this.route });
            }
          },
          error: (error) => {
            this.snb.open(error.detail);
            this.isLoading = false;
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
  editTag(tag: string, event: MatChipEditedEvent) {
    const value = event.value.trim();
    // Remove fruit if it no longer has a name
    if (!value) {
      this.remove(tag);
      return;
    }
    // Edit existing fruit
    const index = this.tagChips.indexOf(tag);
    if (index >= 0) {
      this.tagChips[index] = value;
    }
  }
  addTag(event: MatChipInputEvent): void {
    const value = (event.value || '').trim();
    if (value && this.tagChips.indexOf(value) < 0) {
      this.tagChips.push(value);
    }
    event.chipInput!.clear();
  }

  remove(tag: string): void {
    const index = this.tagChips.indexOf(tag);

    if (index >= 0) {
      this.tagChips.splice(index, 1);
    }
  }
}
