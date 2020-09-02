import { Component, OnInit, Inject } from '@angular/core';
import { MAT_DIALOG_DATA } from '@angular/material/dialog';

@Component({
	selector: 'app-data-form',
	templateUrl: './data-form.component.html',
	styleUrls: ['./data-form.component.scss']
})
export class DataFormComponent implements OnInit {

	items: any;
	type: string;
	dataColumns: string[] = ['Water level', 'Water flow', 'Rainfall', 'Measured time'];
	commandColumns: string[] = ['Command name', 'Endpoint', 'Rest', 'Gateway'];
	analyticsColumns: string[] = ['Water level', 'Water flow', 'Rainfall', 'Measured time', 'Analyzed time', 'Event type'];

	constructor(@Inject(MAT_DIALOG_DATA) public data: any) {
		this.items = data.items;
		this.type = data.type;
	}

	ngOnInit(): void {
	}

	getEventName(data: number): string {
		return data === 0 ? 'Warn' : 'Alarm';
	}
}
