// this file for beanies logic
const db = require("../database/db");
const queries = require("../queries/seriesQueries");
const createError = require("../errors/createError");
const tokenService = require("../service/tokenService")
const serialDTO = require("../models/serialDTO")
const {randomUUID} = require("crypto");

class seriesController {
    async addSerial(req, res, next) {
        const {serialName} = req.body
        if (!serialName)
            return next(new createError(400, 'Not found serialName'));

        const findOne = await db.query(queries.findSerial, [serialName])
        if (findOne.error)
            return next(new createError(401, findOne.error));

        if (findOne.rows.length !== 0)
            return next(new createError(401, `Serial with name ${serialName} already exist`));

        const result = await db.query(queries.addSerial, [randomUUID(), serialName])
        if (result.error) return next(new createError(401, result.error));

        return res.status(200).json({
            message: `Serial with name ${serialName} added`
        })

    }

    async deleteSerial(req, res, next) {
        const {id} = req.body

        const findOne = await db.query(queries.findSerialById, [id])

        if (findOne.rows.length === 0)
            return next(new createError(401, `Not found serial!!`));

        const result = await db.query(queries.deleteSerial, [id])

        if (result.error) return next(new createError(401, 'Serial not found!!'));

        return res.status(200).json({
            message: `Serial deleted`
        })
    }

    async renameSerial(req, res, next) {
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
    async getAllSerials(req, res, next) {
        const result = await db.query(queries.getAllSerials)
        return res.status(200).json(result.rows)
    }
}

module.exports = new seriesController();
