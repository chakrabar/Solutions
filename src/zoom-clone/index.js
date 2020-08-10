const express = require('express') // the express module
const app = express() // app is the main express function
const server = require('http').Server(app) // create a http server with app as the main function
const io = require('socket.io')(server) // connect socket.io with the server
const { v4: uuidV4 } = require('uuid') // use v4 function from uuid module

app.set('view engine', 'ejs') // use EJS as the express view engine
app.use(express.static('public')) // keep static files in folder 'public', using express middleware

// set up app fucntion for the server
// the routes OR response for GET requests, for different paths
app.get('/', (req, res) => {
    res.redirect(`/${uuidV4()}`) // generate a new room id and redirect to parameterized page
})
app.get('/:room', (req, res) => { // parameterized room page (basically still the home page)
    // render view and pass roomId as data i.e. basically pass template & data
    res.render('room', { roomId: req.params.room })
})

// setup socket.io
io.on('connection', (socket) => {
    // let new users connect them to a room (a specific user for a specific room)
    socket.on('join-room', (roomId, userId) => {
        console.log(roomId, userId)
        socket.join(roomId)
        // let other members in the room know a new user has connected
        socket.to(roomId).broadcast.emit('user-connected', userId)
        // let other users in a room know when a user disconnects from socket server
        socket.on('disconnect', () => {
            socket.to(roomId).broadcast.emit('user-disconnected', userId)
        })
    })
})

// start the server
server.listen(8080)

// to start a gloabl peerjs server: peerjs --port 3001
// or run a separate custom peerJS server