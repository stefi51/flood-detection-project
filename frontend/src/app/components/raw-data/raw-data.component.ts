import { Component, OnInit } from '@angular/core';

import { DataService } from 'src/app/services/data.service';

@Component({
	selector: 'app-raw-data',
	templateUrl: './raw-data.component.html',
	styleUrls: ['./raw-data.component.scss']
})
export class RawDataComponent implements OnInit {

	data = []
	options = { year: 'numeric', month: 'long', day: 'numeric' };
	columns: string[] = ['Measured time', 'Rainfall', 'Water Level', 'Water flow'];

	constructor(private dataService: DataService) {}

	ngOnInit(): void {
		setInterval( () => this.dataService.get().subscribe(x => {
			this.data = x.map(y => [new Date(y.measuredDateTime).toLocaleDateString('en-US', this.options), y.rainfall, y.waterLevel, y.waterFlow]);
		}), 10000);
	}

}
