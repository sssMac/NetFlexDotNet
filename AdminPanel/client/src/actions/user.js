import axios from "axios";

export const registration = async (email, password, confirmPassword) => {
  try {
    const response = await axios({
      method: "post",
      url: "http://localhost:5000/auth/registration",
      headers: {},
      data: {
        email,
        password,
        confirmPassword,
      },
    });

    // const response = await axios.post(
    //   "http://localhost:5000/auth/registration",
    //   {
    //     email,
    //     password,
    //     confirmPassword,
    //   }
    // );
    console.log(response);
    alert(response.data.message);
  } catch (e) {
    alert(e);
  }
};
