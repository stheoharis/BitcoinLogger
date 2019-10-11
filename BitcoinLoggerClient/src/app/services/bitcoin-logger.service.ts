import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { Observable } from 'rxjs';
import { CurrencyPair } from '../models/currency-pair';
import { environment } from 'src/environments/environment';

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

}
