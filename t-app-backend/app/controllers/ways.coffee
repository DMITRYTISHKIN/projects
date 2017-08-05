Record       = require('../models/record')
Worker       = require('../models/worker')

config     = require('../../config/database')
moment = require 'moment'


module.exports.getWays = (req, res) ->
  start = moment().format("MM-DD-YYYY")
  end = moment().add(1, 'day').format("MM-DD-YYYY")
  Worker.findOne({id_doctor: req.user._doc._id, start: $gte: new Date(start), $lt: new Date(end)})
    .populate('patients.id_patient', ['address', 'login', '_id', 'latLng'])
    .exec (err, user) =>
      if err
        res.status(400).json(success: false, message: err)
      else if not user
        res.json(success: true, data: [])
      else
        res.json(success: true, data: user._doc.patients)
