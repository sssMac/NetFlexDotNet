const addFilm = 'insert into "Films" ("Id", "Poster", "Title", "Duration", "AgeRating", "UserRating", "Description", "VideoLink", "Preview") values ($1,$2,$3,$4,$5,$6,$7,$8,$9)';
const deleteFilm = 'delete from "Films" where "Id" = $1'
const renameFilm = 'update "Films" set "FilmName" = $1 where "Id" = $2';
const findFilm = 'select * from "Films" where "FilmName" = $1';
const findFilmById = 'select * from "Films" where "Id" = $1';
const getAllFilms = 'select * from "Films"';


module.exports = {
    addFilm,
    deleteFilm,
    renameFilm,
    findFilm,
    findFilmById,
    getAllFilms
};
