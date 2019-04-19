// used mainly to properly and safely use JS libraries in TS
// Type Definition files are TS files that provides type definitions to be used for specific JS library
// they are basically TS wrapper around JS libraries with no logic, mostly types as interfaces (but they 
// can exist for TS libraries as well). Basic purpose is to provide strong typing for libraries

// they have naming convention of .d.ts

// NOTE: in most cases, the original library and type definition files are built independently. So, there 
// is always a possibility of them not being in sync, specially with newer versions of libraries.

// one huge and popular source is https://github.com/DefinitelyTyped/DefinitelyTyped
// Typings Type Definition Manager tools
// npm packages - sometimes JS library authors include d.ts files in the npm package itself
// also, you can write your own.

// AMBIENT MODULES
// to handle Type Definitions in libraries with large number of modules/packages, ambient sometimes modules are 
// used. they kind of re-export modules with types e.g.

// cardcatalog.d.ts => declaration
// declare module "CardCatalog" {
//    export function printCard(callNumber : string) : void
// }

// app.ts => usage
//// <reference path="cardCatalog.d.ts" /> 3 slash reference
// import * as catalog from "CardCatalog"; // that declare module from above

// NOTE
// TypeDefinitions could be added using raw DefinitelyTyped files, through tsd command line tool or through
// typings tool (https://github.com/typings/typings)
// BUT currently, it should only be installed through > npm install @types/<package>

// === NODE MODULES ===

// to use node modules (e.g. popular JS libraries)
// > npm init -f => it'll create a package.json file with basic details like project name, version, description, author
// now install the package you want (for npm version < 5, use --save flag to also add library to dependencies section
// in the package.json file. For npm version > 5, it is done automatically)

// [1] > npm install lodash => install lodash npm module in node_modules directory and add to package.json

// [2] then go get the type definitions (e.g. from DefinitelyTyped) and include in codebase
// add a /// <reference> to the d.ts file
// OR
// use the https://github.com/typings/typings tool to get the types from multiple possible sources (like 
// DefinitelyTyped, Typings registry, GitHub, BitBucket, npm etc.)
// OR
// starting with TypeScript@2.0, simply install the types from npm (it gets it from DefinitelyTyped) as
// > npm install @types/<package>
// it'll creat a new @types directory inside node_modules and create sub-directory for <package> and add
// the d.ts files there. It'll also add to package.json dependencies, e.g.

/*
  "dependencies": {
    "@types/lodash": "^4.14.123",
    "lodash": "^4.17.11"
  }
  directory ::
  node_modules
    @types
      lodash
        ...
    lodash
      ...
*/

// [3] finally import the ambient module declared by the d.ts file => and now you can use the lib nicely from TS..!!!
// e.g. once you have node_modules -> {lodash & @types/lodash}
// in app.ts => import * as _ from 'lodash'; // location refers to node_modules/@types/lodash/index

