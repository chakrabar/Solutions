var app = require('express')();
var http = require('http').createServer(app);
var io = require('socket.io')(http);

// pages
app.get('/', (req, res) => {
    res.sendFile(__dirname + '/index.html');
});
app.get('/hello', (req, res) => {
    res.send('<h1>Hello world</h1>');
});

// socket.io
io.on('connection', (socket) => {
    console.log('a user connected');
    socket.on('disconnect', () => {
        console.log('user disconnected');
        io.emit('user-left', 'Loser left'); // to test
    });
    socket.on('chat-message', (msg) => {
        console.log('message: ' + msg);
        // socket.broadcast.emit('this guy says hi to others...');
        io.emit('chat-message', socket.id + ': ' + msg); // broadcast to all connected clients
    });
});

// start server
http.listen(3000, () => {
    console.log('listening on *:3000');
});