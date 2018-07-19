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
    arguments: new Api({
        endpoint: 'arguments/{id}'
    }),
    argumentValues: new Api({
        endpoint: 'arguments/{id}/values'
    })
};

export const ViewModel = DefineMap.extend({
    argument: {
        Type: DefineMap
    },

    value: {
        type: 'string',
        default: '',
        validate: {
            presence: true
        }
    },

    add: function () {
        if (!!this.errors()) {
            return false;
        }

        api.argumentValues.post({
            value: this.value
        },{
            id: this.argument.id
        })
            .then(function(){
                state.registrationRequested('argument');
            });

        return false;
    }
});

validator(ViewModel);

export default Component.extend({
    tag: 'abacus-argument-value-add',
    ViewModel,
    view
});