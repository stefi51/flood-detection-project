import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, FormControl } from '@angular/forms';
import { DataService } from 'src/app/services/data.service';
import { SensorData } from 'src/app/models/sensor-data.model';
import { MatDialog } from '@angular/material/dialog';
import { DataFormComponent } from '../../data-form/data-form.component';

@Component({
	selector: 'app-data-service',
	templateUrl: './data-service.component.html',
	styleUrls: ['./data-service.component.scss']
})
export class DataServiceComponent implements OnInit {

	dataServiceFormGroup: FormGroup;

	constructor(private fb: FormBuilder, private dataService: DataService, private dialog: MatDialog) { }

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
			waterFlow: Number.parseInt(this.dataServiceFormGroup.get("waterFlow").value),
			waterLevel: Number.parseInt(this.dataServiceFormGroup.get("waterLevel").value),
			rainfall: Number.parseInt(this.dataServiceFormGroup.get("rainfall").value),
			stationId: Number.parseInt(this.dataServiceFormGroup.get("stationId").value),
			measuredDateTime: new Date()
		}
		console.log(rawData);
		this.dataService.post(rawData).subscribe();
	}

	showRawData() {
		this.dataService.get().subscribe(x => {
			const dialogRef = this.dialog.open(DataFormComponent, {
				data: { items: x, type: 'data'}
			});
		});
	}

}
