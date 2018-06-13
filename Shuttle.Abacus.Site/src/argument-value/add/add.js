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

resources.add('argument', { item: 'values', action: 'add', permission: Permissions.Manage.Arguments});

var api = {
    arguments: new Api({
        endpoint: 'arguments/{id}'
    })
};

export const ViewModel = DefineMap.extend({
    argument: {
        Type: DefineMap
    },

    argumentId: {
        get() {
            return state.routeData.id;
        }
    },

    get map() {
        const self = this;
        const refreshTimestamp = this.refreshTimestamp;
        return api.arguments.map({
            id: this.argumentId
        })
            .then(function(map){
                self.argument = map;
            });
    },

    init: function () {
        const self = this;
        const result = stack.pop('argument-value');

        state.title = 'argument-values';

        if (!result) {
            return;
        }

        this.valueType = result.valueType;
        this.value = result.value;
    },

    value: {
        type: 'string',
        default: '',
        validate: {
            presence: true
        }
    },

    valueType: {
        type: 'string',
        default: '',
        validate: {
            presence: true
        }
    },

    valueTypes: {
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
            value: this.value,
            valueType: this.valueType
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
    tag: 'abacus-argument-values-add',
    ViewModel,
    view
});