"use strict";

const express = require("express");
const bodyParser = require('body-parser');
const amqp = require('amqplib/callback_api');

module.exports = {
	name: "gateway",
	settings: {
		port: process.env.PORT || 3000,
	},
	methods: {
		initRoutes(app) {
			app.get("/data", this.getData);
			app.post("/data", this.postData);
		},
		getData(req, res) {
			return Promise.resolve()
				.then(() => {
					return this.broker.call("data.read")
						.then(data => {
							res.send(data);
						});
				})
				.catch(this.handleErr(res));
		},
		postData(req, res) {
			const payload = req.body;
			return Promise.resolve()
				.then(() => {
					return this.sendData(payload)
						.then(data =>
							res.send(data)
						);
				})
				.catch(this.handleErr(res));
		},
		sendData(payload) {
			return this.broker.call("data.create", { id: payload.id });
		},
		handleErr(res) {
			return err => {
				res.status(err.code || 500).send(err.message);
			};
		}
	},
	created() {
		const app = express();
		app.use(bodyParser.urlencoded({ extended: false }));
		app.use(bodyParser.json());
		app.listen(this.settings.port);
		this.initRoutes(app);
		this.app = app;

		amqp.connect('amqp://116.202.13.157', (error0, connection) => {
			if (error0) {
				throw error0;
			}
			connection.createChannel((error1, channel) => {
				if (error1) {
					throw error1;
				}
				var queue = 'CommandQueue';
				channel.assertQueue(queue, {
					durable: false
				});
				channel.prefetch(1);
				console.log(" [*] Waiting for messages in %s. To exit press CTRL+C", queue);
				channel.consume(queue, (msg) => {
					var secs = msg.content.toString().split('.').length - 1;
					console.log(" [x] Received %s", msg.content.toString());
					this.sendData({ id: Number(msg.content.toString()) });
					setTimeout(function () {
						console.log(" [x] Done");
						channel.ack(msg);
					}, secs * 1000);
				}, {
					noAck: false
				});
			});
		});
	}
};