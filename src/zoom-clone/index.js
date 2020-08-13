const express = require('express') // the express module
const app = express() // app is the main express function
const server = require('http').Server(app) // create a http server with app as the main function
const io = require('socket.io')(server) // connect socket.io with the server
const { v4: uuidV4 } = require('uuid') // use v4 function from uuid module

app.set('view engine', 'ejs') // use EJS as the express view engine
app.use(express.static('public')) // keep static files in folder 'public', using express middleware

// set up app fucntion for the server
// the routes OR response for GET requests, for different paths
app.get('/', (req, res) => { // when a request comes to home page (there's no such page actually)
    res.redirect(`/${uuidV4()}`) // generate a new room id and redirect to parameterized page
})
app.get('/:room', (req, res) => { // parameterized room page (basically still the home page)
    // render view and pass roomId as data i.e. basically concept of pass template & data
    res.render('room', { roomId: req.params.room })
})

// mapping of userId vs username, may need in future
const users = {}

// setup socket.io
io.on('connection', (socket) => {
    // let new users connect them to a room (a specific user and a specific room)
    socket.on('join-room', (roomId, userId, username) => {
        console.log(roomId, userId, username)
        users[userId] = username

        // TODO: We are now using username for userId, it may have conflict & other issues
        socket.join(roomId)
        // let other members in the room know a new user has connected
        socket.to(roomId).broadcast.emit('user-connected', userId, username)
        // let other users in a room know when a user disconnects from socket server
        socket.on('disconnect', () => {
            socket.to(roomId).broadcast.emit('user-disconnected', userId)
            users[userId] = undefined
        })
        // for NEW text message, forward message to other users in the room
        socket.on('chat-message', (userId, message) => {
            socket.to(roomId).broadcast.emit('chat-message', userId, message)
        })
        // when a connection queries for an user id
        socket.on('user-query', (queryUserId) => {
            socket.emit('user-query', queryUserId, users[queryUserId]) // reply to that connection, with username
        })
    })
})

// start the server
server.listen(8080)

// to start a local peerjs server: peerjs --port 3001
// or run a separate custom peerJS server, see readme