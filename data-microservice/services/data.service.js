"use strict";

const Influx = require('influx');
const request = require('request');

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
					request.post('http://analyticsmicroservice:80/api/analytics/postdata2', {
						json: {
							waterFlow: ctx.params.waterFlow,
							waterLevel: ctx.params.waterLevel,
							rainfall: ctx.params.rainfall,
							stationId: ctx.params.stationId,
							measuredDateTime: ctx.params.measuredDateTime
						}
					}, (error, res, body) => {
						if (error) {
							console.error(error)
							return
						}
						console.log(`statusCode: ${res.statusCode}`)
						console.log(body)
					})
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
		this.influx = new Influx.InfluxDB("http://admin:admin@influx:8086/data");
		this.influx.getDatabaseNames().then((names) => {
			if (!names.includes('data')) {
				return this.influx.createDatabase('data');
			}
			return null;
		});
	}
};