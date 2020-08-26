"use strict";

const Influx = require('influx');

module.exports = {
	name: "data",
	actions: {
		read: {
			async handler(ctx) {
				try {
					const res = await this.influx.query(
						`select * from data`
					);
					return res;
				}
				catch (err) {
					console.log(err);
					return null;
				}
			}
		},
		create: {
			params: {
				waterFlow: { type: "number" },
				waterLevel: { type: "number" },
				rainfall: { type: "number" },
				stationId: { type: "number" },
				measuredDateTime: { type: "string"}
			},
			async handler(ctx) {
				try {
					console.log(ctx.params.waterFlow);
					console.log(ctx.params.waterLevel);
					console.log(ctx.params.rainfall);
					console.log(ctx.params.stationId);
					console.log(ctx.params.measuredDateTime);
					this.influx.writePoints([
						{
							measurement: 'data',
							fields: {
								waterFlow: ctx.params.waterFlow,
								waterLevel: ctx.params.waterLevel,
								rainfall: ctx.params.rainfall,
								stationId: ctx.params.stationId,
								measuredDateTime: ctx.params.measuredDateTime
							},
							time: Date.now()
						}
					]);

				}
				catch (err) {
					console.log(err);
				}
			}
		}
	},
	methods: {},
	events: {},
	created() {
		this.influx = new Influx.InfluxDB("http://admin:admin@116.202.13.157:8086/data");
		this.influx.getDatabaseNames().then((names) => {
			if (!names.includes('data')) {
				return this.influx.createDatabase('data');
			}
			return null;
		});
	}
};