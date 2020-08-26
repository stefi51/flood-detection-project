import { Component, OnInit, Input } from '@angular/core';

@Component({
	selector: 'app-graph',
	templateUrl: './graph.component.html',
	styleUrls: ['./graph.component.scss']
})
export class GraphComponent implements OnInit {

	@Input() title: string;
	@Input() columns: string[];
	@Input() data: any[];
	
	constructor() { }

	ngOnInit(): void {

	}
}
