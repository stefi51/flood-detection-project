import { NgModule } from '@angular/core';
import { ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations'

import { GoogleChartsModule } from 'angular-google-charts';

import { MatCardModule } from '@angular/material/card';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatSelectModule } from '@angular/material/select';
import { MatDialogModule } from '@angular/material/dialog';
import { MatDividerModule } from '@angular/material/divider';
import { MatFormFieldModule } from '@angular/material/form-field';

import { AppComponent } from './app.component';
import { GraphComponent } from './components/raw-data/graph/graph.component';
import { DataFormComponent } from './components/data-form/data-form.component';
import { NotificationComponent } from './components/notification/notification.component';
import { ControlPanelComponent } from './components/control-panel/control-panel.component';
import { DataServiceComponent } from './components/control-panel/data-service/data-service.component';
import { NotificationListComponent } from './components/notification-list/notification-list.component';
import { CommandServiceComponent } from './components/control-panel/command-service/command-service.component';
import { AnalyticsServiceComponent } from './components/control-panel/analytics-service/analytics-service.component';
import { RawDataComponent } from './components/raw-data/raw-data.component';

@NgModule({
	declarations: [
		AppComponent,
		NotificationListComponent,
		NotificationComponent,
		GraphComponent,
		ControlPanelComponent,
		DataServiceComponent,
		CommandServiceComponent,
		AnalyticsServiceComponent,
		DataFormComponent,
		RawDataComponent
	],
	imports: [
		BrowserModule,
		ReactiveFormsModule,
		HttpClientModule,
		GoogleChartsModule,
		BrowserAnimationsModule,
		MatButtonModule,
		MatInputModule,
		MatFormFieldModule,
		MatCardModule,
		MatSelectModule,
		MatDialogModule,
		MatDividerModule
	],
	providers: [],
	bootstrap: [AppComponent]
})
export class AppModule { }
