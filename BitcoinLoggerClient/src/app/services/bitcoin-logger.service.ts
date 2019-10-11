import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { Observable } from 'rxjs';
import { CurrencyPair } from '../models/currency-pair';
import { environment } from 'src/environments/environment';
import { Source } from '../models/source';

@Injectable({
	providedIn: 'root'
})
export class BitcoinLoggerService {

	private controllerName = "CurrencyPair";

	constructor(private httpClient: HttpClient) { }

	get(): Observable<CurrencyPair[]> {
		console.log(environment.apiUrl);
		return this.httpClient.get<CurrencyPair[]>(environment.apiUrl + this.controllerName);
	}

	getBySources(sources: Source[]): Observable<CurrencyPair[]> {
		console.log(environment.apiUrl);
		return this.httpClient.post<CurrencyPair[]>(environment.apiUrl + this.controllerName + "/GetBySources", sources);
	}

}
