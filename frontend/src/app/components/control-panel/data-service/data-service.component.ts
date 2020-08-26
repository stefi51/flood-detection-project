import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, FormControl } from '@angular/forms';
import { DataService } from 'src/app/services/data.service';
import { SensorData } from 'src/app/models/sensor-data.model';

@Component({
	selector: 'app-data-service',
	templateUrl: './data-service.component.html',
	styleUrls: ['./data-service.component.scss']
})
export class DataServiceComponent implements OnInit {

	dataServiceFormGroup: FormGroup;

	constructor(private fb: FormBuilder, private dataService: DataService) { }

	ngOnInit(): void {
		this.dataServiceFormGroup = this.fb.group({
			waterFlow: new FormControl(''),
			waterLevel: new FormControl(''),
			rainfall: new FormControl(''),
			stationId: new FormControl('1'),
		});
	}

	submitTestData() {
		const rawData: SensorData = {
			waterFlow: this.dataServiceFormGroup.get("waterFlow").value,
			waterLevel: this.dataServiceFormGroup.get("waterLevel").value,
			rainfall: this.dataServiceFormGroup.get("rainfall").value,
			stationId: this.dataServiceFormGroup.get("stationId").value,
			measuredDateTime: new Date()
		}
		console.log(rawData);
		this.dataService.post("dummy").subscribe(console.log);
	}

	showRawData() {
		this.dataService.get().subscribe(console.log);
	}

}
