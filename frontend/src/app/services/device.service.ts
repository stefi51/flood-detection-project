import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
	providedIn: 'root'
})
export class DeviceService {

	constructor(private http: HttpClient) { }

	post(data): Observable<any> {
		return this.http.post("http://localhost:5001/setperiodtime", data);
	}

	get(): Observable<any> {
		return this.http.get("http://localhost:5001/getlivedata");
	}
}
