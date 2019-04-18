// http://www.typescriptlang.org/docs/handbook/compiler-options.html
// https://basarat.gitbooks.io/typescript/content/docs/project/tsconfig.html

// command line compiling
// tsc --t ES5 --outDir compiled --m commonjs --sourceMap app.ts
// can also add --watch flag to run tsc in watch mode => it'll watch for any file change and compile automatically
// to exit watch mode => ctrl + C

// generally all these options are stored in => 'tsconfig.json' file
// tsc reconnizes the directory with 'tsconfig.json' to be the root directory
// if tsc is run in that directory, it'll pickup details from this file and run...
// options in tsconfig can be overriden in the command line though!
// > tsc => just run tsc from terminal if you have a tsconfig.json at root

// you can also instruct the compiler to use a tsconfig.json from s different directory by
// > tsc --project ./lib => use tsconfig.json from lib directory