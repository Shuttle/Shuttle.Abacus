import Component from 'can-component/';
import DefineMap from 'can-define/map/';
import DefineList from 'can-define/list/';
import view from './list.stache!';
import resources from '~/resources';
import Permissions from '~/permissions';
import state from '~/state';
import Api from 'shuttle-can-api';

resources.add('test', { item: 'argument', action: 'list', permission: Permissions.Manage.Tests});

export const ArgumentMap = DefineMap.extend({
    testId: {
        type: 'string'
    },
    remove() {
        api.arguments.delete({
            testId: state.routeData.id,
            argumentId: this.id
        })
            .then(function () {
                state.removalRequested("test-argument")
            });
    }
});

export const api = {
    tests: new Api({
        endpoint: 'tests/{id}'
    }),
    arguments: new Api({
        endpoint: 'tests/{testId}/arguments/{argumentId}',
        Map: ArgumentMap
    })
};

export const ViewModel = DefineMap.extend({
    name: {
        type: 'string',
        default: ''
    },

    testId: {
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

    test: {
        Type: DefineMap
    },

    arguments:{
        Type: DefineList
    },

    get map() {
        const self = this;
        const refreshTimestamp = this.refreshTimestamp;

        if (!this.testId){
            return undefined;
        }

        return api.tests.map({
            id: this.testId
        })
            .then(function(map){
                self.test = map;

                return api.arguments.list({
                    testId: self.testId
                }).then(function(response){
                    self.arguments = response.map(function (item) {
                        item.testId = self.testId;

                        return item;
                    });
                });
            });
    },

    init() {
        const columns = this.columns;

        if (!columns.length) {
            columns.push({
                columnTitle: 'argument',
                columnClass: 'col',
                attributeName: 'argumentName'
            });

            columns.push({
                columnTitle: 'value',
                columnClass: 'col',
                attributeName: 'value'
            });

            columns.push({
                columnTitle: 'remove',
                columnClass: 'col-1',
                stache: '<cs-button-remove click:from="remove" elementClass:from="\'btn-sm\'"/>'
            });
        }

        state.title = 'test-arguments';

        state.navbar.addButton({
            type: 'refresh',
            viewModel: this
        });

        state.navbar.addButton({
            type: 'back',
            viewModel: this
        });
    },

    refresh: function() {
        this.refreshTimestamp = Date.now();
    }
});

export default Component.extend({
    tag: 'abacus-test-argument-list',
    ViewModel,
    view
});