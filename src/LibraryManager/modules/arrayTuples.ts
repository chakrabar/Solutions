// === array ===

const arr1 : string[] = ['an', 'array'];
const arr2 : Array<string> = ['another', 'array'];

console.log([...arr1, ...arr2]);

// === tuples ===
// with first few types defined and members have to be the same type in same order
// rest can be either of the declared types! and can be added with push(). 
// members are accessed with index. Array methods can also be used.

let myTuple: [number, string] = [1, "one"];

myTuple.push(3);

console.log(myTuple);

const one = myTuple[1];

console.log(one);