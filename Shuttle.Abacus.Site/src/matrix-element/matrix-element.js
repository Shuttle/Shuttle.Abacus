import './item/';
import './list/';
import DefineMap from 'can-define/map/';
import DefineList from 'can-define/list/';

export const MatrixElementMap = DefineMap.extend({
    row:{
        type: 'number'
    },
    column:{
        type: 'number'
    },
    value: {
        type: 'string',
        default: ''
    },
    editing: {
        type: 'boolean'
    },
    is(row, column){
        return this.row === row && this.column === column;
    }
});

export const MatrixElementList = DefineList.extend({
    '#': MatrixElementMap
})