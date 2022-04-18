const { Router } = require("express");
const {check} = require("express-validator");
const router = Router();
const authController = require('../controllers/authController')
const {verifyAdmin} = require('../../middleware/auth')

router.post('/registration', [
    check('email', "Uncorrect email").isEmail(),
    check('password', 'Password must be longer than 7 and shorter than 12').isLength({min: 7, max: 12})
],authController.registration)

router.post('/login',authController.login)

router.post('/block',verifyAdmin,authController.blockUser)

module.exports = router;