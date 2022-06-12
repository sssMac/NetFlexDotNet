import axios from "axios";
import {setUser} from "../reducers/userReducer";

export const addGenre = async (genreName) => {
    try {
        const response = await axios.post(
            "http://localhost:5000/genre/addGenre",
            {
                genreName
            }
        );
    } catch (e) {
        alert(e);
    }
}

export const removeGenre = async (id) => {
    try {
        const response = await axios.post(
            "http://localhost:5000/genre/deleteGenre",
            {
                id,
            }
        );
    } catch (e) {
        alert(e);
    }
}

export const renameGenre = async (id, newName) => {
    try {
        const response = await axios.post(
            "http://localhost:5000/genre/renameGenre",
            {
                id,
                newName,
            }
        );
    } catch (e) {
        alert(e);
    }
}

