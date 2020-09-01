import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, Subject } from 'rxjs';
import { SensorData } from '../models/sensor-data.model';

@Injectable({
	providedIn: 'root'
})
export class DataService {

	constructor(private http: HttpClient) {
	}

	post(data): Observable<any> {
		return this.http.post("http://localhost:5001/raw-data", data);
	}

	get(): Observable<any> {
		return this.http.get("http://localhost:5001/raw-data");
	}
}
