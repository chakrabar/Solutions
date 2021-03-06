# zoom clone

## A simple app for peer to peer video calling

1. Video chat with friend(s)
2. Text message in group
3. Text messaging is done with `WebSocket`, using `socket.io` library
4. Video streaming is done as peer-to-peer transmission through `WebRTC`, using `peer.js` library
5. A `socket.io` server, based on `node.js` and `express` is used for initiating connections and passing text messages. A `peer.js` server is used for peer-to-peer connection setup
6. The whole setup (socket-node server, and a peerjs server) is hosted on `Linux` based `Azure App Service`

The basic infrastructure was referred from this great [YouTube tutorial](https://www.youtube.com/watch?v=DvlyzDZDEq4) on WebRTC using Peer.js

## To run

1. `npm install`
2. Globally install peerjs `npm install peer -g`
3. Run peerjs server `peerjs --port 3001` (a custom perJS server might be run at, say http://localhost:3001/peerjsserver). Note, setup this peerjs server connection details in `script.js` which will be used at client front-end. Or see PeerJS server [docs](https://peerjs.com/docs.html#start) for other options
4. Browse to http://localhost:3000
5. Copy generated URI and open another tab/window of the browser and paste URI
6. It **currently works only on** `Chrome` or `Chromium base Edge`, but not on both, in same client, at same time

## Tips to successfully deploy to Azure App Service

1. Create a new App Service
2. Select `LINUX` as the `OS`
3. Select `Node` as the platform
4. Select compatible version of node like `NODE 12 LTS`
5. Make sure app code is configured to run at `port: 8080`
6. Make sure main entry file is correct in `package.json` like `"main": "index.js"`
7. When `peerJS server` is secured (https), configure accordingly in client JS code ([doc](https://peerjs.com/docs.html#start))
8. Now, deploy to the App Service (can be done easily from `VS Code` with `Azure App Service` extension)

> **NOTE:** Apparently, `WebRTC` connection is quite unreliable over Mobile (4G) connections! What's even more weird, this problems on mobile-to-pc connections seem to increase when 'controls' are added to 'video' elements. And the reliability falls fast with increasing distance, mixed networks!!! **Finally,** The only setup where it works within usable standards is, on Chrome (desktop or mobile), when clients are on same (WAN/LAN) network, like using the same wifi connection!

## TURN server

Oh a high level, a `WebRTC` connection needs help of a `STUN` and/or `TURN` server to work

STUN: At core, it helps the clients to figure out their public IP, so that other clients to can connect to them when invited. Then the signalling process happens that inlucdes content format negotiation and other agreements.

TURN: Even after using STUN, sometime clients cannot connect to each other for `Symmetric NAT` and other restrictions. In such cases TURN server can help by working as a proxy server in between, so that the clients can pass data through them. See more details [here](https://developer.mozilla.org/en-US/docs/Web/API/WebRTC_API).

The current code does not work across networks. There are some discussions in StackOverflow [here](https://stackoverflow.com/questions/43992334/why-my-webrtc-connection-works-only-at-local-network), [here](https://stackoverflow.com/questions/25546098/installing-a-turn-server-on-ubuntu-for-webrtc) and [here](https://stackoverflow.com/questions/22233980/implementing-our-own-stun-turn-server-for-webrtc-application/35452566#35452566).

The [PeerJS docs](https://peerjs.com/docs.html) says the PeerJS [cloud server](https://peerjs.com/) provides a free TURN server by default (just use `new Peer()` in code), so it should solve the problem. But that did not work either!