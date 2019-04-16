import { GetBookTitles } from './modules/functions';
import { Person, getSamplePerson } from './modules/interfaces';
import { Employee } from './modules/classes';

class HelloWorld {
    constructor(public message: string) {}
}

const hello = new HelloWorld('Hello TypeScript');
console.log(hello.message);
console.log('yo baby');

// Using a module actually includes that module.js in compiled code (along with .js.map)
// By default it keeps all separate JS files and uses a module pattern like ES6 or CommonJS (config)
console.log('Books ::: ' + JSON.stringify(GetBookTitles(100)));

const person : Person = getSamplePerson();
console.log(person);

const myEMp = new Employee('Arghya', 'Mr', 'Chakrabarty');
console.log(myEMp.name);
console.log(Employee.description);
