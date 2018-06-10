import Component from 'can-component/';
import DefineMap from 'can-define/map/';
import DefineList from 'can-define/list/';
import view from './list.stache!';
import resources from '~/resources';
import Permissions from '~/permissions';
import router from '~/router';
import localisation from '~/localisation';
import state from '~/state';
import Api from 'shuttle-can-api';

resources.add('argument', { item: 'values', action: 'list', permission: Permissions.Manage.Arguments});

export const Map = DefineMap.extend({
    value: {
        type: 'string'
    }
});

export const api = {
    values: new Api({
        endpoint: 'arguments/{id}/values',
        Map
    })
};

export const ViewModel = DefineMap.extend({
    name: {
        type: 'string',
        default: ''
    },

    columns: {
        Default: DefineList
    },

    refreshTimestamp: {
        type: 'string'
    },

    get list () {
        const refreshTimestamp = this.refreshTimestamp;
        return api.values.list({
            id: this.argumentId
        });
    },

    init() {
        const columns = this.columns;

        if (!columns.length) {
            columns.push({
                columnTitle: 'value',
                columnClass: 'col',
                attributeName: 'value'
            });
        }

        state.title = 'argument-values';

        state.navbar.addButton({
            type: 'add',
            viewModel: this,
            permission: Permissions.Manage.Arguments
        });

        state.navbar.addButton({
            type: 'refresh',
            viewModel: this
        });
    },

    add: function() {
        router.goto({
            resource: 'argument',
            action: 'add'
        });
    },

    refresh: function() {
        this.refreshTimestamp = Date.now();
    }
});

export default Component.extend({
    tag: 'abacus-argument-values-list',
    ViewModel,
    view
});