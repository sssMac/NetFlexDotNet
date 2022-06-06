import axios from "axios";
import {setUser} from "../reducers/userReducer";

export const registration = async (email, password, confirmPassword) => {
  try {
    const response = await axios.post(
      "http://localhost:5000/auth/registration",
      {
        email,
        password,
        confirmPassword,
      }
    );
    alert(response.data.user.email);
  } catch (e) {
    alert(e);
  }
}

export const login = (email, password) => {
    return async dispatch => {
        try {
            const response = await axios.post(
                "http://localhost:5000/auth/login",
                {
                    email,
                    password,
                }
            );
            dispatch(setUser(response.data.user))
            localStorage.setItem('token', response.data.accessToken)

        } catch (e) {
            alert(e.response.data.message);
        }
    }
}

export const auth =  () => {
    return async dispatch => {
        try {
            const response = await axios.get(`http://localhost:5000/auth/auth`,
                {headers:{Authorization:`Bearer ${localStorage.getItem('token')}`}}
            )
            dispatch(setUser(response.data.user))

            localStorage.setItem('token', response.data.accessToken)
        } catch (e) {
            console.log(e.response.data.message)
            localStorage.removeItem('token')

        }
    }
}

export const block =  (email) => {
    return async dispatch => {
        try {
            const response = await axios.post("http://localhost:5000/auth/block",
                {
                    email,
                },
                {headers:{Authorization:`Bearer ${localStorage.getItem('token')}`}}
            );

        } catch (e) {
            alert(e);
        }
    }
}

export const unblock =  (email) => {
    return async dispatch => {
        try {
            const response = await axios.post("http://localhost:5000/auth/unblock",
                {
                    email,
                },
                {headers:{Authorization:`Bearer ${localStorage.getItem('token')}`}}
            );
        } catch (e) {
            alert(e);
        }
    }
}

