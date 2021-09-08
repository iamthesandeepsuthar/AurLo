import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HttpClientModule } from '@angular/common/http'
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
const CommonModules = [
  HttpClientModule,
  ReactiveFormsModule,
 // FormsModule  //uncomment this after installing new module and remove form installedModule variable

]

const InstalledModule = [
  FormsModule
]




@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    CommonModules,
    InstalledModule
  ],
  exports: [
    CommonModules,
    InstalledModule
  ]
})
export class SharedModule { }
