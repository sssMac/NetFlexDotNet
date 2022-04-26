const createError = require("../errors/createError");
const userService = require("../service/userService");
const userDto = require("../models/userDTO")

class AuthController {
    async all(req, res, next) {
        try {

            const allUsers = await userService.allUsers()

            if (!allUsers)
                return next(new createError(400, `not found users!!`))

            return res.status(200).json(allUsers.map(u => {
                return new userDto(
                    u.Id,
                    u.Email,
                    u.UserName,
                    u.Avatar,
                    u.EmailConfirmed,
                    u.Status)
            }))

        } catch (e) {
            console.log(e)
            return next(new createError(400, e.message))
        }
    }
}


module.exports = new AuthController();