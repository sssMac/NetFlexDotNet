import axios from "axios";
import {setUser} from "../reducers/userReducer";

export const createRole = async (roleName) => {
    try {
        const response = await axios.post(
            "http://localhost:5000/roles/createRole",
            {
                roleName
            }
        );
    } catch (e) {
        alert(e);
    }
}

export const removeRole = async (roleId) => {
    try {
        const response = await axios.post(
            "http://localhost:5000/roles/removeRole",
            {
                roleId,
            }
        );
    } catch (e) {
        alert(e);
    }
}

export const assignRole = async (userId,roleId) => {
    try {
        const response = await axios.post(
            "http://localhost:5000/roles/assignRole",
            {
                userId,
                roleId,
            }
        );
    } catch (e) {
        alert(e);
    }
}

