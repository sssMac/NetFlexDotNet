const { Router } = require("express");
const router = Router();
const genreController = require('../controllers/genreController')
const {verifyAdmin} = require('../../middleware/authMiddleware')
const {verifySubscription} = require("../../middleware/subsMiddleware");


router.post('/addGenre',genreController.addGenre)
router.post('/deleteGenre',genreController.deleteGenre)
router.post('/renameGenre',genreController.renameGenre)
router.get('/all',verifySubscription,genreController.getAllGenres)
module.exports = router;