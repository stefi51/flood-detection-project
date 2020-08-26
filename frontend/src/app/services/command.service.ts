import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
	providedIn: 'root'
})
export class CommandService {

	constructor(private http: HttpClient) { }

	post(data): Observable<any> {
		return this.http.post("dummy", data);
	}

	get(): Observable<any> {
		return this.http.get("dummy");
	}
}
