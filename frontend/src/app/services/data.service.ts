import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, Subject } from 'rxjs';
import { SensorData } from '../models/sensor-data.model';

@Injectable({
	providedIn: 'root'
})
export class DataService {

	rawDataSubject: Subject<SensorData> = new Subject();

	constructor(private http: HttpClient) {
		setInterval(() => {
			const data: SensorData = {
				waterFlow: Math.random() * 10,
				waterLevel: Math.random() * 10,
				rainfall: Math.random() * 10,
				stationId: 4,
				measuredDateTime: new Date()
			}
			this.rawDataSubject.next(data);
		}, 5000);
	}

	post(data): Observable<any> {
		return this.http.post("dataurl",data);
	}

	get(): Observable<any> {
		return this.http.get("dataurl");
	}
}
