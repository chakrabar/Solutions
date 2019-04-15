// === === INTERFACES === ===

import { TypeOfPeople } from './enums';

// 1. Interfces define contract for an object, member definitions but no implementation
// 2. They are only comoile time, they do not exist in the compiled JS
// 3. It follows duck typing: (theory) if a bird walks like a duck, flys like a duck and quacks like a duck, it can be assumed to be a duck
// That means, an object is considered to implement an interface if the definition holds good, even if it's not explicitly implemented

interface Person {
    id : number; // properties
    name : string;
    jobTitle? : string; // optional property
    level : TypeOfPeople; // enum property
    walk : () => void; // methods
    respond : (message : string) => string[];
}