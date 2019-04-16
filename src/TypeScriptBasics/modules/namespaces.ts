// namespace is like an internal module
// namespace help organize code entities and saves global space from polluting
namespace Utilities {
    export function UpperCase(txt : string) : string { // 'export is required'
        return txt.toUpperCase();
    }

    // can have nested namespaces
    export namespace Numbers {
        export function double(num : number) : number {
            return 2 * num;
        }
    }
}

var uppa = Utilities.UpperCase('oh My GaWd');
var four = Utilities.Numbers.double(2);