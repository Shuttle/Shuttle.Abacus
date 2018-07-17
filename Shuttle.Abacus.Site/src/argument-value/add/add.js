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
    }),
    argumentValues: new Api({
        endpoint: 'arguments/{id}/values'
    })
};

export const ViewModel = DefineMap.extend({
    argumentId: {
        get() {
            return state.routeData.id;
        }
    },

    init: function () {
        const self = this;
        const result = stack.pop('argument-value');

        state.title = 'argument-values';

        if (!result) {
            return;
        }

        this.value = result.value;
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
            id: this.argumentId
        });

        this.close();

        return false;
    },

    close: function () {
        router.goto({
            resource: 'argument',
            item: 'value',
            id: this.argumentId,
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