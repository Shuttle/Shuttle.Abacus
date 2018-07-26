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

resources.add('matrix', { item: 'element', action: 'list', permission: Permissions.Manage.Matrices});

export const api = {
    matrices: new Api({
        endpoint: 'matrices/{id}'
    }),
    operations: new Api({
        endpoint: 'matrices/{id}/operations'
    })
};

export const ViewModel = DefineMap.extend({
    name: {
        type: 'string',
        default: ''
    },

    matrixId: {
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

    matrix: {
        Type: DefineMap
    },

    operations:{
        Type: DefineList
    },

    get map() {
        const self = this;
        const refreshTimestamp = this.refreshTimestamp;

        if (!this.matrixId){
            this.map = undefined;
            return;
        }

        return api.matrices.map({
            id: this.matrixId
        })
            .then(function(map){
                self.matrix = map;

                return api.operations.list({
                    id: self.matrixId
                }).then(function(response){
                    self.operations = response.map(function (item) {
                        item.matrixId = self.matrixId;

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
    tag: 'abacus-matrix-element-list',
    ViewModel,
    view
});