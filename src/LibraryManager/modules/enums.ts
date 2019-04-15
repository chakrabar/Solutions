// === enum ===

enum Category { Biography, Poetry, Fiction } // 0, 1, 2
enum TypeOfPeople { Stupid = 1, Smart, Nerd, Asswhole } // 1, 2, 3, 4
enum TypeOfFish { SweetWater = 2, SaltWater = 5, Acquarium = 9 } // 1, 2, 3, 4

const fishType : TypeOfFish = TypeOfFish.Acquarium;
console.log('fishType = ' + fishType + " = " + TypeOfFish[fishType]); // string value by index

export { Category, TypeOfFish, TypeOfPeople };