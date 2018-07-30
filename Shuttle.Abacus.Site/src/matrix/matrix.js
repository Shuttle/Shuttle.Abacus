import './list/';
import './add/';
import DefineMap from 'can-define/map/';

export const MatrixMap = DefineMap.extend({
    hasColumnArgument: {
        type: 'boolean',
        get () {
            return !!this.columnArgumentId;
        }
    }
});
