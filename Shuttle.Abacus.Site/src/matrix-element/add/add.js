import Component from 'can-component/';
import DefineMap from 'can-define/map/';
import view from './add.stache!';
import resources from '~/resources';
import Permissions from '~/permissions';
import Api from 'shuttle-can-api';
import validator from 'can-define-validate-validatejs';
import state from '~/state';
import {OptionMap, OptionList} from 'shuttle-canstrap/select/';
import localisation from '~/localisation';

var api = {
    matrices: new Api({
        endpoint: 'matrices/{id}'
    }),
    elements: new Api({
        endpoint: 'matrix/{id}/operations'
    })
};

export const ViewModel = DefineMap.extend({
    matrix: {
        Type: DefineMap
    },

    operations: {
        Type: OptionList,
        default: [
            {value: 'Addition', label: 'Addition'},
            {value: 'Division', label: 'Division'},
            {value: 'Multiplication', label: 'Multiplication'},
            {value: 'Rounding', label: 'Rounding'},
            {value: 'Subtraction', label: 'Subtraction'},
            {value: 'Set', label: 'Set'}
        ]
    },

    operation: {
        type: 'string',
        default: '',
        validate: {
            presence: true
        }
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

	valueProviderName: {
        type: 'string',
        default: '',
        validate: {
            presence: true
        }
    },

    constant: {
        type: 'string',
        default: ''
    },

	selectedArgument: {
        Type: DefineMap
	},

	selectedMatrix: {
        Type: DefineMap
	},

	selectedFormula: {
        Type: DefineMap
	},

    inputParameter: {
        type: 'string',
        validate: {
            presence: true
        }
    },

	inputParameterMessage: {
        type: 'string',
        default: ''
    },

    add: function () {
        if (!!this.errors()) {
            return false;
        }

        api.elements.post({
            operation: this.operation
        },{
            id: this.matrix.id
        })
            .then(function(){
                state.registrationRequested('matrix-operation');
            });

        return false;
    },

	argumentSearchMapper (argument){
    	argument.text = argument.name;

        return argument;
    }
});

validator(ViewModel);

export default Component.extend({
    tag: 'abacus-matrix-element-add',
    ViewModel,
    view
});