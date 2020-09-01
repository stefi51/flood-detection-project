import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { FormGroup, FormBuilder, FormControl } from '@angular/forms';
import { CommandService } from 'src/app/services/command.service';
import { Command } from 'src/app/models/command.model';

@Component({
	selector: 'app-command-service',
	templateUrl: './command-service.component.html',
	styleUrls: ['./command-service.component.scss']
})
export class CommandServiceComponent implements OnInit {

	@Input() stationId: number;
	@Output() invoked: EventEmitter<boolean> = new EventEmitter();

	commandServiceFormGroup: FormGroup;

	constructor(private fb: FormBuilder, private commandService: CommandService) { }

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
		this.commandService.post(commandData, path.toLowerCase()).subscribe(console.log);
		this.invoked.emit(true);
	}
}
