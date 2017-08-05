config     = require('../../config/database')
mongoose = require 'mongoose'
Schema = mongoose.Schema
bcrypt = require 'bcrypt'

priorities = ['low', 'middle', 'high']

UserSchema = new mongoose.Schema
  id_patient:
    type: Schema.Types.ObjectId
    ref: 'User'
    required: true

  id_worker:
    type: String
    required: true

  priority:
    type: String
    default: 'low'
    required: true

  comment:
    type: String

  time_create:
    type: Date
    required: true

  time_target:
    type: Date

UserSchema.pre 'save', (next) ->
  record = @
  next()


module.exports = mongoose.model('Record', UserSchema)
