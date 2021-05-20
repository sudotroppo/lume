const express = require('express');
const router = express.Router();

const db = require("../services/database");



	router.post('/', async(req, res, next) => {

		const user = await db.user.create({
		data:{
			email: req.body.email,
			nome: req.body.nome,
			cognome: req.body.cognome
			}
		});

        console.log('Created new user: ', user)  
		res.status(201).json({
			message: "Post di user",
			user:user
		});

	});

	router.get('/:requestId', async(req, res,next) => {
		const user = await db.user.findUnique({
		where: {
			id: Number(req.params.requestId),
		},
		select: {
			nome:true,
			cognome:true,
			email:true,
		}
		})

	
		res.status(201).json({
			message: "Get di user by Id",
			user:user
		});
	});
	
module.exports = router;