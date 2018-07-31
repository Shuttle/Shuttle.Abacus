import Component from 'can-component/';
import DefineMap from 'can-define/map/';
import view from './item.stache!';
import Api from 'shuttle-can-api';
import validator from 'can-define-validate-validatejs';
import state from '~/state';
import { OptionMap, OptionList } from 'shuttle-canstrap/select/';
import { MatrixMap } from '~/matrix/';

var api = {
    matrices: new Api({
        endpoint: 'matrices/{id}'
    }),
    constraints: new Api({
        endpoint: 'matrices/{id}/constraints'
    })
};

export const ViewModel = DefineMap.extend({
    adding: {
        type: 'boolean',
        get () {
            return !this.constraint ||
                !this.constraint.id ||
                this.constraint.id === '00000000-0000-0000-0000-000000000000';
        }
    },

    matrix: {
        Type: MatrixMap
    },

    constraintId () {
        return !!this.constraint ? this.constraint : undefined;
    },

    constraint: {
        Type: DefineMap,
        default: {},
        set (value) {
            if (!!value) {
                this.axis = value.axis;
                this.index = value.index;
                this.comparison = value.comparison;
                this.value = value.value;
            }

            return value;
        }
    },

    axes: {
        Type: OptionList,
        get () {
            if (!this.matrix){
                return [];
            }

            var result =
                [
                    {value: 'Row', label: 'Row'}
                ];

            if (this.matrix.hasColumnArgument) {
                result.push({value: 'Column', label: 'Column'});
            }

            return result;
        }

    },

    axis: {
        type: 'string',
        default: 'Column',
        validate: {
            presence: true
        }
    },

    index: {
        type: 'number',
        validate: {
            presence: true,
            numericality: {
                onlyInteger: true,
                greaterThan: 0,
            }
        }
    },

    comparisons: {
        Type: OptionList,
        default: [
            {value: '==', label: '=='},
            {value: '!=', label: '!='},
            {value: '>=', label: '>='},
            {value: '>', label: '>'},
            {value: '<=', label: '<='},
            {value: '<', label: '<'},
            {value: 'in', label: 'in'}
        ]
    },

    comparison: {
        type: 'string',
        default: '',
        validate: {
            presence: true
        }
    },

    value: {
        type: 'string',
        validate: {
            presence: true
        }
    },

    submitText () {
        return this.adding ? 'add' : 'save';
    },

    submit: function () {
        if (!!this.errors()) {
            return false;
        }

        api.constraints.post({
            id: this.constraintId,
            axis: this.axis,
            index: this.index,
            comparison: this.comparison,
            value: this.value
        }, {
            id: this.matrix.id
        })
            .then(function () {
                state.registrationRequested('matrix-constraint');
            });

        this.cancel();

        return false;
    },

    cancel () {
        this.constraint = undefined;
    }
});

validator(ViewModel);

export default Component.extend({
    tag: 'abacus-matrix-constraint',
    ViewModel,
    view
});