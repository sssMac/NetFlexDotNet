// this file for admin routes
const { Router } = require("express");
const episodesController = require("../controllers/episodesController");
const {verifyAdmin} = require('../../middleware/authMiddleware')
const router = Router();

router.post('/addEpisode', episodesController.addEpisode);
router.post('/deleteEpisode',episodesController.deleteEpisode)
router.post('/updateEpisode',episodesController.updateEpisode)
router.get('/all',episodesController.getAllEpisodes)


module.exports = router;


