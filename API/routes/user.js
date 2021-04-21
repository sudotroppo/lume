const express = require('express');
const router = express.Router();

	router.get('/', (req, res, next) => {
		res.status(200).json({
			message: 'Handling GET requests to /user'
		});
	});

	router.post('/', (req, res, next) => {
		const user = {
			nome: req.body.nome,
			cognome: req.body.cognome
		};
		res.status(201).json({
			message: 'Handling POST requests to /user',
			createdUser: user
		});
	});

	router.get('/:userId', (req, res, next) => {
		const id = req.params.userId;
		if(id === 'special'){
			res.status(200).json({
				message: 'Hai trovato l ID',
				id: id
			});
		}else{
			res.status(200).json({
				message: 'Hai mandato un ID'
			});
		}
	});

		router.patch('/:userId', (req, res, next) => {
			res.status(200).json({
				message: 'Updated user',
			});
		});
		
		router.delete('/:userId', (req, res, next) => {
			res.status(200).json({
				message: 'Deleted user',
			});
		});

module.exports = router;