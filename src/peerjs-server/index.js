const { PeerServer } = require('peer');

const peerServer = PeerServer({
    port: 8080,
    path: '/peerjsserver'
});

console.log('Running peerJS server @ http://localhost:8080/peerjsserver', peerServer)