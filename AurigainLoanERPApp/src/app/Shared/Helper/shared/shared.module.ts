
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HttpClientModule } from '@angular/common/http'
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatTableModule } from '@angular/material/table';
import { MatSortModule } from '@angular/material/sort';
import { MatPaginatorModule } from '@angular/material/paginator';
import { MatDialogModule } from '@angular/material/dialog';
import { MatSnackBarModule } from '@angular/material/snack-bar';
import { NgSelectModule } from '@ng-select/ng-select';
const CommonModules = [
  HttpClientModule,
  ReactiveFormsModule,
  FormsModule,
  
]

const InstalledModule = [
  MatDialogModule,
  MatSnackBarModule,
  MatTableModule,
  MatSortModule,
  MatPaginatorModule,
  MatSortModule,
  NgSelectModule
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
