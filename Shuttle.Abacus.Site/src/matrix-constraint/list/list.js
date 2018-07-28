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

resources.add('matrix', { item: 'constraint', action: 'list', permission: Permissions.Manage.Matrices});

export const api = {
    matrices: new Api({
        endpoint: 'matrices/{id}'
    }),
    constraints: new Api({
        endpoint: 'matrices/{id}/constraints'
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

	get constraints() {
		return !!this.matrix ? api.constraints.list({
			id: this.matrix.id
		}) : Promise.resolve();
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
            });
    },

    init() {
        const columns = this.columns;

        if (!columns.length) {
            columns.push({
                columnTitle: 'axis',
                columnClass: 'col-1',
                attributeName: 'axis'
            });

            columns.push({
                columnTitle: 'index',
                columnClass: 'col-1',
                attributeName: 'index'
            });

            columns.push({
                columnTitle: 'comparison',
                columnClass: 'col-1',
                attributeName: 'comparison'
            });

            columns.push({
                columnTitle: 'value',
                columnClass: 'col',
                attributeName: 'value'
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
    tag: 'abacus-matrix-constraint-list',
    ViewModel,
    view
});