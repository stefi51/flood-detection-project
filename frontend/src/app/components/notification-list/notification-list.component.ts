import { Component, OnInit } from '@angular/core';
import { RefinedData } from 'src/app/models/refined-data.model';
import { AnalyticsService } from 'src/app/services/analytics.service';
import { MatDialog } from '@angular/material/dialog';
import { NotificationComponent } from '../notification/notification.component';

@Component({
	selector: 'app-notification-list',
	templateUrl: './notification-list.component.html',
	styleUrls: ['./notification-list.component.scss']
})
export class NotificationListComponent implements OnInit {

	notificationList: RefinedData[] = [];

	constructor(private analyticsService: AnalyticsService, private dialog: MatDialog) { }

	ngOnInit(): void {
		this.analyticsService.refinedDataSubject.subscribe((x: RefinedData) =>
			this.notificationList.push(x)
		);
	}

	showNotification(index: number) {
		const dialogRef = this.dialog.open(NotificationComponent, {
			data: this.notificationList[index]
		});

		dialogRef.afterClosed().subscribe(invoked => {
			if(invoked)
				this.notificationList.splice(index, 1);
		})
	}

}
