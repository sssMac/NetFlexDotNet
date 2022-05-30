const subsQueries = require("../src/queries/subscriptionQueries");
const userQueries = require("../src/queries/userQueries");
const createError = require("../src/errors/createError");
const db = require("../src/database/db");
const userService = require("../src/service/userService");

async function verifySubscription(req, res, next) {
    const accessToken = req.headers.authorization.split(' ')[1]
    if (!accessToken) {
        return next(new createError(401, 'Token not found'))
    }
    const results = await db.query(userQueries.getUserIdByToken, [accessToken]);

    if (results.rows.length === 0)
        return next(new createError(401, 'Auth error'))

    const userId = results.rows[0].UserId;
    if (!userId)
        return next(new createError(400, 'User Not Found!'));

    const subsType = await db.query(subsQueries.getSubsType, [userId]).then(value => {
        return value.rows[0].Name;
    })

    const role = await userService.getRole(userId);

    if (role[0].Name === "Admin")
        return next();

    if (subsType === 'F')
        return next(new createError(401, 'You must subscribe to continue'));

    const path = req.baseUrl;

    if (subsType === 'S') {
        if (path === '/review')
            return next(new createError(401, `Your subscription '${subsType}' does not have permission for this`));
    }
    return next();
}

module.exports = {
    verifySubscription
};

/**
 *
 * F - даётся по дефолту, нельзя впринцепи зайти на сайт и смотреть фильмы и скриалы
 * S - можно смотреть фильмы и сериалы, нельзя писать рецензии
 * SS - можно смотреть и писать рецензии
 *
 */