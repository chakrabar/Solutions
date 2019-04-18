export interface ISomething { // inline export
    id : String;
}

class InternalStuff {
    name : string;
}

class PublicStuff {
    type : string;
}

function double(num : number) : number {
    return 2 * num;
}

export { PublicStuff as Stuff, double } // export statement : cleaner : can also alias

// to import them in a differen module
// import { Stuff, double as getDouble } from './moduls'
// and use simply as: const six = getDouble(3);
// OR
// import * as mods from './modules'
// and use: const eight = mods.double(4);

// === DEFAULR EXPORT ===
// can be used when exporting just one item from a file. Giving a name to the exporting item is totally optional
// the importing module can give it a name while importing

// export default function (num : number) : boolean {
//     return num % 2 == 0;
// }

// import IsEven from './maths' // giving name while importing

// ==> ==> ==> Imma not gonna use it for the sake of readability