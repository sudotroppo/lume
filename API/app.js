const express = require('express');
const app = express();
const morgan = require('morgan');
const bodyParser = require('body-parser');

const userRoutes = require('./routes/user');
const requestRoutes = require('./routes/request');

app.use(morgan('dev'));
app.use(bodyParser.urlencoded({extended: false}));
app.use(bodyParser.json());

app.use((req, res, next) => {
	res.header("Access-Control-Allow-Origin", "*");
	res.header(
		"Access-Control-Allow-Headers",
		"Origin, X-Requested-With, Content-Type, Accept, Authorization"
	);
	if (req.method === 'OPTIONS') {
		res.header('Access-Control-Allow-Methhos', 'PUT, POST, PATCH, DELETE, GET');
		return res.status(200).json({});
	}
	next();
});

//Routes which should handle request
app.use('/user', userRoutes);
app.use('/request', requestRoutes);

app.use((req, res, next) => {
	const error = new Error('Non trovato');
	error.status = 404;
	next(error);
})

app.use((error, req, res, next) => {
	res.status(error.status || 500);
	res.json({
		error: {
			message: error.message
		}
	});
});

module.exports = app;