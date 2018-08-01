import Component from 'can-component/';
import DefineMap from 'can-define/map/';
import DefineList from 'can-define/list/';
import view from './list.stache!';
import resources from '~/resources';
import Permissions from '~/permissions';
import router from '~/router';
import state from '~/state';
import Api from 'shuttle-can-api';
import each from 'can-util/js/each/';
import { MatrixMap } from '~/matrix/';
import { MatrixConstraintList } from '~/matrix-constraint/';

resources.add('matrix', {item: 'constraint', action: 'list', permission: Permissions.Manage.Matrices});

const ConstraintMap = DefineMap.extend({
    id: {
        type: 'string'
    },
    axis: {
        type: 'string'
    },
    comparison: {
        type: 'string'
    },
    value: {
        type: 'string'
    },
    viewModel: {
        Type: ViewModel
    },
    remove () {
        api.constraints.delete({id: this.id})
            .then(function () {
                state.removalRequested('matrix-constraint');
            });
    },
    edit () {
        this.viewModel.constraint = this;
    }
});

const api = {
    matrices: new Api({
        endpoint: 'matrices/{id}',
        Map: MatrixMap
    }),
    constraints: new Api({
        endpoint: 'matrices/{id}/constraints',
        Map: ConstraintMap
    })
};

export const ViewModel = DefineMap.extend({
    name: {
        type: 'string',
        default: ''
    },

    matrixId: {
        get () {
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
        Type: MatrixMap
    },

    constraint: {
        Type: MatrixConstraintList
    },

    get constraints () {
        const self = this;

        return !!this.matrix ? api.constraints.list({
            id: this.matrix.id
        }).then(function (result) {
            each(result, function (item) {
                item.viewModel = self;
            });

            return result;
        }) : Promise.resolve();
    },

    get map () {
        const self = this;
        const refreshTimestamp = this.refreshTimestamp;

        if (!this.matrixId) {
            this.map = undefined;
            return;
        }

        return api.matrices.map({
            id: this.matrixId
        })
            .then(function (map) {
                self.matrix = map;
            });
    },

    init () {
        const columns = this.columns;

        if (!columns.length) {
            columns.push({
                columnTitle: 'edit',
                columnClass: 'col-1',
                stache: '<cs-button text:from="\'edit\'" click:from="edit" elementClass:from="\'btn-sm\'"/>'
            });

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

        state.title = 'matrix-constraints';

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
    tag: 'abacus-matrix-constraint-list',
    ViewModel,
    view
});