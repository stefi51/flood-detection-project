import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, FormControl } from '@angular/forms';
import { CommandService } from 'src/app/services/command.service';

@Component({
	selector: 'app-command-service',
	templateUrl: './command-service.component.html',
	styleUrls: ['./command-service.component.scss']
})
export class CommandServiceComponent implements OnInit {

	commandServiceFormGroup: FormGroup;

	constructor(private fb: FormBuilder, private commandService: CommandService) { }

	ngOnInit(): void {
		this.commandServiceFormGroup = this.fb.group({
			commandName: new FormControl(''),
			stationId: new FormControl('')
		})
	}

	showCommands() {
		this.commandService.get().subscribe(console.log);
	}

	invokeCommand() {
		const commandData = { 
			name: this.commandServiceFormGroup.get("commandName").value,
			stationId: this.commandServiceFormGroup.get("stationId").value
		}
		this.commandService.post(commandData).subscribe(console.log);
	}
}
