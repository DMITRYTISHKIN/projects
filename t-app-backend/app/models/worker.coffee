config     = require('../../config/database')
mongoose = require 'mongoose'
Schema = mongoose.Schema
bcrypt = require 'bcrypt'

UserSchema = new mongoose.Schema
  id_doctor:
    type: Schema.Types.ObjectId
    ref: 'User'
    required: true

  estimated_time:
    type: Date

  patients: [
    {
      id_worker:
        type: String

      id_patient:
        type: Schema.Types.ObjectId
        ref: 'User'

      priority:
        type: String

      comment:
        type: String

      time_create:
        type: Date

      time_target:
        type: Date
    }
  ]

  start:
    type: Date
    required: true

  end:
    type: Date
    required: true

  title:
    type: String


UserSchema.pre 'save', (next) ->
  worker = @
  next()

module.exports = mongoose.model('Worker', UserSchema)
