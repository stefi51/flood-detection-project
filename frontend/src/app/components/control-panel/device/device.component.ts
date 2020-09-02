import { Component, OnInit } from '@angular/core';
import { DeviceService } from 'src/app/services/device.service';
import { MatDialog } from '@angular/material/dialog';
import { FormControl, FormBuilder, FormGroup } from '@angular/forms';
import { DataFormComponent } from '../../data-form/data-form.component';

@Component({
	selector: 'app-device',
	templateUrl: './device.component.html',
	styleUrls: ['./device.component.scss']
})
export class DeviceComponent implements OnInit {

	deviceServiceFormGroup: FormGroup;

	constructor(private fb: FormBuilder, private deviceService: DeviceService, private dialog: MatDialog) { }

	ngOnInit(): void {
		this.deviceServiceFormGroup = this.fb.group({
			timestep: new FormControl()
		});
	}

	submitData() {
		const data: TimeStepValue = { timestep: Number.parseInt(this.deviceServiceFormGroup.get("timestep").value) };
		console.log(data);
		this.deviceService.post(data).subscribe();
	}

	showMetrics() {
		this.deviceService.get().subscribe(x => {
			const dialogRef = this.dialog.open(DataFormComponent, {
				data: { items: x, type: 'device'}
			});
		});
	}
}

export class TimeStepValue {
	timestep: number;
}

