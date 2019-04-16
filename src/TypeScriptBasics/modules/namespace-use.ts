// weird way to reference a namespace..!!!
/// <reference path="namespaces.ts" />
const six = Utilities.Numbers.double(3); // now the namespace & members are available

// namespaces are supported in browser by adding script tag for all the required JS files
// BUT, NAMESPACES are NOT SUPPORTED IN NODE.JS, it does not know how to find the required namespace files
// this can be fixed though, by creating just one js file with --outFile tsc compiler option

// on the other hand
// MODULES ARE SUPPORTED in BOTH NODE.JS & BROWSER (directly in ES6, or through loaders like requireJS/commonJS)

// === MODULES ===
// modules are better modern standard
// modules are structured simply by directories and files => each file is an module

// in brief,  ==> ==> ==> I'm not gonna use namespace and confuse myself and other fellow developers!

// === A WORD ABOUT TYPESCRIPT MODULES ===

// TypeScript does not have a runtime of it's own. So, wahtever modules are created, a module loader is required
// for those modules to work correctly at runtime.

// if "module": "commonjs" => it creates CommonJS style modules which are natively supported in Node.js and also
// works fine in VS Code debugging environment.
// but it does not work well in browser. (things like http://browserify.org/ may do the trick)

// if "module": "amd" => it creates AMD style modules, which can be used in browser with a module loader like
// requirejs. Download from [here](https://requirejs.org/docs/download.html) and then use like this in HTML
// <script data-main="scripts/main" src="scripts/require.js"></script> where main.js is entry point js for app
// require.js would load main.js once require is loaded completely. ==> Inside main.js, 
// you could use something like: var functions_1 = require("./modules/other"); to load other.js module etc.
// AMD modules does not work in Node.js out-of-the-box, but can be used via https://github.com/jrburke/amdefine

// other option here for browser based usage is => use --outFile tsc compiler option to output
// a single JS file, which will work fine in browser. But, well, then it's not modules anymore :)

// AMD style (in BROWSER via require.js) <== originally derived from CommonJS :p
// define(["require", "exports"], function (require, exports) { ... }

// CommonJS style (natively in NODE.JS)
// Object.defineProperty(exports, "__esModule", { value: true }); ...

// One major difference is, AMD supports asynchronous (thus parallel) loading of scripts