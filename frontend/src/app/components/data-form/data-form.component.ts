import { Component, OnInit, Inject } from '@angular/core';
import { MAT_DIALOG_DATA } from '@angular/material/dialog';

@Component({
	selector: 'app-data-form',
	templateUrl: './data-form.component.html',
	styleUrls: ['./data-form.component.scss']
})
export class DataFormComponent implements OnInit {

	constructor(@Inject(MAT_DIALOG_DATA) public data: any) { }

	ngOnInit(): void {
	}

}
