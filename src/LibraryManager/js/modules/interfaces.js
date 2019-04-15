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
//# sourceMappingURL=interfaces.js.map