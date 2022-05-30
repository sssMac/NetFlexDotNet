const db = require("../database/db");
const queries = require("../queries/filmsQueries");
const createError = require("../errors/createError");
const {randomUUID} = require("crypto");

class filmsController {
    async addFilm(req, res, next) {
        const {poster, title, duration, ageRating, userRating, description, videoLink, preview, genreName} = req.body
        
        const findOne = await db.query(queries.findFilm, [title])
        if (findOne.error)
            return next(new createError(400, findOne.error));

        if (findOne.rows.length !== 0)
            return next(new createError(500, `Film with name ${title} already exist`));

        const generatedUUID = randomUUID()
        const filmAddResult = await db.query(queries.addFilm, [generatedUUID, poster, title, duration, ageRating, userRating, description, videoLink, preview])
        if (filmAddResult.error) return next(new createError(500, filmAddResult.error));
        const filmGenreAddResult = await db.query(queries.addFilmGenre, [randomUUID(), genreName, generatedUUID])
        if (filmGenreAddResult.error) return next(new createError(500, filmGenreAddResult.error));
        
        return res.status(200).json({
            message: `Serial with name ${title} and genre ${genreName} added`
        })

    }

    async deleteFilm(req, res, next) {
        const {id} = req.body

        const findOne = await db.query(queries.findFilmById, [id])

        if (findOne.rows.length === 0)
            return next(new createError(404, `Not found film!!`));

        const result = await db.query(queries.deleteFilm, [id])
        if (result.error) return next(new createError(500, 'Error deleting film!'));
        const filmGenreDeleteResult = await db.query(queries.deleteFilmGenre, [id])
        if (filmGenreDeleteResult.error) return next(new createError(500, 'Error deleting film!'));

        return res.status(200).json({
            message: `Film and genre deleted`
        })
    }

    async updateFilm(req, res, next) {
        const {id, poster, title, duration, ageRating, userRating, description, videoLink, preview, genreName} = req.body

        const findOne = await db.query(queries.findFilmById, [id])

        if (findOne.rows.length === 0)
            return next(new createError(404, `Not found film!!`));

        const result = await db.query(queries.updateFilm, [id, poster, title, duration, ageRating, userRating, description, videoLink, preview])
        if (result.error) return next(new createError(500, result.error));
        const genreNameUpdateResult = await db.query(queries.updateFilmGenre, [id, genreName])
        if (genreNameUpdateResult.error) return next(new createError(500, result.error));

        return res.status(200).json({
            message: `Film and genre updated`
        })
    }
    
    async getAllFilms(req, res, next) {
        const result = await db.query(queries.getAllFilms)
        return res.status(200).json(result.rows)
    }
}

module.exports = new filmsController();
