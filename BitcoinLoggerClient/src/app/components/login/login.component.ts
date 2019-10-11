import { Component, OnInit } from '@angular/core';
import { AuthenticationService } from 'src/app/services/authentication.service';
import { User } from 'src/app/models/user';
import { UserSession } from 'src/app/models/user-session';
import { Router } from '@angular/router';

@Component({
	selector: 'app-login',
	templateUrl: './login.component.html',
	styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {

	user: User = new User();
	userSession: UserSession;
	error: string;

	constructor(private authenticationService: AuthenticationService, private router: Router) { }

	ngOnInit() {
	}

	Login(user: User) {
		this.error = "";
		this.authenticationService.Login(user).subscribe(response => {
			this.userSession = response;
			this.router.navigate(["/bitcoinloggermain"]);
			//routerLink="/bitcoinloggermain"

		}, error => {
			console.log(error);
			if (error.error) {
				this.error = error.error
			}
		});
	}

}
