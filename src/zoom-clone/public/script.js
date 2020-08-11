const socket = io('/') // the socket server is availabel at root path of same server

const myVideoGrid = document.querySelector('#video-grid') // get reference to out div element

// IMP: NOTE: Following setup currently works only in Chrome and Chrome based Edge
// ALSO NOTE: It does NOT work for Chrome & Edge at the same time, for client on same machine
const myPeer = new Peer(undefined, { // ID is kept undefind for peerjs server to create
    host: 'peerjs-webrtc.azurewebsites.net', // back-end peer server host
    // port: 3001, // peer server port (http://localhost:3001/peerjsserver) (https://peerjs-webrtc.azurewebsites.net/peerjsserver)
    path: 'peerjsserver',
    secure: true // https://peerjs.com/docs.html#start
}) // peerjs will take all the WebRTC info from browser, and relate that to newly created ID

const createVidElement = () => {
    const vid = document.createElement('video')
    // vid.className = 'video-box'
    vid.setAttribute('controls', 'controls')
    vid.setAttribute('autoplay', 'autoplay')
    return vid;
}

const myVideo = createVidElement(); // document.createElement('video') // create a video element
myVideo.muted = true // make this video muted, that is it PLAYS without sound (video can record sound)

// a mapping of connected peers and their peerjs call object
const peers = {}
let myUserId;

try {
    // this is a problem in different browsers, and docs does not help much
    // start my own stream by capturing my device audio+video and get ready for calls
    const getUserMedia = navigator.mediaDevices.getUserMedia
    getUserMedia({
        video: {
            width: { exact: 270 },
            height: { exact: 270 },
            frameRate: {
                ideal: 10,
                max: 15
            }
        },
        audio: true
    }).then((myStream) => {
        // show my own video stream on a video element
        addVideoStream(myVideo, myStream)

        // if get a call from peerjs peer, answer the call with own stream
        myPeer.on('call', (call) => {
            console.log('Got call from: ')
            call.answer(myStream)
            // when peer starts to stream, show their video
            const peerVideo = createVidElement(); // document.createElement('video')
            call.on('stream', (peerVideoStream) => {
                addVideoStream(peerVideo, peerVideoStream)
            })
        })

        // when another user joins the same socket room => add them to call
        socket.on('user-connected', (peerUserId) => {
            connectToNewUser(peerUserId, myStream)
        })
    })

    // when a user disconnects, close the corresponding call (channel)
    // this will trigger the onclose of the call and remove the video
    socket.on('user-disconnected', (peerUserId) => {
        console.log('Peer disconnected: ' + peerUserId)
        if (peers[peerUserId]) {
            peers[peerUserId].close()
        }
    })

    // NEW chat message
    socket.on('chat-message', (peerUserId, message) => {
        appendChatMessage(peerUserId, message)
    })

    // as soon as it connects to peerjs server (above), it'll come back with it's own ID
    myPeer.on('open', (id) => { // and we're gonna use this id as our standard user ID
        // we could very well have used socket.id as userId, but we'll need peerjs ids later anyway
        // now
        // join socket room with roomId & userId
        myUserId = id;
        socket.emit('join-room', ROOM_ID, id)
    })
} catch (error) {
    console.error(error)
    alert(`Unknown error: ${error.message}`)
}

// get a video element, a stream & and an outer wrapper for the video element
// put the stream as source for the video element, and start playing when ready
const addVideoStream = (videoEl, stream) => {
    // videoEl.srcObject = stream
    if (videoEl.mozSrcObject !== undefined) { // FF18a
        videoEl.mozSrcObject = stream;
    } else if (videoEl.srcObject !== undefined) {
        videoEl.srcObject = stream;
    } else { // FF16a, 17a
        videoEl.src = stream;
    }
    videoEl.addEventListener('loadedmetadata', () => {
        videoEl.play()
    })
    myVideoGrid.append(videoEl)
    videoEl.load() // not sure if this is required
}

// call the new user with my own stream, show their video when they start to stream
const connectToNewUser = (peerUserId, myStream) => {
    // call the peer with my stream
    const call = myPeer.call(peerUserId, myStream)
    // show their video when they start to stream
    const peerVideo = createVidElement(); // document.createElement('video')
    call.on('stream', (peerVideoStream) => {
        console.log('Peer started to stream: ' + peerUserId)
        addVideoStream(peerVideo, peerVideoStream)
    })
    // when that call closes, remove their video
    call.on('close', () => {
        // peerVideo.remove()
        peerVideo.parentNode.removeChild(peerVideo);
    })

    // keep reference to call for this peer, so that can be disconnected later
    peers[peerUserId] = call
}

const appendChatMessage = (user, message) => {
    const chatDisplay = document.querySelector('#chat-display')
    
    const msgSpan = document.createElement('span')
    msgSpan.innerHTML = `${user}: ${message}`
    chatDisplay.appendChild(msgSpan)
}

const sendText = (e) => {
    e.preventDefault();

    const message = document.querySelector('#msg-txt').value
    
    appendChatMessage('me', message)

    document.querySelector('#msg-txt').value = ''
    
    socket.emit('chat-message', myUserId, message)
}

const form = document.getElementById('chat-form');
form.addEventListener('submit', sendText);