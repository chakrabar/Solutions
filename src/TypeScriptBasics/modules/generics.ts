import { Employee } from './classes';

let oldie : Employee[];

// GENERICS : TYPE PARAMETER inside <angle brackets>

let goldie : Array<Employee>; // generic
goldie = new Array<Employee>(5); // instantiation with generic

// a generic function
function GenericFunction<T>(thing : T) : T { // type parameter name can be any letter or word
    console.log(thing);
    console.log(typeof thing);
    return thing;
}

// using a generic function
let emp = new Employee('Arghya', 'Mr', 'Chakrabarty');
const myEmp = GenericFunction<Employee>(emp);

// === GENERIC INTERFACE ===

interface IInventory<T> {
    addItem : (item : T) => void;
    getCount : () => number;
    getItems : () => Array<T>;
}

let hrRecords : IInventory<Employee>;
// const employees = hrRecords.getItems(); // won't work as it has no implementtion

class Inventory<T> implements IInventory<T> {
    private _items : Array<T> = new Array<T>();

    addItem : (item: T) => void = (ietm : T) => {
        this._items.push(ietm);
    }
    getCount : () => number = () => {
        return this._items.length;
    }
    getItems : () => T[] = () => {
        return this._items;
    }
}

const TestGenericInterface : () => void = () => {
    // setup
    const myEMp = new Employee('Arghya', 'Mr', 'Chakrabarty');  
    const emp22 = GenericFunction<Employee>(myEMp);

    // usage
    let emplpoyees : IInventory<Employee>;
    emplpoyees = new Inventory<Employee>();
    emplpoyees.addItem(myEMp);
    emplpoyees.addItem(emp22);
    console.log(emplpoyees.getCount());
    console.log(emplpoyees.getItems())
}

// === GENERIC CONSTRAINTS (on type parameter) ===
interface IEntity {
    id : number;
}

// see ma, cool extends usage for two different purposes!
// first extends basically says, the type for parameter must be of type IEntity
interface IRepository<TEntity extends IEntity> extends IInventory<TEntity> {
    find : (id : number) => TEntity;
}

export { GenericFunction, TestGenericInterface };