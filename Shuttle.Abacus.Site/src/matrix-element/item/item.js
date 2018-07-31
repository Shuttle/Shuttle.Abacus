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
    elements: new Api({
        endpoint: 'matrices/{id}/elements'
    })
};

export const ViewModel = DefineMap.extend({
    adding: {
        type: 'boolean',
        get () {
            return !this.element ||
                !this.element.id ||
                this.element.id === '00000000-0000-0000-0000-000000000000';
        }
    },

    matrix: {
        Type: MatrixMap
    },

    elementId () {
        return !!this.element ? this.element : undefined;
    },

    element: {
        Type: DefineMap,
        default: {},
        set (value) {
            if (!!value) {
                this.row = value.row;
                this.column = value.column;
                this.value = value.value;
            }

            return value;
        }
    },

    row: {
        type: 'number'
    },

    column: {
        type: 'number'
    },

    value: {
        type: 'string',
        validate: {
            presence: true
        }
    },

    submit: function () {
        if (!!this.errors()) {
            return false;
        }

        api.elements.post({
            id: this.elementId,
            axis: this.axis,
            index: this.index,
            comparison: this.comparison,
            value: this.value
        }, {
            id: this.matrix.id
        })
            .then(function () {
                state.registrationRequested('matrix-element');
            });

        this.cancel();

        return false;
    },

    cancel () {
        this.element = undefined;
    }
});

validator(ViewModel);

export default Component.extend({
    tag: 'abacus-matrix-element',
    ViewModel,
    view
});