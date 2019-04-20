// relative imports
// ==> path relative from this file
// ==> no extension required, it'll find TS compatible file(s)

// => should be used for own modules

// import { mod } from './functions'; // functions in same directory
// import { fn } from './child/interfaces'; // interfsces inside child inside this
// import { gah } from '/super'; // super at system root
// import { lol } from '../sibling/classes'; // go up one level, then to siblings and find classes

// non-relative imports

// => should be used for third party modules

// import * as $ from 'jquery'
// import * as lodash from 'lodash'

// === MODULE RESOLUTION ===

// Classic: default for AMD, System or ES6
// Node: default for CommonJS or UMD

// === CLASSIC ===

// relative in classic mode ==>
// file: src/modules/my.ts
// uses: import { something } from './cult'
// ==> look for
// src/modules/cult.ts , OR
// src/modules/cult.d.ts // corresponding type definition

// non-relative in classic mode ==>
// file: src/modules/my.ts
// uses: import { something } from 'cult'
// ==> look for
// src/modules/cult.ts , OR
// src/modules/cult.d.ts // corresponding type definition
// move up the directory hierarchy and search like 
// src/cult.ts , OR
// src/cult.d.ts
// ... and so on until found, or no more directories!

// === NODE ===

// relative in node mode ==>
// file: src/modules/my.ts
// uses: import { something } from './cult'
// ==> look for (in given order)
// src/modules/cult.ts
// src/modules/cult.tsx // TS equivalent of JSX
// src/modules/cult.d.ts // corresponding type definition
// src/modules/cult/package.json (with 'typings' property with location of the module)
// src/modules/index.ts
// src/modules/index.tsx // TS equivalent of JSX
// src/modules/index.d.ts

// non-relative in node mode ==> basically, try to find node_module
// file: src/modules/my.ts
// uses: import { something } from 'cult'
// ==> look for (in given order)
// src/modules/node_modules/cult.ts / tsx / d.ts
// src/modules/node_modules/cult/package.json (with 'typings' property with location of the module)
// src/modules/node_modules/index.ts / tsx / d.ts
// if not found, walk up the hierarchy and try to find the same files
// src/node_modules/cult.ts / tsx / d.ts
// src/node_modules/cult/package.json (with 'typings' property with location of the module)
// src/node_modules/index.ts / tsx / d.ts
// ... and so on
// it also looks for node_modules/@types/index.d.ts etc. in case there are type definitions