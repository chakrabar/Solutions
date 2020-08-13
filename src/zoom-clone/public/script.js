const socket = io('/') // the socket server is availabel at root path of same server

const myVideoGrid = document.querySelector('#video-grid') // get reference to out div element

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
const peerCalls = {}
// a mapping of connected peers ids and their usernames
const peerUsers = {}
let vidChatOptions = {
    myUserId: null, // peerjs & socket.io userid for me
    myName: null, // my username
    sharingChoice: null, // video OR screen share,
    roomId: null
}
let myPeer; // the peerJS object to handle calls
const videoStreamConstraints = {
    video: {
        width: { exact: 270 },
        height: { exact: 270 },
        frameRate: {
            ideal: 10,
            max: 15
        }
    },
    audio: true
};
const screenShareConstraints = {
    video: {
        cursor: 'always'
    },
    audio: {
        echoCancellation: true,
        noiseSuppression: true,
        sampleRate: 44100
    }
};

// method to start peerJS call, video OR screen sharing
const startCall = (myStream) => {
    // show my own video stream on a video element
    addVideoStream(myVideo, myStream)

    // if get a call from peerjs peer, answer the call with own stream
    myPeer.on('call', (call) => {
        const callingPeerId = call.peer
        console.log('Got call from: ', callingPeerId)
        if (!peerUsers[callingPeerId]) {
            // query username for peer
            socket.emit('user-query', callingPeerId)
        }
        call.answer(myStream)
        // when peer starts to stream, show their video
        const peerVideo = createVidElement(); // document.createElement('video')
        call.on('stream', (peerVideoStream) => {
            addVideoStream(peerVideo, peerVideoStream)
        })
    })

    // when another user joins the same socket room => add them to call
    socket.on('user-connected', (peerUserId, peerUsername) => {
        console.log('Peer connected: ', peerUserId, peerUsername)
        peerUsers[peerUserId] = peerUsername
        connectToNewUser(peerUserId, myStream)
    })

    // peer user query result
    socket.on('user-query', (queryUserId, queryUsername) => {
        console.info('user-query', queryUserId, queryUsername)
        peerUsers[queryUserId] = queryUsername
    })
}

// start a video sharing call
const startScreenShareCall = () => {
    navigator.mediaDevices.getDisplayMedia(screenShareConstraints)
    .then(startCall)
    .catch(err => { console.error('Error: ' + err); return null; })
}

// start a screen sharing call
const startVideoCall = () => {
    // this is a problem in different browsers, and docs does not help much
    // start my own stream by capturing my device audio+video and get ready for calls
    // navigator.mediaDevices.getUserMedia
    navigator.mediaDevices.getUserMedia(videoStreamConstraints)
    .then(startCall)
    .catch(err => { console.error('Error: ' + err); return null; })
}

// THIS IS THE MAIN METHOD TO ACTUALLY START (VIDEO/SCREEN SHARING) CALL
const initiateCallAndChat = () => {
    try {    
    
        if (vidChatOptions.sharingChoice === 'video') {
            startVideoCall()
        } else { // 'screen'
            startScreenShareCall()
        }

        // when a user disconnects, close the corresponding call (channel)
        // this will trigger the onclose of the call and remove the video
        socket.on('user-disconnected', (peerUserId) => {
            console.log('Peer disconnected: ' + peerUserId)
            if (peerCalls[peerUserId]) {
                peerUsers[peerUserId] = null
                peerCalls[peerUserId].close()
            }
        })
    
        // NEW chat message
        socket.on('chat-message', (peerUserId, message) => {
            const peerName = peerUsers[peerUserId]
            appendChatMessage(peerName, message)
        })

        // IMP: NOTE: Following setup currently works only in Chrome and Chrome based Edge
        // ALSO NOTE: It does NOT work for Chrome & Edge at the same time, for client on same machine
        myPeer = new Peer(undefined, { // ID is kept undefind for peerjs server to create
            host: 'peerjs-webrtc.azurewebsites.net', // back-end peer server host
            // port: 3001, // peer server port (http://localhost:3001/peerjsserver) (https://peerjs-webrtc.azurewebsites.net/peerjsserver)
            path: 'peerjsserver',
            secure: true // https://peerjs.com/docs.html#start
        }) // peerjs will take all the WebRTC info from browser, and relate that to newly created ID
    
        // as soon as it connects to peerjs server (above), it'll come back with it's own ID
        myPeer.on('open', (id) => { // and we're gonna use this id as our standard user ID
            // we could very well have used socket.id as userId, but we'll need peerjs ids later anyway
            // now
            // join socket room with roomId & userId
            vidChatOptions.myUserId = id;
            vidChatOptions.roomId = ROOM_ID
            sessionStorage.setItem('vidChatOptions', JSON.stringify(vidChatOptions))
            console.info('join-room', ROOM_ID, id, vidChatOptions.myName)
            socket.emit('join-room', ROOM_ID, id, vidChatOptions.myName)
        })
    } catch (error) {
        console.error(error)
        alert(`Unknown error: ${error.message}`)
    }
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
    peerCalls[peerUserId] = call
}

const appendChatMessage = (user, message) => {
    const chatDisplay = document.querySelector('#chat-display')

    const msgSpan = document.createElement('span')
    msgSpan.innerHTML = `${user}: ${message}`
    chatDisplay.appendChild(msgSpan)
}

// FOR TEXT CHAT, through socket.io
const sendText = (e) => {
    e.preventDefault();

    const message = document.querySelector('#msg-txt').value
    appendChatMessage('me', message)
    document.querySelector('#msg-txt').value = ''

    socket.emit('chat-message', vidChatOptions.myUserId, message)
}

const setupStuffsAndInitCall = () => {
    const nameLabel = document.querySelector('#name-label')
    nameLabel.innerHTML = `${vidChatOptions.myName}, sharing ${vidChatOptions.sharingChoice}`

    document.querySelector('#options-section').style.display = 'none'
    document.querySelector('#chat-section').style.display = 'block'
    document.querySelector('#video-grid').style.display = 'block'

    const form = document.getElementById('chat-form')
    form.addEventListener('submit', sendText)

    initiateCallAndChat()
}

// SETUP & INITIATE CALL
let sessionData = sessionStorage.getItem('vidChatOptions')
if (sessionData) {
    sessionData = JSON.parse(sessionData)
    if (sessionData && sessionData.roomId === ROOM_ID) {
        console.info('session data', sessionData)
        vidChatOptions = sessionData
        setupStuffsAndInitCall()
    }    
}
const start = () => {
    const nameText = document.querySelector('#name')
    if (!nameText.value) {
        alert('Enter your name')
        nameText.focus()
    } else {        
        vidChatOptions.myName = nameText.value
        vidChatOptions.sharingChoice = document.querySelector('input[name="vidoption"]:checked').value

        setupStuffsAndInitCall()
        
    }
}