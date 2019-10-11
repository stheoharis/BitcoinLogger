import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Source } from '../models/source';
import { environment } from 'src/environments/environment';

@Injectable({
	providedIn: 'root'
})
export class SourcesService {

	private controllerName = "Source";

	constructor(private httpClient: HttpClient) { }

	get(): Observable<Source[]> {
		console.log(environment.apiUrl);
		return this.httpClient.get<Source[]>(environment.apiUrl + this.controllerName);
	}


}
