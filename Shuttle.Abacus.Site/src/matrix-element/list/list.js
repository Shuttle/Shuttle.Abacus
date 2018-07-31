import Component from 'can-component/';
import DefineMap from 'can-define/map/';
import DefineList from 'can-define/list/';
import view from './list.stache!';
import resources from '~/resources';
import Permissions from '~/permissions';
import state from '~/state';
import Api from 'shuttle-can-api';
import each from 'can-util/js/each/';
import { MatrixMap } from '~/matrix/';

resources.add('matrix', {item: 'element', action: 'list', permission: Permissions.Manage.Matrices});

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
        api.elements.delete({id: this.id})
            .then(function () {
                state.removalRequested('matrix-element');
            });
    },
    edit () {
        this.viewModel.element = this;
    }
});

const api = {
    matrices: new Api({
        endpoint: 'matrices/{id}',
        Map: MatrixMap
    }),
    constraints: new Api({
        endpoint: 'matrices/{id}/constraints'
    }),
    elements: new Api({
        endpoint: 'matrices/{id}/elements'
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
        get () {
            var result = new DefineList();

            for (var i = 1; i <= this.requiredColumnCount(); i++) {
                result.push({index: i});
            }

            return result;
        }
    },

    rows: {
        get () {
            var result = new DefineList();

            for (var i = 1; i <= this.requiredRowCount(); i++) {
                result.push({index: i});
            }

            return result;
        }
    },

    requiredRowCount () {
        if (!this.constraints ||
            !this.elements) {
            return 0;
        }

        return this.constraints.reduce((current, item) => {
                return item.axis.toLowerCase() === 'row' ? item.index > current ? item.index : current : current;
            },
            this.elements.reduce((current, item) => {
                return item.row > current ? item.row : current;
            }, 0));
    },

    requiredColumnCount () {
        if (!this.constraints) {
            return 1;
        }

        return this.constraints.reduce((current, item) => {
                return item.axis.toLowerCase() === 'column' ? item.index > current ? item.index : current : current;
            }, 1);
    },

    getValue(row, column) {
        return `${column.index} / ${row.index}`;
    },

    refreshTimestamp: {
        type: 'string'
    },

    matrix: {
        Type: MatrixMap
    },

    element: {
        Type: DefineMap
    },

    constraints: {
        Type: DefineList
    },

    elements: {
        Type: DefineList
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
            })
            .finally(function () {
                api.elements.list({
                    id: self.matrix.id
                }).then(function (result) {
                    self.elements = result;
                });

                api.constraints.list({
                    id: self.matrix.id
                }).then(function (result) {
                    self.constraints = result;
                });
            });
    },

    init () {
        state.title = 'matrix-elements';

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
    tag: 'abacus-matrix-element-list',
    ViewModel,
    view
});