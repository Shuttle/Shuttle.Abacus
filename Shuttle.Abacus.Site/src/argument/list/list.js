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

resources.add('argument', { action: 'list', permission: Permissions.Manage.Arguments});

export const Map = DefineMap.extend({
    id: {
        type: 'string'
    },
    name: {
        type: 'string'
    },
    valueType: {
        type: 'string'
    },
    remove() {
        api.delete({id: this.id})
            .then(function () {
                state.alerts.show({
                    message: localisation.value('itemRemovalRequested',
                        {itemName: localisation.value('argument')})
                });
            });
    },
    argumentValues() {
        router.goto({
            resource: 'argument',
            item: 'values',
            id: this.id,
            action: 'list'
        });
    }
});

export const api = {
    search: new Api({
        endpoint: 'arguments/search',
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
            name: this.name,
            minimumFormulaName: this.minimumFormulaName,
            maximumFormulaName: this.maximumFormulaName
        }, {
            post: true
        });
    },

    init() {
        const columns = this.columns;

        if (!columns.length) {
            columns.push({
                columnTitle: '',
                columnClass: 'col-1',
                stache: '<cs-button text:from="\'values\'" click:from="argumentValues" elementClass:from="\'btn-sm\'"/>'
            });

            columns.push({
                columnTitle: 'name',
                columnClass: 'col',
                attributeName: 'name'
            });

            columns.push({
                columnTitle: 'value-type',
                columnClass: 'col',
                attributeName: 'valueType'
            });

            columns.push({
                columnTitle: 'remove',
                columnClass: 'col-1',
                stache: '<cs-button-remove click:from="remove" elementClass:from="\'btn-sm\'"/>'
            });
        }

        state.title = 'arguments';

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
    tag: 'abacus-argument-list',
    ViewModel,
    view
});