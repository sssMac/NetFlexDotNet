const { Router } = require("express");
const router = Router();
const rolesController = require('../controllers/rolesController')
const {verifyAdmin} = require('../../middleware/authMiddleware')
require("express");

router.post('/createRole',verifyAdmin,rolesController.createRole)

router.post('/removeRole',verifyAdmin,rolesController.removeRoles)

router.post('/assignRole',verifyAdmin,rolesController.assignRole)

router.get('/all',verifyAdmin,rolesController.all)

router.post('/getRole',verifyAdmin,rolesController.getRole)

module.exports = router;