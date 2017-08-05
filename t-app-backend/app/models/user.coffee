config     = require('../../config/database')
mongoose = require 'mongoose'
Schema = mongoose.Schema
bcrypt = require 'bcrypt'

mongoose.connect(config.database)


UserSchema = new mongoose.Schema
  login:
    type: String
    required: true
    unique: false

  email:
    type: String
    lowercase: true
    unique: true
    required: true

  password:
    type: String
    required: true

  role:
    type: String
    enum: ['patient', 'doctor', 'admin']
    default: 'patient'

  address:
    type: String
    required: true

  latLng:
    type: Object
    required: true

  addressfull:
    type: String
    required: true

  phone:
    type: String
    required: true


UserSchema.pre 'save', (next) ->
  user = @
  if @isModified('password') or @isNew
      bcrypt.genSalt 10, (err, salt) ->
          if err then return next(err)

          bcrypt.hash user.password, salt, (err, hash) ->
              if err then next(err)

              user.password = hash
              next()
              return
  else
    next()


UserSchema.methods.comparePassword = (passw, cb) ->
    bcrypt.compare passw, @password, (err, isMatch) ->
        if err
          cb(err)

        cb(null, isMatch)


module.exports = mongoose.model('User', UserSchema)
