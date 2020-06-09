"use strict";

const express = require("express");
const bodyParser = require('body-parser');

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
					return this.broker.call("data.create", { id: payload.id })
						.then(data =>
							res.send(data)
						);
				})
				.catch(this.handleErr(res));
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
	}
};