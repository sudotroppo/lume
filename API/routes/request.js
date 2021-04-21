const express = require('express');
const router = express.Router();

	router.get('/', (req, res, next) => {
		res.status(200).json({
			message: 'Richiesta mandata'
		});
	});

	router.post('/', (req, res, next) => {
		const request = {
			userId: req.body.userId,
			titolo: req.body.titolo,
			descrizione: req.body.descrizione
		};
		res.status(201).json({
			message: 'La richiesta è stata creata',
			request: request
		});
	});

	router.get('/:requestId', (req, res, next) => {
		res.status(200).json({
			message: 'Dettagli richiesta',
			requestId: req.params.requestId
		});
	});

	router.delete('/:requestId', (req, res, next) => {
		res.status(200).json({
			message: 'Richiesta rimossa',
			requestId: req.params.requestId
		});
	});

		

module.exports = router;