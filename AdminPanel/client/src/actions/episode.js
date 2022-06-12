import axios from "axios";

export const addEpisode = async (id,title, serialId, duration, number, videoLink, previewVideo) => {
    try {
        const response = await axios.post(
            "http://localhost:5000/episodes/addEpisode",
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

export const deleteEpisode = async (id) => {
    try {
        const response = await axios.post(
            "http://localhost:5000/episodes/deleteEpisode",
            {
                id
            }
        );
    } catch (e) {

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

export const episodesBySerialId = async (serialId) => {
    try {
        const response = await axios.get(
            `http://localhost:5000/episodes/episodesBySerialId?serialId=${serialId}`,
        );
    } catch (e) {
        alert(e);
    }
}