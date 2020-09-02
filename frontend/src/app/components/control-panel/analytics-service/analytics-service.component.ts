import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, FormControl } from '@angular/forms';
import { RefinedData } from 'src/app/models/refined-data.model';
import { AnalyticsService } from 'src/app/services/analytics.service';
import { DataFormComponent } from '../../data-form/data-form.component';
import { MatDialog } from '@angular/material/dialog';

@Component({
	selector: 'app-analytics-service',
	templateUrl: './analytics-service.component.html',
	styleUrls: ['./analytics-service.component.scss']
})
export class AnalyticsServiceComponent implements OnInit {

	eventTypes = [ "Warn", "Alarm" ];

	analyticsServiceFormGroup: FormGroup;

	constructor(private fb: FormBuilder, private analyticsService: AnalyticsService, private dialog: MatDialog) { }

	ngOnInit(): void {
		this.analyticsServiceFormGroup = this.fb.group({
			waterFlow: new FormControl(''),
			waterLevel: new FormControl(''),
			rainfall: new FormControl(''),
			stationId: new FormControl('1'),
			eventType: new FormControl('')
		});
	}

	submitTestData() {
		const rawData: RefinedData = {
			waterFlow: Number.parseInt(this.analyticsServiceFormGroup.get("waterFlow").value),
			waterLevel: Number.parseInt(this.analyticsServiceFormGroup.get("waterLevel").value),
			rainfall: Number.parseInt(this.analyticsServiceFormGroup.get("rainfall").value),
			stationId: Number.parseInt(this.analyticsServiceFormGroup.get("stationId").value),
			analyzedEventType: this.analyticsServiceFormGroup.get("eventType").value === "Warn" ? 0 : 1
		}
		console.log(rawData);
		this.analyticsService.post(rawData).subscribe(x => console.log(x));
	}

	showRefinedData() {
		this.analyticsService.get().subscribe(x => {
			console.log(x);
			const dialogRef = this.dialog.open(DataFormComponent, {
				data: { items: x, type: 'analytics'}
			});
		});
	}

}
