import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';

import { Subscription } from 'rxjs';

import { RefinedData } from 'src/app/models/refined-data.model';
import { AnalyticsService } from 'src/app/services/analytics.service';
import { NotificationComponent } from '../notification/notification.component';

@Component({
	selector: 'app-notification-list',
	templateUrl: './notification-list.component.html',
	styleUrls: ['./notification-list.component.scss']
})
export class NotificationListComponent implements OnInit {

	notificationList: RefinedData[] = [];

	private refinedDataSubscription: Subscription;

	constructor(private analyticsService: AnalyticsService, private dialog: MatDialog) { }

	ngOnInit(): void {
		this.refinedDataSubscription = this.analyticsService.refinedDataSubject.subscribe((x: RefinedData) =>
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

	ngOnDestroy(): void {
		this.refinedDataSubscription?.unsubscribe();
	}

}
