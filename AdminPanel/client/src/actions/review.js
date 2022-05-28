import axios from "axios";
import {setUser} from "../reducers/userReducer";

export const publicReview = async (UserName, ContentId, Text, Rating) => {
    try {
        const response = await axios.post(
            "http://localhost:5000/review/publicReview",
            {
                UserName,
                ContentId,
                Text,
                Rating,
            }
        );

    } catch (e) {
        alert(e);
    }
}

export const cancelReview = async (Id) => {
    try {
        const response = await axios.post(
            "http://localhost:5000/review/reject",
            {
                Id,
            }
        );
    } catch (e) {
        alert(e);
    }
}

export const acceptReview = async (Id) => {
    try {
        const response = await axios.post(
            "http://localhost:5000/review/accept",
            {
                Id,
            }
        );
    } catch (e) {
        alert(e);
    }
}

export const pendingReview = async (Id) => {
    try {
        const response = await axios.post(
            "http://localhost:5000/review/pending",
            {
                Id,
            }
        );
    } catch (e) {
        alert(e);
    }
}

