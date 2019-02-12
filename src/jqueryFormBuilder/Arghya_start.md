Go to `tools` directory

#### Global installation

It's recommended to install packages locally as it is easier to maintain different versions and update independently.

npm install webpack -g

npm i -g webpack-cli

#### Local installation

npm install --save-dev webpack

npm install --save-dev webpack-cli

#### Build

Go to `tools` directory and run `webpack`. You may get errors

`npm link webpack`
^ Every time it says "run if error says "cannot find webpack""

`npm install autoprefixer`
^ Every time it says "cannot find module X" => run "npm install X"
e.g. Error: Cannot find module 'compression-webpack-plugin' => npm install compression-webpack-plugin

npm install babel-loader
npm install eslint-loader
npm install babel-core


## Build - the right way

* Delete `node_modules` directory
* Delete `package-lock.json` file
* Run `npm install` on root directory
* Go to `/tools` and run `webpack`