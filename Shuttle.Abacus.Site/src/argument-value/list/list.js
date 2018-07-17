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
import $ from 'jquery';

resources.add('argument', {item: 'value', action: 'list', permission: Permissions.Manage.Arguments});

export const api = {
    arguments: new Api({
        endpoint: 'arguments/{id}'
    }),
    values: new Api({
        endpoint: 'arguments/{id}/values'
    })
};

export const ViewModel = DefineMap.extend({
    name: {
        type: 'string',
        default: ''
    },

    argumentId: {
        get() {
            return state.routeData.id;
        }
    },

    columns: {
        Default: DefineList
    },

    refreshTimestamp: {
        type: 'string'
    },

    argument: {
        Type: DefineMap
    },

    get values() {
        if (!this.argument){
            return;
        }

        return api.values.list({
            id: this.argument.id
        });
    },

    get map() {
        const self = this;
        const refreshTimestamp = this.refreshTimestamp;

        if (!this.argumentId){
            this.map = undefined;
            return;
        }

        return api.arguments.map({
            id: this.argumentId
        })
            .then(function(map){
                self.argument = map;
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

    add: function () {
        router.goto({
            resource: 'argument',
            id: this.argumentId,
            item: 'values',
            action: 'add'
        });
    },

    refresh: function () {
        this.refreshTimestamp = Date.now();
    }
});

export default Component.extend({
    tag: 'abacus-argument-value-list',
    ViewModel,
    view
});