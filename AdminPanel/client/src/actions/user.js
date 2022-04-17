import axios from "axios";

export const registration = async (email, password, confirmPassword) => {
    try {
        const response = await axios.post('http://localhost:5000/auth/registration', {
            email,
            password,
            confirmPassword
        })
        alert(response.data.message)
    }
    catch (e) {
        alert(e)
    }

}