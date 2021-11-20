import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClient, HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { SimpleNotificationsModule } from 'angular2-notifications';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { TranslateLoader, TranslateModule } from '@ngx-translate/core';
import { TranslateHttpLoader } from '@ngx-translate/http-loader';
import { AppRoutingModule } from './app-routing.module';
import { AuthModule } from './auth/auth.module';
import { JwtModule } from '@auth0/angular-jwt';
import { environment } from '../environments/environment';
import { SharedModule } from './shared/shared.module';
import { UnauthorizedPageComponent } from './unauthorized-page/unauthorized-page.component';
import { AuthorizeGuard } from './shared/guards/authorize.guard';
import { RoleGuard } from './shared/guards/role.guard';
import { AuthorizeInterceptor } from './shared/interceptors/authorize.interceptor';


@NgModule({
  declarations: [
    AppComponent,
    UnauthorizedPageComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    AuthModule,
    AppRoutingModule,
    SimpleNotificationsModule.forRoot(),
    JwtModule.forRoot({
      config: {
        tokenGetter: () => {
          return localStorage.getItem(environment.token);
        },
        allowedDomains: ["https://localhost:44357/"],
        disallowedRoutes: [],
      },
    }),
    BrowserAnimationsModule,
    SharedModule,
    TranslateModule.forRoot({
      loader: {
        provide: TranslateLoader,
        useFactory: (http: HttpClient) => new TranslateHttpLoader(http),
        deps: [HttpClient]
      }
    }),
  ],
  providers: [
    AuthorizeGuard,
    RoleGuard,
    { provide: HTTP_INTERCEPTORS, useClass: AuthorizeInterceptor, multi: true }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
