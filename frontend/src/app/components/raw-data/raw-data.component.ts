import { Component, OnInit, OnDestroy } from '@angular/core';

import { Subscription } from 'rxjs';

import { DataService } from 'src/app/services/data.service';

@Component({
	selector: 'app-raw-data',
	templateUrl: './raw-data.component.html',
	styleUrls: ['./raw-data.component.scss']
})
export class RawDataComponent implements OnInit, OnDestroy {

	data = []
	options = { year: 'numeric', month: 'long', day: 'numeric' };
	columns: string[] = ['Measured time', 'Rainfall', 'Water Level', 'Water flow'];

	private rawDataSubscription: Subscription;

	constructor(private dataService: DataService) {}

	ngOnInit(): void {
		this.rawDataSubscription = this.dataService.rawDataSubject.subscribe(x => {
			this.data = [...this.data, [x.measuredDateTime.toLocaleDateString('en-US', this.options) , x.rainfall, x.waterLevel, x.waterFlow]];
		});
	}

	ngOnDestroy(): void {
		this.rawDataSubscription?.unsubscribe();
	}

}
