Record       = require('../models/record')
Worker       = require('../models/worker')

config     = require('../../config/database')
moment = require 'moment'
buildingController = require('./building.coffee')


module.exports.getPatients = (req, res) ->
  start = moment().format("MM-DD-YYYY")
  end = moment().add(1, 'day').format("MM-DD-YYYY")
  Worker.findOne({id_doctor: req.user._doc._id, start: $gte: new Date(start), $lt: new Date(end)})
    .populate('patients.id_patient', ['address', 'login', '_id', 'phone'])
    .exec (err, user) =>
      if err
        res.status(400).json(success: false, message: err)
      else if not user
        res.json(success: true, data: [])
      else
        res.json(success: true, data: user._doc.patients)

module.exports.delPatients = (req, res) ->
  Record.findOne { _id: req.body.id }, (err, record) ->
    if err
      res.status(400).json(success: false, message: err)
    else if not record
      res.status(400).json(success: false, message: 'Record not found.')
    else
      Worker.findByIdAndUpdate {_id: record._doc.id_worker}, { $pull: { patients: record }},  (err, worker) ->
        if err
          res.status(400).json(success: false, message: err)
        else
          buildingController.createPathRecord(query: {id_worker: record._doc.id_worker})
          Record.findOneAndRemove { _id: req.body.id }, (err) ->
            if err
              res.status(400).json(success: false, message: err)
            else
              res.json(success: true, message: req.body.id)
