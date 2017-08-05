require('coffee-script')

express    = require('express')
app        = express()
mongoose   = require('mongoose')
mongoose.Promise = global.Promise;
bodyParser = require('body-parser')
server     = require('http').Server(app)
passport   = require('passport')
config     = require('./config/database')
io         = require('socket.io')(server)
jwt = require('jsonwebtoken')
port       = 4001
cors       = require('cors')

userController = require('./app/controllers/user.coffee')
workerController = require('./app/controllers/worker.coffee')
recordController = require('./app/controllers/record.coffee')
buildingController = require('./app/controllers/building.coffee')
dashboardController = require('./app/controllers/dashboard.coffee')
waysController = require('./app/controllers/ways.coffee')



app.use cors()

app.use bodyParser.urlencoded
  extended: false

app.use bodyParser.json()


mongoose.createConnection(config.database)

app.use passport.initialize()
require('./config/passport')(passport)
apiRoutes = express.Router()


apiRoutes.post '/register', userController.register
apiRoutes.post '/authenticate', userController.login
apiRoutes.get '/check', passport.authenticate('jwt', { session: false }), userController.check
apiRoutes.get '/users', passport.authenticate('jwt', { session: false }), userController.getUsers

apiRoutes.post '/scheduler', passport.authenticate('jwt', { session: false }), workerController.addWorker
apiRoutes.get '/scheduler', passport.authenticate('jwt', { session: false }), workerController.getWorker

apiRoutes.get '/workers', passport.authenticate('jwt', { session: false }), workerController.getWorkerForSelect
apiRoutes.get '/workers/all', passport.authenticate('jwt', { session: false }), workerController.getWorkers

apiRoutes.get '/record', passport.authenticate('jwt', { session: false }), recordController.getRecord
apiRoutes.post '/record', passport.authenticate('jwt', { session: false }), recordController.addRecord
apiRoutes.delete '/record', passport.authenticate('jwt', { session: false }), recordController.delRecord

apiRoutes.get '/dashboard', passport.authenticate('jwt', { session: false }), dashboardController.getPatients
apiRoutes.delete '/dashboard', passport.authenticate('jwt', { session: false }), dashboardController.delPatients

apiRoutes.get '/ways', passport.authenticate('jwt', { session: false }), waysController.getWays


apiRoutes.get '/building', buildingController.createPath


app.use('/api', apiRoutes)



app.get '/', (req, res) ->
  res.send 'Hello World!!!'

app.listen port, () ->
  console.log 'app listening on port ' + port + '!'

app.use -> (req, res, next) ->
  res.status "404"
  res.json
    error: 'Not found'

app.use -> (err, req, res, next) ->
  res.status err.status or "404"
  res.json
    error: err.message


module.export = app
