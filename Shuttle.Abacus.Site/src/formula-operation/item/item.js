import Component from 'can-component/';
import DefineMap from 'can-define/map/';
import view from './item.stache!';
import resources from '~/resources';
import Permissions from '~/permissions';
import Api from 'shuttle-can-api';
import validator from 'can-define-validate-validatejs';
import state from '~/state';
import { OptionMap, OptionList } from 'shuttle-canstrap/select/';
import localisation from '~/localisation';

export const Map = DefineMap.extend({
    operation: {
        type: 'string',
        default: '',
        validate: {
            presence: true
        }
    },

    valueProviderName: {
        type: 'string',
        default: '',
        validate: {
            presence: true
        }
    },

    constant: {
        type: 'string',
        default: '',
        set (value) {
            this.inputParameter = value;

            return value;
        },
        serialize: false
    },

    selectedArgument: {
        Type: DefineMap,
        set (value) {
            if (!value){
                return;
            }

            this.inputParameter = value.id;

            return value;
        },
        serialize: false
    },

    selectedMatrix: {
        Type: DefineMap,
        set (value) {
            this.inputParameter = value.id;

            return value;
        },
        serialize: false
    },

    selectedFormula: {
        Type: DefineMap,
        set (value) {
            this.inputParameter = value.id;

            return value;
        },
        serialize: false
    },

    inputParameter: {
        type: 'string',
        validate: {
            presence: true
        }
    },

    inputParameterMessage: {
        type: 'string',
        default: '',
        serialize: false
    },

    remove: {
        type: 'any'
    },

    edit: {
        type: 'any'
    }
});

var api = {
    search: new Api({
        endpoint: '{type}/search'
    }),
    formulas: new Api({
        endpoint: 'formulas/{id}'
    }),
    operations: new Api({
        endpoint: 'formulas/{id}/operations'
    })
};

validator(Map);

export const ViewModel = DefineMap.extend({
    formula: {
        Type: DefineMap
    },

    map: {
        Default: Map,
        set (map) {
            switch (map.valueProviderName) {
                case 'Argument': {
                    api.search.list({
                        id: map.inputParameter
                    }, {
                        post: true,
                        parameters: {type: 'arguments'}
                    }).then(function(list){
                        if (!list.length){
                            return;
                        }

                        map.selectedArgument = list[0];
                    });
                }
            }

            return map;
        }
    },

    operations: {
        Type: OptionList,
        default: [
            {value: 'Addition', label: 'Addition'},
            {value: 'Division', label: 'Division'},
            {value: 'Multiplication', label: 'Multiplication'},
            {value: 'Rounding', label: 'Rounding'},
            {value: 'Subtraction', label: 'Subtraction'}
        ]
    },

    valueProviderNames: {
        Type: OptionList,
        default: [
            {value: 'Argument', label: 'Argument'},
            {value: 'Constant', label: 'Constant'},
            {value: 'Matrix', label: 'Matrix'},
            {value: 'Formula', label: 'Formula'},
            {value: 'RunningTotal', label: 'RunningTotal'}
        ]
    },

    register: function () {
        if (!!this.errors()) {
            return false;
        }

        api.operations.post(this.map.serialize(), {
            id: this.formula.id
        })
            .then(function () {
                state.registrationRequested('formula-operation');
            });

        return false;
    },

    argumentSearchMapper (argument) {
        argument.text = argument.name;

        return argument;
    }
});

export default Component.extend({
    tag: 'abacus-formula-operation-item',
    ViewModel,
    view
});