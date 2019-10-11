import { Component, OnInit, ViewChild } from '@angular/core';
import { BitcoinLoggerService } from 'src/app/services/bitcoin-logger.service';
import { CurrencyPair } from 'src/app/models/currency-pair';
import { MatSort } from '@angular/material';
import { MatTableDataSource } from '@angular/material/table';

@Component({
	selector: 'app-bitcoin-logger-main',
	templateUrl: './bitcoin-logger-main.component.html',
	styleUrls: ['./bitcoin-logger-main.component.scss']
})
export class BitcoinLoggerMainComponent implements OnInit {

	currencyPairs: CurrencyPair[];
	filteredCurrencyPairs: CurrencyPair[];
	displayedColumns: string[] = ['source', 'price', 'timeStamp'];
	keyword: string;
	@ViewChild(MatSort, { static: true }) sort: MatSort;
	dataSource;

	constructor(private bitcoinLoggerService: BitcoinLoggerService) { }

	ngOnInit() {



		this.bitcoinLoggerService.get().subscribe(response => {
			console.log(response);
			this.currencyPairs = response;
			this.filteredCurrencyPairs = JSON.parse(JSON.stringify(this.currencyPairs));

			
			this.dataSource = new MatTableDataSource(this.filteredCurrencyPairs);
			this.dataSource.sort = this.sort;

		});
	}

	quickSearch() {

		this.filteredCurrencyPairs = this.currencyPairs.filter(pair =>
			pair.price.toString().toLowerCase().trim().includes(this.keyword.toLowerCase().trim())
			|| pair.timeStamp.toString().toLowerCase().trim().includes(this.keyword.toLowerCase().trim())
			|| pair.source.toString().toLowerCase().trim().includes(this.keyword.toLowerCase().trim())
		);
	}


}
