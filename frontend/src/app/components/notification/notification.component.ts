import { Component, OnInit, Inject } from '@angular/core';
import { RefinedData } from 'src/app/models/refined-data.model';
import { MAT_DIALOG_DATA } from '@angular/material/dialog';

@Component({
	selector: 'app-notification',
	templateUrl: './notification.component.html',
	styleUrls: ['./notification.component.scss']
})
export class NotificationComponent implements OnInit {

	invoked: boolean = false;

	constructor(@Inject(MAT_DIALOG_DATA) public data: RefinedData) { }

	ngOnInit(): void {
	}

	resolve() {

	}

	postpone() {

	}

}
