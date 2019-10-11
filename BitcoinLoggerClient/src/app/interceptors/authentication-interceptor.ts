import { Injectable } from '@angular/core';
import { HttpInterceptor, HttpRequest, HttpHandler, HttpEvent, HttpErrorResponse, HttpHeaders } from '@angular/common/http';
import { Observable, of, empty, throwError } from 'rxjs';
import { AuthenticationService } from '../services/authentication.service';
import { map, catchError, retry } from 'rxjs/operators';
import { Router } from '@angular/router';
import { UserSession } from '../models/user-session';
import { ServerError } from '../models/server-error';

@Injectable({
	providedIn: 'root'
})
export class AuthenticationInterceptorService implements HttpInterceptor {

	constructor(private authenticationService: AuthenticationService, private router: Router) { }

	intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {

		let session: UserSession = this.authenticationService.session;
		let thisHeaders: HttpHeaders;
		if (session && session.sessionKey) {
			thisHeaders = new HttpHeaders({
				'Authorization': 'Bearer ' + session.sessionKey,
				'Content-Type': 'application/json'
			});
		}


		request = request.clone({ headers: thisHeaders });

		console.log("========================")
		console.log("INTERCEPTOR");
		console.log(request);
		console.log("========================")

		return next.handle(request).pipe(
			catchError((err: HttpErrorResponse) => {

				let newError: ServerError = {
					status: err.status,
					stackTrace: "",
					message: undefined,
					originalError: err
				}

				if (err.status === 401) newError.message = "You have to log in to perform this action.";
				else if (err.status === 404) newError.message = "The provided url: " + err.url + " was not found.";
				else if (err.status === 403) newError.message = "This action is forbidden";
				else if (err.status === 422) newError.message = err.error; //BUSINESS ERROR
				else if (err.status === 500) newError.message = "Oops! You found a bug: " + err.error; //WE HAVE A BUG!	
				else if (err.message) newError.message = err.message;

				console.log("========================")
				console.log("INTERCEPTOR ERROR:")
				console.log(newError);
				console.log("========================")

				if (err.status == 401) this.router.navigate(['/login']);

				throw newError;
			})
		);
	}



}
