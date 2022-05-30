// this file for beanies logic
const db = require("../database/db");
const queries = require("../queries/episodesQueries");
const createError = require("../errors/createError");
const tokenService = require("../service/tokenService")
const episodeDTO = require("../models/episodeDTO")
const {randomUUID} = require("crypto");

class episodesController {
    async addEpisode(req, res, next) {
        const {title, serialId, duration, number, videoLink, previewVideo} = req.body
        
        const findOne = await db.query(queries.findEpisode, [title])
        if (findOne.error)
            return next(new createError(500, findOne.error));

        if (findOne.rows.length !== 0)
            return next(new createError(404, `Episode with name ${title} already exist`));

        const result = await db.query(queries.addEpisode, [randomUUID(), title, serialId, duration, number, videoLink, previewVideo])
        if (result.error) return next(new createError(500, result.error));

        return res.status(200).json({
            message: `Episode with name ${title} added`
        })

    }

    async deleteEpisode(req, res, next) {
        const {id} = req.body

        const findOne = await db.query(queries.findEpisodeById, [id])

        if (findOne.rows.length === 0)
            return next(new createError(404, `Not found episode!!`));

        const result = await db.query(queries.deleteEpisode, [id])

        if (result.error) return next(new createError(500, 'Episode delete error!!'));

        return res.status(200).json({
            message: `Episode deleted`
        })
    }

    async updateEpisode(req, res, next) {
        const {id, title, serialId, duration, number, videoLink, previewVideo} = req.body

        const findOne = await db.query(queries.findEpisodeById, [id])

        if (findOne.rows.length === 0)
            return next(new createError(404, `Not found episode!!`));

        const result = await db.query(queries.updateEpisode, [id, title, serialId, duration, number, videoLink, previewVideo])
        if (result.error) return next(new createError(500, result.error));

        return res.status(200).json({
            message: `Episode updated`
        })
    }
    
    async getAllEpisodes(req, res, next) {
        const result = await db.query(queries.getAllEpisodes)
        return res.status(200).json(result.rows)
    }
}

module.exports = new episodesController();
