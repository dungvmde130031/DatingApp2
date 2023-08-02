import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NgxGalleryModule } from '@kolkov/ngx-gallery';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
import { ToastrModule } from 'ngx-toastr';
import { TabsModule } from 'ngx-bootstrap/tabs';
import { NgxSpinnerModule } from "ngx-spinner";
import { FileUploadModule } from 'ng2-file-upload';
import { FileSelectDirective } from 'ng2-file-upload';
import { BsDatepickerModule } from 'ngx-bootstrap/datepicker';



@NgModule({
  declarations: [],
  providers: [
    FileSelectDirective
  ],
  imports: [
    CommonModule,
    BsDropdownModule.forRoot(),
    TabsModule.forRoot(),
    ToastrModule.forRoot({
      positionClass: 'toast-bottom-right'
    }),
    NgxGalleryModule,
    NgxSpinnerModule.forRoot({
      type: 'pacman'
    }),
    FileUploadModule,
    BsDatepickerModule.forRoot()
  ],
  exports: [
    BsDropdownModule,
    ToastrModule,
    NgxGalleryModule,
    TabsModule,
    NgxSpinnerModule,
    FileUploadModule,
    BsDatepickerModule
  ]
})
export class SharedModule { }
