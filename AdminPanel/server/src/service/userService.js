const db = require("../database/db");
const queries = require("../queries/userQueries");

class UserService {
    async findOne(email){
        const user = await db.query(queries.findUser, [email])
        if (user.error) return null
        if (user.rows.length === 0) return null;
        return user.rows[0];
    }
    async save(userDTO){
        const user = await db.query(queries.addUser, [userDTO.Id,userDTO.Email,userDTO.Password])
        if (user.error) return null
        return user.rows[0];
    }
    async saveToken(userId,accessToken){
        const user = await db.query(queries.saveToken, [userId,accessToken])
        if (user.error) return null
    }
}

module.exports = new UserService();