import { Injectable } from '@angular/core';
import { User } from '../models/user';
import { Observable } from 'rxjs';
import { UserSession } from '../models/user-session';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';

@Injectable({
	providedIn: 'root'
})
export class AuthenticationService {

	private controllerName = "Authentication";
	private thisSession: UserSession;

	constructor(private httpClient: HttpClient) { }

	public Login(user: User): Observable<UserSession> {
		return this.httpClient.post<UserSession>(environment.apiUrl + this.controllerName + "/login", user);
	}

	get session() {
		return this.thisSession;
	}
	set session(userSession: UserSession) {
		this.thisSession = userSession;
	}

}
