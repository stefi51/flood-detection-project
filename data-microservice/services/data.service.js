"use strict";

const request = require('request');
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
				id: { type: "number" }
			},
			async handler(ctx) {
				try {
					console.log(ctx.params.id);
					this.influx.writePoints([
						{
							measurement: 'data',
							fields: {
								id: ctx.params.id
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