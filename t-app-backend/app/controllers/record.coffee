Record       = require('../models/record')
Worker       = require('../models/worker')
buildingController = require('./building.coffee')

config     = require('../../config/database')
moment = require 'moment'

module.exports.addRecord = (req, res) ->
  newRecord = new Record
    id_worker  : req.body.id_worker
    id_patient : req.user._doc._id
    priority   : req.body.priority
    comment    : req.body.comment
    time_create: new Date()

  newRecord.save (err) ->
    if err
      return res.status(400).send(err.message)
    Worker.findByIdAndUpdate newRecord._doc.id_worker, { $push: { patients: newRecord }},  (err, user) ->
      if err
        res.status(400).json(success: false, message: err)

      buildingController.createPathRecord(query: {id_worker: req.body.id_worker})

      res.json(success: true, data: newRecord)

module.exports.getRecord = (req, res) ->
  Record.findOne {id_patient: req.user._doc._id}, (err, record) ->
    if err
      res.status(400).json(success: false, message: err)
    else if not record
      res.json(success: false, data: {})
    else if moment(moment() - moment(record.get("time_create"))) > moment(0).add(24, 'hour')
      record.remove()
      res.json(success: true, data: {})
    else
      res.json(success: true, data: record)

module.exports.delRecord = (req, res) ->
  Record.findOne { id_patient: req.user._doc._id }, (err, record) ->
    if err
      res.status(400).json(success: false, message: err)
    else if not record
      res.status(400).json(success: false, message: 'Record not found.')
    else
      record.time_target = "none"
      Worker.findByIdAndUpdate {_id: record._doc.id_worker}, { $pull: { patients: record }},  (err, worker) ->
        if err
          res.status(400).json(success: false, message: err)
        else
          buildingController.createPathRecord(query: {id_worker: record._doc.id_worker})

          Record.findOneAndRemove { id_patient: req.user._doc._id }, (err) ->
            if err
              res.status(400).json(success: false, message: err)
            else
              res.json(success: true, message: "delete true")
