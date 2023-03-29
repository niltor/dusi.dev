import { Component, Inject, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ThirdNews } from 'src/app/share/admin/models/third-news/third-news.model';
import { ThirdNewsService } from 'src/app/share/admin/services/third-news.service';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ThirdNewsUpdateDto } from 'src/app/share/admin/models/third-news/third-news-update-dto.model';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { Location } from '@angular/common';
import { NewsSource } from 'src/app/share/admin/models/enum/news-source.model';
import { NewsType } from 'src/app/share/admin/models/enum/news-type.model';
import { TechType } from 'src/app/share/admin/models/enum/tech-type.model';

@Component({
  selector: 'app-edit',
  templateUrl: './edit.component.html',
  styleUrls: ['./edit.component.css']
})
export class EditComponent implements OnInit {
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


  get title() { return this.formGroup.get('title'); }
  get description() { return this.formGroup.get('description'); }
  get datePublished() { return this.formGroup.get('datePublished'); }
  get content() { return this.formGroup.get('content'); }
  get type() { return this.formGroup.get('type'); }
  get newsType() { return this.formGroup.get('newsType'); }
  get techType() { return this.formGroup.get('techType'); }


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
      title: new FormControl(this.data.title, [Validators.maxLength(200)]),
      description: new FormControl(this.data.description, [Validators.maxLength(5000)]),
      content: new FormControl(this.data.content, [Validators.maxLength(8000)]),
      type: new FormControl(this.data.type, []),
      newsType: new FormControl(this.data.newsType, []),
      techType: new FormControl(this.data.techType, []),

    });
  }
  getValidatorMessage(type: string): string {
    switch (type) {

      case 'title':
        return this.title?.errors?.['required'] ? 'Title必填' :
          this.title?.errors?.['minlength'] ? 'Title长度最少位' :
            this.title?.errors?.['maxlength'] ? 'Title长度最多200位' : '';
      case 'description':
        return this.description?.errors?.['required'] ? 'Description必填' :
          this.description?.errors?.['minlength'] ? 'Description长度最少位' :
            this.description?.errors?.['maxlength'] ? 'Description长度最多5000位' : '';

      case 'datePublished':
        return this.datePublished?.errors?.['required'] ? 'DatePublished必填' :
          this.datePublished?.errors?.['minlength'] ? 'DatePublished长度最少位' :
            this.datePublished?.errors?.['maxlength'] ? 'DatePublished长度最多位' : '';
      case 'content':
        return this.content?.errors?.['required'] ? 'Content必填' :
          this.content?.errors?.['minlength'] ? 'Content长度最少位' :
            this.content?.errors?.['maxlength'] ? 'Content长度最多8000位' : '';
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
    if (this.formGroup.valid) {
      this.updateData = this.formGroup.value as ThirdNewsUpdateDto;
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
