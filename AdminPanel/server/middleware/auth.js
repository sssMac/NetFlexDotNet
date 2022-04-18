const jwt = require("jsonwebtoken");
const createError = require("../src/errors/createError");
const db = require("../src/database/db");
const queries = require("../src/queries/userQueries");

const config = process.env;

const verifyToken = (req, res, next) => {
    const token =
        req.body.token || req.query.token || req.headers["access-token"];

    if (!token) {
        return next(new createError(403,"A token is required for authentication"))
    }
    try {
        const decoded = jwt.verify(token, config.ACCESS_TOKEN_SECRET);
        req.user = decoded;
    } catch (err) {
        return next(new createError(401,"Invalid Token"))
    }
    return next();
};

async function verifyAdmin(req,res,next){
    const accessToken = req.body.token || req.query.token || req.headers["access-token"];

    const results = await db.query(queries.getUserIdByToken, [accessToken]);
    if (results.error)
        return next(new createError(401,results.error))
    if (results.rows.length === 0)
        return next(new createError(404,"access-token not found!! "))
    const userId = results.rows[0].UserId
    if (userId !== null) {
        const results = await db.query(queries.getRoleName, [userId]);
        if (results.error)
            return next(new createError(401,results.error))
        if (results.rows.length === 0)
            return next(new createError(404,"not found roles for this user!!"))
        const roleName = results.rows[0].Name;
        if (roleName !== "Admin")
            return next(new createError(401,"This is for administrators only !!"))
    }
    next()
}
module.exports = {
    verifyToken,
    verifyAdmin
};