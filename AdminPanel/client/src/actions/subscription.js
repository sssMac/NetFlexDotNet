import axios from "axios";

export const addSubscription = async (name, cost) => {
    try {
        const response = await axios.post(
            "http://localhost:5000/subscription/add",
            {
                name,
                cost
            }
        );
    } catch (e) {
        alert(e);
    }
}

export const removeSubscription = async (id) => {
    try {
        const response = await axios.post(
            "http://localhost:5000/subscription/remove",
            {
                id
            }
        );
    } catch (e) {
        alert(e);
    }
}

export const editSubscription = async (id, name, cost) => {
    try {
        const response = await axios.post(
            "http://localhost:5000/subscription/edit",
            {
                id,
                name,
                cost
            }
        );
    } catch (e) {
        alert(e);
    }
}

export const setSubscription = async (userId, subsId) => {
    try {
        const response = await axios.post(
            "http://localhost:5000/subscription/setSubs",
            {
                userId,
                subsId
            }
        );
    } catch (e) {
        alert(e);
    }
}

export const allSubscription = async (userId, subsId) => {
    try {
        const response = await axios.get(
            "http://localhost:5000/subscription/all",
        );
    } catch (e) {
        alert(e);
    }
}

export const allUserSubscription = async (userId, subsId) => {
    try {
        const response = await axios.get(
            "http://localhost:5000/subscription/allUser",
        );
    } catch (e) {
        alert(e);
    }
}
