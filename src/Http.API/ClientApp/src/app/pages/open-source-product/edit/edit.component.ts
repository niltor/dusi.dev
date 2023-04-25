import { Component, Inject, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { OpenSourceProduct } from 'src/app/share/admin/models/open-source-product/open-source-product.model';
import { OpenSourceProductService } from 'src/app/share/admin/services/open-source-product.service';
import { MatSnackBar } from '@angular/material/snack-bar';
import { OpenSourceProductUpdateDto } from 'src/app/share/admin/models/open-source-product/open-source-product-update-dto.model';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { Location } from '@angular/common';

@Component({
  selector: 'app-edit',
  templateUrl: './edit.component.html',
  styleUrls: ['./edit.component.css']
})
export class EditComponent implements OnInit {
  
  id!: string;
  isLoading = true;
  isProcessing = false;
  data = {} as OpenSourceProduct;
  updateData = {} as OpenSourceProductUpdateDto;
  formGroup!: FormGroup;
    constructor(
    
    private service: OpenSourceProductService,
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
    get projectUrl() { return this.formGroup.get('projectUrl'); }
    get description() { return this.formGroup.get('description'); }
    get thumbnail() { return this.formGroup.get('thumbnail'); }
    get authors() { return this.formGroup.get('authors'); }
    get tags() { return this.formGroup.get('tags'); }


  ngOnInit(): void {
    this.getDetail();
    
    // TODO:等待数据加载完成
    // this.isLoading = false;
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
      title: new FormControl(this.data.title, [Validators.maxLength(100)]),
      projectUrl: new FormControl(this.data.projectUrl, [Validators.maxLength(200)]),
      description: new FormControl(this.data.description, [Validators.maxLength(500)]),
      thumbnail: new FormControl(this.data.thumbnail, [Validators.maxLength(200)]),
      authors: new FormControl(this.data.authors, [Validators.maxLength(200)]),
      tags: new FormControl(this.data.tags, [Validators.maxLength(300)]),

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
  edit(): void {
    if(this.formGroup.valid) {
      this.isProcessing = true;
      this.updateData = this.formGroup.value as OpenSourceProductUpdateDto;
      this.service.update(this.id, this.updateData)
        .subscribe({
          next: (res) => {
            if(res){
              this.snb.open('修改成功');
              // this.dialogRef.close(res);
              this.router.navigate(['../../index'], { relativeTo: this.route });
            }
          },
          error: (error) => {
            this.snb.open(error.detail);
          },
          complate: () => {
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
