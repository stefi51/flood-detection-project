export class RefinedData {
	waterFlow: number;
	waterLevel: number;
	rainfall: number;
	stationId: number;
	measuredDateTime: Date;
	analyzedDataTime: Date;
	analyzedEventType: EventType;
}

export enum EventType {
	Warning,
	Alarm
}