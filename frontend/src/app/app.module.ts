import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';
import { ReactiveFormsModule } from '@angular/forms';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations'

import { MatButtonModule } from '@angular/material/button';
import { MatInputModule } from '@angular/material/input';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatCardModule } from '@angular/material/card';
import { MatSelectModule } from '@angular/material/select';
import { MatDialogModule } from '@angular/material/dialog';

import { AppComponent } from './app.component';
import { NotificationListComponent } from './components/notification-list/notification-list.component';
import { NotificationComponent } from './components/notification/notification.component';
import { GraphComponent } from './components/graph/graph.component';
import { ControlPanelComponent } from './components/control-panel/control-panel.component';
import { DataServiceComponent } from './components/control-panel/data-service/data-service.component';
import { CommandServiceComponent } from './components/control-panel/command-service/command-service.component';
import { AnalyticsServiceComponent } from './components/control-panel/analytics-service/analytics-service.component';
import { DataFormComponent } from './components/data-form/data-form.component';

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
		DataFormComponent
	],
	imports: [
		BrowserModule,
		ReactiveFormsModule,
		HttpClientModule,
		MatButtonModule,
		MatInputModule,
		MatFormFieldModule,
		BrowserAnimationsModule,
		MatCardModule,
		MatSelectModule,
		MatDialogModule
	],
	providers: [],
	bootstrap: [AppComponent]
})
export class AppModule { }
