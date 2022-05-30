// this file for beanies logic
const db = require("../database/db");
const queries = require("../queries/seriesQueries");
const createError = require("../errors/createError");
const tokenService = require("../service/tokenService")
const serialDTO = require("../models/serialDTO")
const {randomUUID} = require("crypto");

class seriesController {
    async addSerial(req, res, next) {
        const {poster, title, numEpisodes, ageRating, userRating, description} = req.body

        const findOne = await db.query(queries.findSerial, [title])
        if (findOne.error)
            return next(new createError(500, findOne.error));

        if (findOne.rows.length !== 0)
            return next(new createError(400, `Serial with name ${title} already exist`));

        const result = await db.query(queries.addSerial, [randomUUID(), poster, title, numEpisodes, ageRating, userRating, description])
        if (result.error) return next(new createError(500, result.error));

        return res.status(200).json({
            message: `Serial with name ${title} added`
        })

    }

    async deleteSerial(req, res, next) {
        const {id} = req.body

        const findOne = await db.query(queries.findSerialById, [id])

        if (findOne.rows.length === 0)
            return next(new createError(404, `Not found serial!!`));

        const result = await db.query(queries.deleteSerial, [id])

        if (result.error) return next(new createError(500, 'Unable to delete serial!!'));

        return res.status(200).json({
            message: `Serial deleted`
        })
    }

    async updateSerial(req, res, next) {
        const {id, poster, title, numEpisodes, ageRating, userRating, description} = req.body

        const findOne = await db.query(queries.findSerialById, [id])

        if (findOne.rows.length === 0)
            return next(new createError(404, `Not found serial!!`));

        const result = await db.query(queries.updateSerial, [id, poster, title, numEpisodes, ageRating, userRating, description])
        if (result.error) return next(new createError(500, result.error));

        return res.status(200).json({
            message: `Serial updated`
        })
    }
    
    async getAllSerials(req, res, next) {
        const result = await db.query(queries.getAllSerials)
        return res.status(200).json(result.rows)
    }
}

module.exports = new seriesController();
