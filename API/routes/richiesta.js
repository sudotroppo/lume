const express = require('express');
const router = express.Router();

	router.get('/', (req, res, next) => {
		res.status(200).json({
			message: 'Richiesta mandata'
		});
	});

	router.post('/', (req, res, next) => {
		const richiesta = {
			userId: req.body.userId,
			titolo: req.body.titolo,
			descrizione: req.body.descrizione
		};
		res.status(201).json({
			message: 'La richiesta è stata creata',
			richiesta: richiesta
		});
	});

	router.get('/:richiestaId', (req, res, next) => {
		res.status(200).json({
			message: 'Dettagli richiesta',
			richiestaId: req.params.richiestaId
		});
	});

	router.delete('/:richiestaId', (req, res, next) => {
		res.status(200).json({
			message: 'Richiesta rimossa',
			richiestaId: req.params.richiestaId
		});
	});

		

module.exports = router;