// this file for beanies logic
const db = require("../database/db");
const queries = require("../queries/seriesQueries");
const createError = require("../errors/createError");
const tokenService = require("../service/tokenService")
const serialDTO = require("../models/serialDTO")

class SeriesController {
    async addSerial(req, res, next) {

        const access_token =  req.headers['user-agent'];

        if (!await tokenService.isAdmin(access_token))
            return next(new createError(403, "Данная функция только для администраторов."));

        const {Id,Poster,Title,NumEpisodes,AgeRating,UserRating,Description} = req.body;

        await db.query(queries.addSerial,[Id,Poster,Title,NumEpisodes,AgeRating,UserRating,Description], (error, results) => {
            if (error)
                return next(new createError(400, "Данные неверны"));

            const serial = results.rows;
            console.log(serial);
            return res.status(200).json(new serialDTO(
                Id,
                Title,
                NumEpisodes,
                AgeRating,
                UserRating,
                Description
            ))
        });
    }
    async deleteSerial(req, res, next) {

    }
    async editSerial(req, res, next) {

    }
}

module.exports = new SeriesController();
