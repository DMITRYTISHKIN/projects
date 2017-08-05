User       = require('../models/user')
jwt = require('jsonwebtoken')
config     = require('../../config/database')


module.exports.getUsers = (req, res) ->
  debugger
  User.find({})
    .exec (err, user) =>
      if err
        res.status(400).json(success: false, message: err)
      else if not user
        res.json(success: true, data: [])
      else
        res.json(success: true, data: user)


module.exports.login = (req, res) ->
  User.findOne({email: req.body.email}, (err, user) ->
    if err
      throw err

    if not user
      res.status(400).json({ success: false, message: 'Authentication failed. User not found.' });
    else
      user.comparePassword(req.body.password, (err, isMatch) =>
        if isMatch and not err
          token = jwt.sign(user, config.secret, {expiresIn: 10080})
          res.json({ success: true, token: "JWT #{token}", user: user })
        else
          res.status(400).json({ success: false, message: 'Authentication failed. Passwords did not match.' });
      )

  )

module.exports.check = (req, res) ->
  res.json(success: true, data: req.user)


module.exports.register = (req, res) ->
  if not req.body.email or not req.body.password or not req.body.login
    return res.status(400).json(success: false, message: 'Please enter email and password.')
  else
    newUser = new User
      login    : req.body.login
      email    : req.body.email
      password : req.body.password
      addressfull  : req.body.addressfull
      address  : req.body.address
      latLng   : req.body.latLng
      phone    : req.body.phone

    newUser.save (err) ->
      if err
        return res.status(400).send(err.message)

      res.json(success: true, message: 'Successfully created new user.')
