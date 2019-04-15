// === === INTERFACES === ===

import { TypeOfPeople } from './enums';

// 1. Interfces define contract for an object, member definitions but no implementation
// 2. They are only comoile time, they do not exist in the compiled JS
// 3. It follows duck typing: (theory) if a bird walks like a duck, flys like a duck and quacks like a duck, it can be assumed to be a duck
// That means, an object is considered to implement an interface if the definition holds good, even if it's not explicitly implemented
// NA: xx NOTE: an object will not be considered an interface implementation, if it has extra members!!

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

export { Person, getSamplePerson };