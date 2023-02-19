import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { ComponentsModule } from './components/components.module';
import { AppRoutingModule } from './app-routing.module';
import { HomeModule } from './pages/home/home.module';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { MAT_SNACK_BAR_DEFAULT_OPTIONS } from '@angular/material/snack-bar';
import { CustomerHttpInterceptor } from './share/customer-http.interceptor';
import { SystemModule } from './pages/system/system.module';
import { LoginComponent } from './pages/system/login/login.component';
import { ShareModule } from './share/share.module';
import { AccountModule } from './pages/account/account.module';

@NgModule({
  declarations: [
    AppComponent,
    LoginComponent
  ],
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    HttpClientModule,
    AppRoutingModule,
    ComponentsModule,
    ShareModule,
    HomeModule,
    SystemModule,
    AccountModule
  ],
  providers: [
    { provide: MAT_SNACK_BAR_DEFAULT_OPTIONS, useValue: { duration: 2500 } },
    { provide: HTTP_INTERCEPTORS, useClass: CustomerHttpInterceptor, multi: true },
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
