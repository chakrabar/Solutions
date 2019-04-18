"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var enums_1 = require("./enums");
var getSamplePerson = function () {
    return {
        id: 100,
        name: 'AC',
        jobTitle: 'Dev',
        level: enums_1.TypeOfPeople.Stupid,
        walk: function () { return console.log('walking...'); },
        respond: function (msg) { return [msg, msg, msg]; },
        age: 30
    };
};
exports.getSamplePerson = getSamplePerson;
var getSimpleId = function (idx, id) { return idx + id; };
var myGenerator;
myGenerator = getSimpleId;
var myNinja = {
    name: 'Yoyo',
    fight: function () { return console.log('aaya...'); },
    skills: ['kick', 'punch'],
};
//# sourceMappingURL=interfaces.js.map