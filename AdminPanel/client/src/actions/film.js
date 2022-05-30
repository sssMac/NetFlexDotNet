import axios from "axios";

export const addFilm = async (poster, title, duration, ageRating, userRating, description, videoLink, preview, genreName) => {
    try {
        const response = await axios.post(
            "http://localhost:5000/films/addFilm",
            {
                poster,
                title,
                duration,
                ageRating,
                userRating,
                description,
                videoLink,
                preview,
                genreName
            }
        );
    } catch (e) {
        alert(e);
    }
}

export const deleteFilm = async (id) => {
    try {
        const response = await axios.post(
            "http://localhost:5000/films/deleteFilm",
            {
                id,
            }
        );
    } catch (e) {
        alert(e);
    }
}

export const updateFilm = async (id, poster, title, duration, ageRating, userRating, description, videoLink, preview, genreName) => {
    try {
        const response = await axios.post(
            "http://localhost:5000/films/updateFilm",
            {
                id,
                poster,
                title,
                duration,
                ageRating,
                userRating,
                description,
                videoLink,
                preview,
                genreName
            }
        );
    } catch (e) {
        alert(e);
    }
}

export const allFilms = async () => {
    try {
        const response = await axios.get(
            "http://localhost:5000/films/all",
        );
    } catch (e) {
        alert(e);
    }
}
