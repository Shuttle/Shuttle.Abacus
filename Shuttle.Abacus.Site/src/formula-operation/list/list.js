import Component from 'can-component/';
import DefineMap from 'can-define/map/';
import DefineList from 'can-define/list/';
import view from './list.stache!';
import { Map } from '../item/';
import resources from '~/resources';
import Permissions from '~/permissions';
import state from '~/state';
import Api from 'shuttle-can-api';
import stache from 'can-stache/';

resources.add('formula', {item: 'operation', action: 'list', permission: Permissions.Manage.Formulas});

export const api = {
    formulas: new Api({
        endpoint: 'formulas/{id}'
    }),
    operations: new Api({
        endpoint: 'formulas/{formulaId}/operations/{operationId}',
        Map
    })
};

export const ViewModel = DefineMap.extend({
    remove (operation) {
        api.operations.delete({
            formulaId: state.routeData.id,
            operationId: operation.id
        })
            .then(function () {
                state.removalRequested('formula-operation');
            });
    },

    name: {
        type: 'string',
        default: ''
    },

    formulaId: {
        get () {
            return state.routeData.id;
        }
    },

    operation: {
        Default: Map
    },

    columns: {
        Default: DefineList
    },

    refreshTimestamp: {
        type: 'string'
    },

    formula: {
        Type: DefineMap
    },

    operations: {
        Type: DefineList
    },

    get map () {
        const self = this;
        const refreshTimestamp = this.refreshTimestamp;

        if (!this.formulaId) {
            return undefined;
        }

        return api.formulas.map({
            id: this.formulaId
        })
            .then(function (map) {
                self.formula = map;

                return api.operations.list({
                    formulaId: self.formulaId
                }).then(function (response) {
                    self.operations = response.map(function (item) {
                        item.formulaId = self.formulaId;

                        return item;
                    });
                });
            });
    },

    init () {
        const columns = this.columns;
        const self = this;
        const editView = stache('<cs-button text:raw="edit" click:from="edit" elementClass:from="\'btn-sm\'"/>');
        const removeView = stache('<cs-button-remove click:from="remove" elementClass:from="\'btn-sm\'"/>');

        if (!columns.length) {
            columns.push({
                columnTitle: 'edit',
                columnClass: 'col-1',
                view: function (operation) {
                    operation.edit = function() {
                        self.operation = this;
                    };

                    return editView(operation);
                }
            });

            columns.push({
                columnTitle: 'operation',
                columnClass: 'col',
                attributeName: 'operation'
            });

            columns.push({
                columnTitle: 'value-provider-name',
                columnClass: 'col-2',
                attributeName: 'valueProviderName'
            });

            columns.push({
                columnTitle: 'input-parameter',
                columnClass: 'col',
                attributeName: 'inputParameterDescription'
            });

            columns.push({
                columnTitle: 'remove',
                columnClass: 'col-1',
                view: function (operation) {
                    operation.remove = function() {
                        self.remove(this);
                    };

                    return removeView(operation);
                }
            });
        }

        state.title = 'operations';

        state.navbar.addButton({
            type: 'refresh',
            viewModel: this
        });
    },

    refresh: function () {
        this.refreshTimestamp = Date.now();
    }
});

export default Component.extend({
    tag: 'abacus-formula-operation-list',
    ViewModel,
    view
});