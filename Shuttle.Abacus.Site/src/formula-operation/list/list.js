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

export const api = {
    formulas: new Api({
        endpoint: 'formulas/{id}'
    }),
    operations: new Api({
        endpoint: 'formulas/{id}/operations'
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

    get operations(){
        if (!this.formula){
            return;
        }

        return api.operations.list({
            id: this.formula.id
        });
    },

    get map() {
        const self = this;
        const refreshTimestamp = this.refreshTimestamp;

        if (!this.formulaId){
            this.map = undefined;
            return;
        }

        return api.formulas.map({
            id: this.formulaId
        })
            .then(function(map){
                self.formula = map;
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