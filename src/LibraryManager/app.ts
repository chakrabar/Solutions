import { GetBookTitles } from './modules/functions'; // only importing does not add to the JS

class HelloWorld {
    constructor(public message: string) {}
}

const hello = new HelloWorld('Hello TypeScript');
console.log(hello.message);
console.log('yo baby');

// Using a module actually includes that module.js in compiled code (along with .js.map)
// By default it keeps all separate JS files and uses a module pattern like ES6 or CommonJS (config)
console.log('Books: ' + JSON.stringify(GetBookTitles(100)));