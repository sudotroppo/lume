const express = require('express');
const router = express.Router();
const db = require("../services/database");
const authenticateMiddleware = require("../services/auth");


	router.post('/', authenticateMiddleware, async(req, res, next) => {

		const utente = await db.utente.create({
		data:{
			email: req.body.email,
			nome: req.body.nome,
			cognome: req.body.cognome,
			telefono: req.body.telefono
			}
		});

        console.log('Created new utente: ', utente)  
		res.status(201).json({
			message: "Post di utente",
			utente:utente
		});

	});

	router.get('/:userId', async(req, res,next) => {
		const utente = await db.utente.findUnique({
		where: {
			id: Number(req.params.utenteId),
		},
		select: {
			nome:true,
			cognome:true,
			email:true,
			telefono:true,
		}
		})

	
		res.status(201).json({
			message: "Get di utente by id",
			utente:utente
		});
	});
	
module.exports = router;