export class RefinedData {
	waterFlow: number;
	waterLevel: number;
	rainfall: number;
	stationId: number;
	measuredDateTime: any;
	analyzedDataTime: any;
	analyzedEventType: EventType;
}

export enum EventType {
	Warning,
	Alarm
}