Worker       = require('../models/worker')
config     = require('../../config/database')
moment = require 'moment'

module.exports.addWorker = (req, res) ->
  newWorker = new Worker
    id_doctor : req.user._doc._id
    start     : req.body.start
    end       : req.body.end
    title     : req.body.title

  newWorker.save (err) ->
    if err
      return res.status(400).send(err.message)

    res.json(success: true, data: newWorker)

module.exports.getWorker = (req, res) ->
  Worker.find {id_doctor: req.query.id_user}, (err, user) ->
    if err
      res.status(400).json(success: false, message: err)
    else if not user
      res.status(400).json(success: false, message: 'Worker not found.')
    else
      res.json(success: true, data: user)

module.exports.addPatient = (req, res) ->
  Worker.findOne {id_doctor: req.body.id_doctor}, (err, user) ->
    if err
      res.status(400).json(success: false, message: err)
    else if not user
      res.status(400).json(success: false, message: 'Worker not found.')
    else
      user.patients.push(req.body.id_patient)
      user.save (err) ->
        if err
          return res.status(400).send(err.message)

        res.json(success: true, message: 'Add patient.')

module.exports.deletePatient = (req, res) ->
  Worker.findOne {id_doctor: req.body.id_doctor}, (err, user) ->
    if err
      res.status(400).json(success: false, message: err)
    else if not user
      res.status(400).json(success: false, message: 'Worker not found.')
    else
      res.json(success: true, message: 'Deleted patient.')

module.exports.getWorkerForSelect = (req, res) ->
  start = moment().format("MM-DD-YYYY")
  end = moment().add(1, 'day').format("MM-DD-YYYY")

  Worker.find(start: $gte: new Date(start), $lt: new Date(end))
  .populate('id_doctor', ['login', 'email'])
  .exec (err, user) ->
    if err
      throw err

    if not user
      res.status(400).json(success: false, message: 'Worker not found.')
    else
      res.json(success: true, data: user)

module.exports.getWorkers = (req, res) ->
  Worker.find({})
    .populate('id_doctor', ['login', 'email'])
      .exec (err, user) =>
        if err
          res.status(400).json(success: false, message: err)
        else if not user
          res.status(400).json(success: false, message: 'Worker not found.')
        else
          res.json(success: true, data: user)
