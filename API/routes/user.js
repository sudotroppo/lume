const express = require('express');
const router = express.Router();

const db = require("../services/database");


	
	/*const getUserIdByEmail = async (req, res, next) => {
	req.data = await db.user.findUnique({
		where: {
		 email: req.body.email,
		},
	 });
	next();
	};

	router.get('/', (req, res, next) => {
		res.json();
	});
	*/

	router.post('/', (req, res, next) => {
		const user = {
			email: req.body.email,
			nome: req.body.nome,
			cognome: req.body.cognome
		};

		db.user.create({ data: user })
                    .catch((err) => {
                        return res.status(500).json({
                            error: err
                        });

                    });
		res.status(201).json({
			message: "Post di user",
			Utente:user
		});

	});

module.exports = router;