import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { LoginComponent } from './components/login/login.component';
import { BitcoinLoggerMainComponent } from './components/bitcoin-logger-main/bitcoin-logger-main.component';

const routes: Routes = [
	{ path: "", component: LoginComponent },
	{ path: "login", component: LoginComponent },
	//{ path: "sudoku", component: BitcoinLoggerMainComponent, canActivate: [AuthenticationGuard] }
	{ path: "bitcoinloggermain", component: BitcoinLoggerMainComponent }
];

@NgModule({
	imports: [RouterModule.forRoot(routes)],
	exports: [RouterModule]
})
export class AppRoutingModule { }
