import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, FormControl } from '@angular/forms';
import { RefinedData } from 'src/app/models/refined-data.model';
import { AnalyticsService } from 'src/app/services/analytics.service';

@Component({
	selector: 'app-analytics-service',
	templateUrl: './analytics-service.component.html',
	styleUrls: ['./analytics-service.component.scss']
})
export class AnalyticsServiceComponent implements OnInit {

	eventTypes = [ "Warn", "Alarm" ];

	analyticsServiceFormGroup: FormGroup;

	constructor(private fb: FormBuilder, private analyticsService: AnalyticsService) { }

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
			waterFlow: this.analyticsServiceFormGroup.get("waterFlow").value,
			waterLevel: this.analyticsServiceFormGroup.get("waterLevel").value,
			rainfall: this.analyticsServiceFormGroup.get("rainfall").value,
			stationId: this.analyticsServiceFormGroup.get("stationId").value,
			measuredDateTime: Date.now(),
			analyzedDataTime: Date.now(),
			analyzedEventType: this.analyticsServiceFormGroup.get("eventType").value === "Warn" ? 0 : 1
		}
		console.log(rawData);
		this.analyticsService.post("dummy").subscribe(console.log);
	}

	showRawData() {
		this.analyticsService.get().subscribe(console.log);
	}

}
