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

resources.add('formula', { action: 'list', permission: Permissions.Manage.Method});

export const Map = DefineMap.extend({
    id: {
        type: 'string'
    },
    name: {
        type: 'string'
    },
    select() {
        state.stack.put('account', this);
    }
});

export const api = {
    search: new Api({
        endpoint: 'formulas/search',
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
        return api.search.list({
            name: this.name
        }, {
            post: true
        });
    },

    init() {
        const columns = this.columns;

        if (!columns.length) {
            columns.push({
                columnTitle: 'select',
                columnClass: 'col-1',
                stache: '<cs-button text:from="\'select\'" click:from="select" elementClass:from="\'btn-sm\'"/>'
            });

            columns.push({
                columnTitle: 'name',
                columnClass: 'col',
                attributeName: 'name'
            });
        }

        state.title = 'formulas';

        state.navbar.addButton({
            type: 'add',
            viewModel: this,
            permission: Permissions.Manage.Formula
        });

        state.navbar.addButton({
            type: 'refresh',
            viewModel: this
        });
    },

    add: function() {
        router.goto({
            resource: 'method',
            action: 'add'
        });
    },

    refresh: function() {
        this.refreshTimestamp = Date.now();
    }
});

export default Component.extend({
    tag: 'abacus-formula-list',
    ViewModel,
    view
});