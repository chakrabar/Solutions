const { PeerServer } = require('peer');

const peerServer = PeerServer({
    port: 3001,
    path: '/peerjsserver'
});

console.log('Running peerJS server @ http://localhost:3001/peerjsserver')