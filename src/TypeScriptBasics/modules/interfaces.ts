// === === INTERFACES === ===

import { TypeOfPeople } from './enums';

// 1. Interfces define contract for an object, member definitions but no implementation
// 2. They are only comoile time, they do not exist in the compiled JS
// 3. It follows duck typing: (theory) if a bird walks like a duck, flys like a duck and quacks like a duck, it can be assumed to be a duck
// That means, an object is considered to implement an interface if the definition holds good, even if it's not explicitly implemented
// NOTE: A direct object literal will NOT be considered an interface implementation, if it has extra members!!

interface Person {
    id : number; // properties
    name : string;
    jobTitle? : string; // optional property
    level : TypeOfPeople; // enum property
    walk : () => void; // methods
    respond? : (message : string) => string[];
}

const getSamplePerson : () => Person = () => {
    return {
        id: 100,
        name: 'AC',
        jobTitle: 'Dev',
        level: TypeOfPeople.Stupid,
        walk: () => console.log('walking...'),
        respond: (msg : string) => [msg, msg, msg],
        age: 30
    }
}

// === interface for defining function types ===
// NOTE the syntax, it's different from STANDARD interface member and function type

interface StringGenerator {
    (id : string, num : number) : string // take note!!
}

const getSimpleId = (idx : string, id : number) : string => idx + id;

let myGenerator : StringGenerator;
myGenerator = getSimpleId; // check this out

// === interface extension ===

interface IPerson {
    name: string;
}
interface IFighter {
    fight: () => void;
}

interface INinja extends IPerson, IFighter {
    skills: string[];
}

const myNinja : INinja = {
    name: 'Yoyo', // all 3 properties are required
    fight: () => console.log('aaya...'),
    skills: ['kick', 'punch'],
    // title: 'Yoda' // extra property is not allowed in direct object literal declaration
}

export { Person, getSamplePerson };