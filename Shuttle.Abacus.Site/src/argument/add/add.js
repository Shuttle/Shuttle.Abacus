import Component from 'can-component/';
import DefineMap from 'can-define/map/';
import view from './add.stache!';
import resources from '~/resources';
import Permissions from '~/permissions';
import router from '~/router';
import Api from 'shuttle-can-api';
import validator from 'can-define-validate-validatejs';
import state from '~/state';
import stack from '~/stack';
import {OptionMap, OptionList} from 'shuttle-canstrap/select/';

resources.add('argument', {action: 'add', permission: Permissions.Manage.Arguments});

var api = {
    arguments: new Api({
        endpoint: 'arguments/{id}'
    })
};

export const ViewModel = DefineMap.extend({
    init: function () {
        const result = stack.pop('formula');

        state.title = 'arguments';

        if (!result) {
            return;
        }

        this.name = result.name;
        this.dataTypeName = result.dataTypeName;
    },

    name: {
        type: 'string',
        default: '',
        validate: {
            presence: true
        }
    },

    dataTypeName: {
        type: 'string',
        default: '',
        validate: {
            presence: true
        }
    },

    dataTypeNames: {
        Type: OptionList,
        default: [
            {value: 'Boolean', label: 'Boolean'},
            {value: 'Date', label: 'Date'},
            {value: 'Decimal', label: 'Decimal'},
            {value: 'Integer', label: 'Integer'},
            {value: 'Text', label: 'Text'}
        ]
    },

    add: function () {
        if (!!this.errors()) {
            return false;
        }

        api.arguments.post({
            name: this.name,
            dataTypeName: this.dataTypeName
        })
            .then(function(){
                state.registrationRequested('argument');
            });

        this.close();

        return false;
    },

    close: function () {
        router.goto({
            resource: 'argument',
            action: 'list'
        });
    }
});

validator(ViewModel);

export default Component.extend({
    tag: 'abacus-argument-add',
    ViewModel,
    view
});