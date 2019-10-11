import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { FormsModule } from '@angular/forms';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { LoginComponent } from './components/login/login.component';
import { BitcoinLoggerMainComponent } from './components/bitcoin-logger-main/bitcoin-logger-main.component';
import { AngularMaterialModule } from './angular-material/angular-material.module';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { AuthenticationInterceptorService } from './interceptors/authentication-interceptor';

@NgModule({
	declarations: [
		AppComponent,
		LoginComponent,
		BitcoinLoggerMainComponent
	],
	imports: [
		BrowserModule,
		AppRoutingModule,
		HttpClientModule,
		AngularMaterialModule,
		BrowserAnimationsModule,
		FormsModule
	],
	providers: [
		{
			provide: HTTP_INTERCEPTORS,
			useClass: AuthenticationInterceptorService,
			multi: true
		}
	],
	bootstrap: [AppComponent]
})
export class AppModule { }
