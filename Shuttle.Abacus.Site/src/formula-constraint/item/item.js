import Component from 'can-component/';
import DefineMap from 'can-define/map/';
import view from './item.stache!';
import resources from '~/resources';
import Permissions from '~/permissions';
import Api from 'shuttle-can-api';
import validator from 'can-define-validate-validatejs';
import state from '~/state';
import {OptionMap, OptionList} from 'shuttle-canstrap/select/';
import localisation from '~/localisation';

var api = {
    formulas: new Api({
        endpoint: 'formulas/{id}'
    }),
    constraints: new Api({
        endpoint: 'formulas/{id}/constraints'
    })
};

export const ViewModel = DefineMap.extend({
    formula: {
        Type: DefineMap
    },

    argument: {
        Type: DefineMap,
        validate: {
            presence: true
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

    save: function () {
        if (!!this.errors()) {
            return false;
        }

        api.constraints.post({
            argumentId: this.argument.id,
            comparison: this.comparison,
            value: this.value
        },{
            id: this.formula.id
        })
            .then(function(){
                state.registrationRequested('formula-constraint');
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
    tag: 'abacus-formula-constraint-add',
    ViewModel,
    view
});