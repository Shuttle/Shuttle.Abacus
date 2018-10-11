import Component from 'can-component/';
import DefineMap from 'can-define/map/';
import view from './item.stache!';
import Api from 'shuttle-can-api';
import validator from 'can-define-validate-validatejs';
import state from '~/state';
import { OptionMap, OptionList } from 'shuttle-canstrap/select/';

export const Map = DefineMap.extend({
    id: {
        type: 'string'
    },

    argumentId:{
        type: 'string'
    },

    argument: {
        Type: DefineMap,
        validate: {
            presence: true
        },
        serialize: false,
        set(value){
            if (!value){
                return;
            }

            this.argumentId = value.id;

            return value;
        }
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

    remove: {
        type: 'any'
    },

    edit: {
        type: 'any'
    }
});

validator(Map);

var api = {
    arguments: new Api({
        endpoint: 'arguments/search'
    }),
    formulas: new Api({
        endpoint: 'formulas/{id}'
    }),
    constraints: new Api({
        endpoint: 'formulas/{id}/constraints'
    })
};

export const ViewModel = DefineMap.extend({
    adding: {
        type: 'boolean',
        get () {
            return !this.map ||
                !this.map.id ||
                this.map.id === '00000000-0000-0000-0000-000000000000';
        }
    },

    formula: {
        Type: DefineMap
    },

    map: {
        Default: Map,
        set(map){
            if (!map || !map.argumentId) {
                return map;
            }

            api.arguments.list({
                id: map.argumentId
            }, {
                post: true
            }).then(function (list) {
                if (!list.length) {
                    return;
                }

                map.argument = list[0];
            });

            return map;
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

    save: function () {
        const self = this;

        if (!!this.map.errors()) {
            return false;
        }

        api.constraints.post(this.map.serialize(), {
            id: this.formula.id
        })
            .then(function () {
                state.registrationRequested('formula-constraint');

                self.cancel();
            });

        return false;
    },

    cancel () {
        this.map = new Map();
    }
});

export default Component.extend({
    tag: 'abacus-formula-constraint',
    ViewModel,
    view
});