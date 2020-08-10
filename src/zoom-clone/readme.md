# zoom clone

## A simple app for peer to peer video calling

Reference: This great [YouTube tutorial](https://www.youtube.com/watch?v=DvlyzDZDEq4)

## To run

1. `npm install`
2. Globally install peerjs `npm install peer -g`
3. Run peerjs server `peerjs --port 3001` (a custom perJS server might be run at, say http://localhost:3001/peerjsserver)
4. Browse to http://localhost:3000
5. Copy generated URI and open another tab/window of the browser and paste URI
6. Currently it works only on `Chrome` or `Chromium base Edge`, but not on both on same client at same time