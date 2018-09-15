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
    tests: new Api({
        endpoint: 'tests/{id}'
    }),
    arguments: new Api({
        endpoint: 'tests/{id}/arguments'
    })
};

export const ViewModel = DefineMap.extend({
    adding: {
        type: 'boolean',
        get () {
            return !this.argument ||
                !this.argument.id ||
                this.argument.id === '00000000-0000-0000-0000-000000000000';
        }
    },

    test: {
        Type: DefineMap
    },

    argument: {
        Type: DefineMap,
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

        api.arguments.post({
            argumentId: this.argument.id,
            value: this.value
        },{
            id: this.test.id
        })
            .then(function(){
                state.registrationRequested('test-argument');
            });

        return false;
    },


    cancel () {
        this.argument = undefined;
    },

	argumentSearchMapper (argument){
    	argument.text = argument.name;

        return argument;
    }
});

validator(ViewModel);

export default Component.extend({
    tag: 'abacus-test-argument',
    ViewModel,
    view
});