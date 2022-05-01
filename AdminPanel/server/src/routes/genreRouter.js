const { Router } = require("express");
const router = Router();
const genreController = require('../controllers/genreController')
const {verifyAdmin} = require('../../middleware/authMiddleware')


router.post('/addGenre',genreController.addGenre)
router.post('/deleteGenre',genreController.deleteGenre)
router.post('/renameGenre',genreController.renameGenre)

module.exports = router;