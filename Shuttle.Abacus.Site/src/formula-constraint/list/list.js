import Component from 'can-component/';
import DefineMap from 'can-define/map/';
import DefineList from 'can-define/list/';
import view from './list.stache!';
import resources from '~/resources';
import Permissions from '~/permissions';
import state from '~/state';
import Api from 'shuttle-can-api';

resources.add('formula', { item: 'constraint', action: 'list', permission: Permissions.Manage.Formulas});

export const ConstraintMap = DefineMap.extend({
    remove() {
        api.constraints.delete({
            formulaId: state.routeData.id,
            constraintId: this.id
        })
            .then(function () {
                state.removalRequested("formula-constraint")
            });
    }
});

export const api = {
    formulas: new Api({
        endpoint: 'formulas/{id}'
    }),
    constraints: new Api({
        endpoint: 'formulas/{formulaId}/constraints/{constraintId}',
        Map: ConstraintMap
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

    constraints:{
        Type: DefineList
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

                return api.constraints.list({
                    formulaId: self.formulaId
                }).then(function(response){
                    self.constraints = response.map(function (item) {
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
                columnTitle: 'argument',
                columnClass: 'col',
                attributeName: 'argumentName'
            });

            columns.push({
                columnTitle: 'comparison',
                columnClass: 'col-2',
                attributeName: 'comparison'
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

        state.title = 'constraints';

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
    tag: 'abacus-formula-constraint-list',
    ViewModel,
    view
});