import axios from "axios";

export const addEpisode = async (title, serialId, duration, number, videoLink, previewVideo) => {
    try {
        const response = await axios.post(
            "http://localhost:5000/episodes/addEpisode",
            {
                title,
                serialId,
                duration,
                number,
                videoLink,
                previewVideo
            }
        );
    } catch (e) {
        alert(e);
    }
}

export const deleteEpisode = async (id) => {
    try {
        const response = await axios.post(
            "http://localhost:5000/episodes/deleteEpisode",
            {
                id
            }
        );
    } catch (e) {
        alert(e);
    }
}

export const updateEpisode = async (id, title, serialId, duration, number, videoLink, previewVideo) => {
    try {
        const response = await axios.post(
            "http://localhost:5000/episodes/updateEpisode",
            {
                id,
                title,
                serialId,
                duration,
                number,
                videoLink,
                previewVideo
            }
        );
    } catch (e) {
        alert(e);
    }
}

export const episodesBySerialId = async (id) => {
    try {
        const response = await axios.get(
            "http://localhost:5000/episodes/episodesBySerialId",
            {
                id
            }
        );
    } catch (e) {
        alert(e);
    }
}