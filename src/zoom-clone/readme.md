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

## Tips to successfully deploy to Azure App Service

1. Create a new App Service
2. Select `LINUX` as the `OS`
3. Select `Node` as the platform
4. Select compatible version of node like `NODE 12 LTS`
5. Make sure app is configured to run at `port: 8080`
6. Make sure main entry file is correct in `package.json` like `"main": "index.js"`
7. When `peerJS server` is secured (https), configure accordingly in client JS ([doc](https://peerjs.com/docs.html#start))
8. Deploy to the App Service (can be done from `VS Code` with `Azure App Service` extension)