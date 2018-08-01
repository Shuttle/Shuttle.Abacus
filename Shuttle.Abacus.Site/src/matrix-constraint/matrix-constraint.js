import './item/';
import './list/';
import DefineMap from 'can-define/map/';
import DefineList from 'can-define/list/';

export const MatrixConstraintMap = DefineMap.extend({
    getComparisonDisplay() {
        return `${this.comparison} ${this.value}`;
    }
});

export const MatrixConstraintList = DefineList.extend({
    '#': MatrixConstraintMap
})