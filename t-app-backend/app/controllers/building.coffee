Record       = require('../models/record')
Worker       = require('../models/worker')
User         = require('../models/user.coffee')
requestify = require('requestify');
config     = require('../../config/database')
moment = require 'moment'

#AIzaSyClPh8aGnZ_c0TV6lvWn4ILfLoZjiHlmwE

getHref = (address, patients) ->
  new Promise (resolve, reject) ->
    href = "https://maps.googleapis.com/maps/api/distancematrix/json?units=metric&language=ru&origins=#{address}&destinations=#{address}&mode=walking&key=AIzaSyAnJ1kiFUkb8WoRmOLz0hdUE7qRcjSV21E"

    requestify
    .get href
    .then (response) ->
      resolve response.getBody()


getMatrix = (matrix, patients) ->
  matrix.best = []
  matrix.bestTime = []
  matrix.bestPath = []
  for i in [0...matrix.rows.length]
    priority = 1
    switch patients[i]._doc.priority
      when "low"
        priority = 1
      when "middle"
        priority = 5
      when "high"
        priority = 100

    matrix.destination_addresses[i] =
      address  : matrix.destination_addresses[i]
      id       : patients[i]._doc.id_patient._doc._id.toString()
      priority : priority

    for j in [0...matrix.rows[i].elements.length]
      elem = matrix.rows[i].elements[j]

      unless i is j
        minutes = Math.round(elem.duration.value / 60)
        elem.time = minutes
        elem.feromon = 1
        elem.was = []

  return matrix


module.exports.createPathRecord = (req, res) ->
  new Promise (resolve, reject) ->
    Worker.findOne({ _id: req.query.id_worker })
      .populate('patients.id_patient', ['address', 'login', '_id'])
      .exec (err, worker) =>
        if worker._doc.patients.length < 3
          return resolve()
        patients = worker._doc.patients
        address = patients.map (item) -> "Москва,#{item._doc.id_patient._doc.address}"
        getHref(address.join('|'), patients).then (body) ->
          m = new Matan getMatrix(body, patients)
          matrix = m.solve()
          newPatients = []
          matrix.best[matrix.best.length - 1].path.forEach (item, i) ->
            newPatients.push patients[item]
            debugger
            if i is 0
              newPatients[i].set("time_target", moment(worker.get("start")).minutes(15))
            else
              matrix.best[matrix.best.length - 1].ribs.shift()
              timeRib = matrix.best[matrix.best.length - 1].ribs[0].time
              matrix.best[matrix.best.length - 1].ribs.shift()
              debugger
              newPatients[i].set("time_target", moment(newPatients[i - 1].get("time_target")).add(timeRib + 15, 'm'))

          Worker.findByIdAndUpdate {_id: req.query.id_worker}, { $set: { patients: newPatients }},  (err, worker) ->
            console.log("complete optimization")


module.exports.createPath = (req, res) ->
  Worker.findOne({ _id: req.query.id_worker })
    .populate('patients.id_patient', ['address', 'login', '_id'])
    .exec (err, worker) =>
      patients = worker._doc.patients
      address = patients.map (item) -> "#{item._doc.id_patient._doc.address}"
      getHref(address.join('|'), patients).then (body) ->
        m = new Matan getMatrix(body, patients)
        res.json(success: true, data: m.solve())


class Matan
  constructor: (matrix) ->
    @matrix = matrix

  alfa: 1
  beta: 3
  p: 0.5
  q: 50
  t: 1

  pijkt: (tau, eta, vers, indexelem) ->
    tau_eta = (t, e, index) =>
      Math.pow(t, 1) * Math.pow(1 / e, @beta)

    sum = (arr, index) =>
      summ = 0
      arr.forEach (elem) ->
        summ += tau_eta(elem.item.feromon, elem.item.time, index)

      summ


    tau_eta(tau, eta, indexelem) / sum(vers, indexelem)

  randomizer: (arr) ->
    getRandomFloat = (min, max) ->
      Math.random() * (max - min) + min

    inter = 0
    arr.forEach (item) ->
      item.min = inter
      inter += item.chance
      item.max = inter

    rand = getRandomFloat(arr[0].min, arr[arr.length - 1].max)

    elem = arr.filter (item) ->
      rand >= item.min && rand <= item.max

    elem[0]

  solve: =>
    countVer = @matrix.rows.length
    countReb = ((countVer * countVer) - countVer) / 2

    ants = []
    for iterate in [0...100]
      for i in [0...countVer]
        c = Math.floor(Math.random() * (countVer - 1)) + 0
        path = [0]
        ribs = []
        timePath = 0
        currentVar = 0

        for reb in [0...(countVer - 1)]
          canArr = []
          @matrix.rows[currentVar].elements.forEach (item, j) ->
            if (currentVar isnt j) and not (j in path) and (i isnt item.was)
              canArr.push
                index: j
                item: item

          canElem = canArr[0]

          if Math.floor(Math.random() * (countVer - 1)) + 0 < countVer / 2
            canArr.forEach (elem) ->
              if elem.item.feromon > canElem.item.feromon then canElem = elem
          else
            chanceArr = []
            canArr.forEach (elem, indexCanElem) =>
              chanceArr.push
                index: indexCanElem
                chance: (@pijkt elem.item.feromon, elem.item.time, canArr, elem.index) * 100

            chanceElem = @randomizer(chanceArr)
            canElem = canArr[chanceElem.index]

          timePath += @matrix.rows[currentVar].elements[canElem.index].time

          @matrix.rows[currentVar].elements[canElem.index].was.push i
          @matrix.rows[canElem.index].elements[currentVar].was.push i

          ribs.push @matrix.rows[currentVar].elements[canElem.index]
          ribs[ribs.length - 1].feromon =
            ((1 - @p) * ribs[ribs.length - 1].feromon) + (@p * @t)

          ribs.push @matrix.rows[canElem.index].elements[currentVar]
          ribs[ribs.length - 1].feromon = ribs[ribs.length - 2].feromon

          path.push canElem.index
          currentVar = canElem.index

        ants.push
          path: path
          ribs: ribs
          timePath: timePath

      bestAnt = ants[0]
      ants.forEach (elem) ->
        if elem.timePath < bestAnt.timePath then bestAnt = elem

      @matrix.bestTime.push(bestAnt.timePath)
      @matrix.bestPath.push(bestAnt.path)

      #@matrix.best.push bestAnt

      for reb in [(bestAnt.ribs.length - 1)..0]
        feromon = bestAnt.ribs[reb].feromon
        sum = 0
        bestAnt.ribs[reb].was.forEach (item) =>
          sum += (@q / ants[item].timePath)

        bestAnt.ribs[reb].feromon = @p * feromon + sum

    @matrix.rows.forEach (elem) ->
      elem.elements.forEach (item) ->
        if item.was then item.was = undefined


    @matrix.bestTime = @matrix.bestTime.join(", ")
    @matrix.bestPath = @matrix.bestPath.join(", ")
    @matrix
