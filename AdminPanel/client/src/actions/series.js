import axios from "axios";

export const addSeries = async (poster, title, numEpisodes, ageRating, userRating, description) => {
    try {
        const response = await axios.post(
            "http://localhost:5000/series/addSerial",
            {
                poster,
                title,
                numEpisodes,
                ageRating,
                userRating,
                description
            }
        );
    } catch (e) {
        alert(e);
    }
}

export const deleteSeries = async (id) => {
    try {
        const response = await axios.post(
            "http://localhost:5000/series/deleteSerial",
            {
                id,
            }
        );
    } catch (e) {
        alert(e);
    }
}

export const updateSerial = async (id, poster, title, numEpisodes, ageRating, userRating, description) => {
    try {
        const response = await axios.post(
            "http://localhost:5000/series/deleteSerial",
            {
                id,
                poster,
                title,
                numEpisodes,
                ageRating,
                userRating,
                description
            }
        );
    } catch (e) {
        alert(e);
    }
}

export const allSeries = async () => {
    try {
        const response = await axios.get(
            "http://localhost:5000/series/all",
        );
    } catch (e) {
        alert(e);
    }
}