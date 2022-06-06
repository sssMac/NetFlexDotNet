// this file for admin routes
const { Router } = require("express");
const seriesController = require("../controllers/seriesController");
const {verifyAdmin} = require('../../middleware/authMiddleware')
const router = Router();

router.post("/addSerial", seriesController.addSerial);
// router.post("/removeSerial", seriesController.removeSerial);
// router.post("/editSerial", seriesController.editSerial);
router.post('/addSerial',seriesController.addSerial)
router.post('/deleteSerial',seriesController.deleteSerial)
router.post('/renameSerial',seriesController.renameSerial)
router.get('/all',seriesController.getAllSerials)


module.exports = router;


