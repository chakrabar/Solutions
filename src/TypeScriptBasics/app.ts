import { GetBookTitles } from './modules/functions';
import { Person, getSamplePerson } from './modules/interfaces';
import { Employee } from './modules/classes';
import { GenericFunction, TestGenericInterface } from './modules/generics';
// import lodash
import * as _ from 'lodash'; // location refers to node_modules/@types/lodash/index

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

const emp22 = GenericFunction<Employee>(myEMp);

TestGenericInterface();

// test lodash JS library with TypeDefinition in ./node_modules/@types/lodash/index.d.ts
const emp_snake_name = _.snakeCase(emp22.name);
console.log(emp_snake_name);
