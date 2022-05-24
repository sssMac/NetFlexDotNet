const db = require("../database/db");
const queries = require("../queries/filmsQueries");
const createError = require("../errors/createError");
const tokenService = require("../service/tokenService")
const filmsDTO = require("../models/filmsDTO")
const {randomUUID} = require("crypto");

class filmsController {
    async addFilm(req, res, next) {
        const {filmName} = req.body
        if (!filmName)
            return next(new createError(400, 'Not found filmName'));

        const findOne = await db.query(queries.findFilm, [filmName])
        if (findOne.error)
            return next(new createError(401, findOne.error));

        if (findOne.rows.length !== 0)
            return next(new createError(401, `Film with name ${filmName} already exist`));

        const result = await db.query(queries.addFilm, [randomUUID(), filmName])
        if (result.error) return next(new createError(401, result.error));

        return res.status(200).json({
            message: `Serial with name ${filmName} added`
        })

    }

    async deleteFilm(req, res, next) {
        const {id} = req.body

        const findOne = await db.query(queries.findFilmById, [id])

        if (findOne.rows.length === 0)
            return next(new createError(401, `Not found film!!`));

        const result = await db.query(queries.deleteFilm, [id])

        if (result.error) return next(new createError(401, 'Film not found!!'));

        return res.status(200).json({
            message: `Film deleted`
        })
    }

    async renameFilm(req, res, next) {
        const {id, newName} = req.body

        const findOne = await db.query(queries.findSerialById, [id])

        if (findOne.rows.length === 0)
            return next(new createError(401, `Not found serial!!`));

        const result = await db.query(queries.renameSerial, [newName, id])
        if (result.error) return next(new createError(401, result.error));

        return res.status(200).json({
            message: `Serial renamed`
        })
    }
    async getAllFilms(req, res, next) {
        const result = await db.query(queries.getAllFilms)
        return res.status(200).json(result.rows)
    }
}

module.exports = new filmsController();
