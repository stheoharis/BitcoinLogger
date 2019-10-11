import { Component, OnInit, ViewChild } from '@angular/core';
import { BitcoinLoggerService } from 'src/app/services/bitcoin-logger.service';
import { CurrencyPair } from 'src/app/models/currency-pair';
import { MatSort } from '@angular/material';
import { MatTableDataSource } from '@angular/material/table';
import { forkJoin } from 'rxjs';
import { SourcesService } from 'src/app/services/sources.service';
import { Source } from 'src/app/models/source';

@Component({
	selector: 'app-bitcoin-logger-main',
	templateUrl: './bitcoin-logger-main.component.html',
	styleUrls: ['./bitcoin-logger-main.component.scss']
})
export class BitcoinLoggerMainComponent implements OnInit {

	allSources: Source[];
	selectedSources: Source[];

	allCurrencyPairs: CurrencyPair[];
	filteredCurrencyPairs: CurrencyPair[];

	displayedColumns: string[] = ['source', 'price', 'timeStamp'];
	keyword: string;
	@ViewChild(MatSort, { static: true }) sort: MatSort;
	dataSource;

	constructor(private bitcoinLoggerService: BitcoinLoggerService, private sourcesService: SourcesService) { }

	ngOnInit() {

		forkJoin(
			this.bitcoinLoggerService.get(),
			this.sourcesService.get()
		).subscribe(([currencyPairs, sources]) => {

			console.log(currencyPairs);
			this.setDatasource(currencyPairs);
			this.allSources = sources;
			console.log(sources);
		})

	}

	private setDatasource(currencyPairs: CurrencyPair[]) {
		this.allCurrencyPairs = currencyPairs;
		this.filteredCurrencyPairs = JSON.parse(JSON.stringify(this.allCurrencyPairs));
		this.dataSource = new MatTableDataSource(this.filteredCurrencyPairs);
		this.dataSource.sort = this.sort;
	}

	getBySources() {
		console.log(this.selectedSources);
		this.bitcoinLoggerService.getBySources(this.selectedSources).subscribe(currencyPairs => {
			console.log(currencyPairs);
			this.setDatasource(currencyPairs);
		});
	}

	quickSearch() {

		this.filteredCurrencyPairs = this.allCurrencyPairs.filter(pair =>
			pair.price.toString().toLowerCase().trim().includes(this.keyword.toLowerCase().trim())
			|| pair.timeStamp.toString().toLowerCase().trim().includes(this.keyword.toLowerCase().trim())
			|| pair.source.name.toString().toLowerCase().trim().includes(this.keyword.toLowerCase().trim())
		);

		this.dataSource = new MatTableDataSource(this.filteredCurrencyPairs);
		this.dataSource.sort = this.sort;
	}


}
