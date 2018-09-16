import Component from 'can-component/';
import DefineMap from 'can-define/map/';
import view from './item.stache!';
import resources from '~/resources';
import Permissions from '~/permissions';
import router from '~/router';
import Api from 'shuttle-can-api';
import validator from 'can-define-validate-validatejs';
import state from '~/state';
import { OptionMap, OptionList } from 'shuttle-canstrap/select/';

resources.add('argument', {action: 'item', permission: Permissions.Manage.Arguments});

export const Map = DefineMap.extend({
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
    }
});

var api = {
    arguments: new Api({
        endpoint: 'arguments/{id}'
    })
};

export const ViewModel = DefineMap.extend({
    init: function () {
        const self = this;

        state.title = 'arguments';

        if (state.routeData.id) {
            api.arguments.map({id: state.routeData.id})
                .then(function (map) {
                    self.map = map;
                });
        }
    },

    map: {
        Default: Map
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
            .then(function () {
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

validator(Map);

export default Component.extend({
    tag: 'abacus-argument-item',
    ViewModel,
    view
});