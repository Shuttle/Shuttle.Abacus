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
import $ from "jquery";

resources.add('formula', { item: 'operation', action: 'list', permission: Permissions.Manage.Formulas});

export const OperationMap = DefineMap.extend({
    remove() {
        api.operations.delete({
            formulaId: state.routeData.id,
            operationId: this.id
        })
            .then(function () {
                state.removalRequested("formula-operation")
            });
    }
});

export const api = {
    formulas: new Api({
        endpoint: 'formulas/{id}'
    }),
    operations: new Api({
        endpoint: 'formulas/{formulaId}/operations/{operationId}',
        Map: OperationMap
    })
};

export const ViewModel = DefineMap.extend({
    name: {
        type: 'string',
        default: ''
    },

    formulaId: {
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

    formula: {
        Type: DefineMap
    },

    operations:{
        Type: DefineList
    },

    get map() {
        const self = this;
        const refreshTimestamp = this.refreshTimestamp;

        if (!this.formulaId){
            return undefined;
        }

        return api.formulas.map({
            id: this.formulaId
        })
            .then(function(map){
                self.formula = map;

                return api.operations.list({
                    formulaId: self.formulaId
                }).then(function(response){
                    self.operations = response.map(function (item) {
                        item.formulaId = self.formulaId;

                        return item;
                    });
                });
            });
    },

    init() {
        const columns = this.columns;

        if (!columns.length) {
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
                stache: '<cs-button-remove click:from="remove" elementClass:from="\'btn-sm\'"/>'
            });
        }

        state.title = 'operations';

        state.navbar.addButton({
            type: 'refresh',
            viewModel: this
        });
    },

    refresh: function() {
        this.refreshTimestamp = Date.now();
    }
});

export default Component.extend({
    tag: 'abacus-formula-operation-list',
    ViewModel,
    view
});