"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var functions_1 = require("./modules/functions");
var interfaces_1 = require("./modules/interfaces");
var classes_1 = require("./modules/classes");
var generics_1 = require("./modules/generics");
var _ = require("lodash");
var HelloWorld = (function () {
    function HelloWorld(message) {
        this.message = message;
    }
    return HelloWorld;
}());
var hello = new HelloWorld('Hello TypeScript');
console.log(hello.message);
console.log('yo baby');
console.log('Books ::: ' + JSON.stringify(functions_1.GetBookTitles(100)));
var person = interfaces_1.getSamplePerson();
console.log(person);
var myEMp = new classes_1.Employee('Arghya', 'Mr', 'Chakrabarty');
console.log(myEMp.name);
console.log(classes_1.Employee.description);
var emp22 = generics_1.GenericFunction(myEMp);
generics_1.TestGenericInterface();
var emp_snake_name = _.snakeCase(emp22.name);
console.log(emp_snake_name);
//# sourceMappingURL=app.js.map