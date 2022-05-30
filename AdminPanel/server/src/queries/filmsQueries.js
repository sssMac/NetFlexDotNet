const addFilm = 'insert into "Films" ("Id", "Poster", "Title", "Duration", "AgeRating", "UserRating", "Description", "VideoLink", "Preview") values ($1,$2,$3,$4,$5,$6,$7,$8,$9)';
const addFilmGenre = 'insert into "GenreVideos" ("Id", "GenreName", "ContentId") values ($1, $2, $3)'
const deleteFilm = 'delete from "Films" where "Id" = $1'
const deleteFilmGenre = 'delete from "GenreVideos" where "ContentId" = $1'
const updateFilm = 'update "Films" set "Poster" = $2, "Title" = $3, "Duration" = $4, "AgeRating" = $5, "UserRating" = $6, "Description" = $7, "VideoLink" = $8, "Preview" = $9 where "Id" = $1';
const updateFilmGenre = 'update "GenreVideos" set "GenreName" = $2 where "ContentId" = $1'
const findFilm = 'select * from "Films" where "FilmName" = $1 INNER JOIN GenreVideos ON Films.Id=GenreVideos.ContentId';
const findFilmById = 'select * from "Films" where "Id" = $1 INNER JOIN GenreVideos ON Films.Id=GenreVideos.ContentId';
const getAllFilms = 'select * from "Films" INNER JOIN GenreVideos ON Films.Id=GenreVideos.ContentId';


module.exports = {
    addFilm,
    addFilmGenre,
    deleteFilm,
    deleteFilmGenre,
    updateFilm,
    updateFilmGenre,
    findFilm,
    findFilmById,
    getAllFilms
};
