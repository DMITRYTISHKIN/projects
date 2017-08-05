JwtStrategy = require('passport-jwt').Strategy
ExtractJwt  = require('passport-jwt').ExtractJwt
User        = require('../app/models/user')
config      = require('../config/database')

module.exports = (passport) ->
  opts = {}
  opts.secretOrKey = config.secret
  opts.jwtFromRequest = ExtractJwt.fromAuthHeader()

  passport.use(new JwtStrategy(opts, (jwt_payload, done) ->
    User.findOne {_id: jwt_payload._doc._id}, (err, user) ->
          if err
              done(err, false)
          if user
              done(null, user)
          else
              done(null, false)
    )
  )
