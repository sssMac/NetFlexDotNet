const express = require("express");
const seriesRouter = require("./src/routes/seriesRouter");
const authRouter = require('./src/routes/authRouter')
const rolesRouter = require('./src/routes/rolesRouter')
const userRouter = require('./src/routes/userRouter')
const genreRouter = require('./src/routes/genreRouter');
const createError = require("./src/errors/createError");
const corsMiddleware = require('./middleware/corsMiddleware')
const passport = require('passport');

const VKontakteStrategy = require('passport-vkontakte').Strategy;

const app = express();

// middleware
app.use(corsMiddleware)
app.use(express.json());

const port = 5000;

// set routes..
app.use("/series", seriesRouter);
app.use("/auth", authRouter);
app.use("/roles", rolesRouter);
app.use("/user", userRouter);
app.use("/genre", genreRouter);

passport.use(new VKontakteStrategy({
        clientID:     process.env.VK_CLIENT_ID,
        clientSecret: process.env.VK_CLIENT_SECRET,
        callbackURL:  "http://localhost:5000/auth/vkontakte/callback"
    },
    function(accessToken, refreshToken, params, profile, done) {
        return done(null, profile);
    }
));

app.get(
    "/auth/vkontakte",
    passport.authenticate("vkontakte", {
        scope: ["status", "email", "friends", "notify"],
    }),
    function (req, res) {
        // The request will be redirected to vk.com for authentication, with
        // extended permissions.
    }
);

app.get('/auth/vkontakte/callback',
    passport.authenticate('vkontakte', {
        failureRedirect: '/login',
        session: false,
    }),

    function(req, res) {
        res.send(req.user);
    });

// home handler
app.get("/", (res, rep) => {
    rep.send("welcome for home");
});

// 404 error handler and pass to error handler
app.use((req, res, next) => {
    next(new createError(404, "Not Found"));
});

// error handler
app.use((err, req, res, next) => {
    res.status(err.code || 500);
    res.send({
        status: err.code,
        message: err.message,
        timestamp: Date.now(),
        path: req.originalUrl,
    });
});

app.listen(port, () => {
    console.log("Sever is now listening at port " + port);
});
