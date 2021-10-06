import { CommonModule, LocationStrategy, PathLocationStrategy } from "@angular/common";
import { HTTP_INTERCEPTORS } from "@angular/common/http";
import { NgModule } from "@angular/core";
import { BrowserModule } from "@angular/platform-browser";
import { AppRoutingModule } from "./app-routing.module";
import { AppComponent } from "./app.component";
import { AdminModule } from "./Content/admin/admin.module";
import { FooterComponent } from "./Content/Common/footer/footer.component";
import { HeaderComponent } from "./Content/Common/header/header.component";
import { NavigationComponent } from "./Content/Common/navigation/navigation.component";
import { PageNotFoundComponent } from "./Content/Common/page-not-found/page-not-found.component";
import { CustomerModule } from "./Content/customer/customer.module";
import { AppInterceptor } from "./Shared/Helper/app.interceptor";
import { BaseAPIService } from "./Shared/Helper/base-api.service";
import { LoaderService } from "./Shared/Helper/loader.service";
import { SharedModule } from "./Shared/Helper/shared/shared.module";
import { LoaderComponent } from './Content/Common/loader/loader.component';
import { DashboardModule } from "./Content/dashboard/dashboard.module";
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { HtmlComponent } from './Content/html/html.component';
import { LoginComponent } from './Content/Common/login/login.component';
import { ToastrModule } from "ngx-toastr";
import { timeout } from "rxjs/operators";

@NgModule({
  declarations: [
    AppComponent,
    HeaderComponent,
    FooterComponent,
    PageNotFoundComponent,
    NavigationComponent,
    LoaderComponent,
    HtmlComponent,
    LoginComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    SharedModule,
    AdminModule,
    CustomerModule,
    CommonModule,
    DashboardModule,
    BrowserAnimationsModule,
    ToastrModule.forRoot({
      timeOut:3000,
      closeButton:true,
      autoDismiss:true,
      maxOpened:5
    }),
  ],
  providers: [ BaseAPIService,

    LoaderService, { provide: HTTP_INTERCEPTORS, useClass: AppInterceptor, multi: true }, {provide: LocationStrategy, useClass: PathLocationStrategy}],
  bootstrap: [AppComponent]
})
export class AppModule { }
