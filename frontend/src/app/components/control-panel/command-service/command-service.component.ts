import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { FormGroup, FormBuilder, FormControl } from '@angular/forms';
import { CommandService } from 'src/app/services/command.service';
import { Command } from 'src/app/models/command.model';
import { MatDialog } from '@angular/material/dialog';
import { DataFormComponent } from '../../data-form/data-form.component';

@Component({
	selector: 'app-command-service',
	templateUrl: './command-service.component.html',
	styleUrls: ['./command-service.component.scss']
})
export class CommandServiceComponent implements OnInit {

	@Input() stationId: number;
	@Output() invoked: EventEmitter<boolean> = new EventEmitter();

	commandServiceFormGroup: FormGroup;

	constructor(private fb: FormBuilder, private commandService: CommandService, private dialog: MatDialog) { }

	ngOnInit(): void {
		this.commandServiceFormGroup = this.fb.group({
			commandName: new FormControl(''),
			stationId: new FormControl(this.stationId),
			ammount: new FormControl(),
			commandType: new FormControl(),
			value: new FormControl()
		})
	}

	invokeCommand() {
		const dynamicPropertyName: string = `${this.commandServiceFormGroup.get("value").value}Water${this.commandServiceFormGroup.get("commandType").value}`;
		const commandData: Command = {
			name: this.commandServiceFormGroup.get("commandName").value,
			stationId: Number.parseInt(this.commandServiceFormGroup.get("stationId").value),
			[dynamicPropertyName]: Number.parseInt(this.commandServiceFormGroup.get("ammount").value)
		}

		const path: string = `${this.commandServiceFormGroup.get("value").value === "plus" ? "increase": "decrease"}water${this.commandServiceFormGroup.get("commandType").value}`;
		console.log(path.toLowerCase());
		this.commandService.post(commandData, path.toLowerCase()).subscribe();
		this.invoked.emit(true);
	}

	getCommands() {
		this.commandService.get().subscribe(x => {
			const dialogRef = this.dialog.open(DataFormComponent, {
				data: { items: x, type: 'command'}
			});
		});
	}

	reset() {
		const dynamicPropertyName: string = "reset";
		const commandData: Command = {
			name: "Reset",
			stationId: Number.parseInt(this.commandServiceFormGroup.get("stationId").value),
			[dynamicPropertyName]: true
		}
		this.commandService.post(commandData, "reset").subscribe(x => console.log(x));
	}
}
