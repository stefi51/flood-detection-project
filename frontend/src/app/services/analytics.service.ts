import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { Observable, Subject } from 'rxjs';
import * as signalR from "@aspnet/signalr";

import { RefinedData } from '../models/refined-data.model';
import { MatDialog } from '@angular/material/dialog';
import { AlertComponent } from '../components/alert/alert.component';

@Injectable({
	providedIn: 'root'
})
export class AnalyticsService {

	refinedDataSubject: Subject<RefinedData> = new Subject();
	private _hubConnection: signalR.HubConnection;

	constructor(private http: HttpClient, private dialog: MatDialog) {
		this._hubConnection = new signalR.HubConnectionBuilder()
			.withUrl("http://localhost:4000/notificationservice")
			.build();

		this._hubConnection
			.start()
			.then(() => console.log('Connection started'))
			.catch(err => console.log('Error while starting connection: ' + err));
		
		this.registerRefinedDataUpdate();
	}

	getRefinedData(): Observable<RefinedData[]> {
		return this.http.get<RefinedData[]>("http://localhost:5000/api/AnalyticsController");
	}

	private registerRefinedDataUpdate() {
		this._hubConnection.on('refinedDataUpdate', (data: RefinedData) => {
			this.refinedDataSubject.next(data);
			this.dialog.open(AlertComponent);
			setTimeout(() => {
				this.dialog.closeAll();
			}, 10000);
		});
	}

	post(data): Observable<any> {
		return this.http.post("http://localhost:5001/newsensordata", data);
	}

	get(): Observable<any> {
		return this.http.get("http://localhost:5001/getrefineddata");
	}
}
