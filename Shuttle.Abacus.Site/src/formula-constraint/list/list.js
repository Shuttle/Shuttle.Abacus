import Component from 'can-component/';
import DefineMap from 'can-define/map/';
import DefineList from 'can-define/list/';
import view from './list.stache!';
import { Map } from '../item/';
import resources from '~/resources';
import Permissions from '~/permissions';
import state from '~/state';
import Api from 'shuttle-can-api';
import stache from 'can-stache';

resources.add('formula', { item: 'constraint', action: 'list', permission: Permissions.Manage.Formulas});

export const api = {
    formulas: new Api({
        endpoint: 'formulas/{id}'
    }),
    constraints: new Api({
        endpoint: 'formulas/{formulaId}/constraints/{constraintId}',
        Map
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

    constraint: {
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

    constraints:{
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
        const self = this;
        const editView = stache('<cs-button text:raw="edit" click:from="edit" elementClass:from="\'btn-sm\'"/>');
        const removeView = stache('<cs-button-remove click:from="remove" elementClass:from="\'btn-sm\'"/>');

        if (!columns.length) {
            columns.push({
                columnTitle: 'edit',
                columnClass: 'col-1',
                view: function (constraint) {
                    constraint.edit = function() {
                        self.constraint = this;
                    };

                    return editView(constraint);
                }
            });

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
                view: function (operation) {
                    operation.remove = function() {
                        self.remove(this);
                    };

                    return removeView(operation);
                }
            });
        }

        state.title = 'constraints';

        state.navbar.addButton({
            type: 'refresh',
            viewModel: this
        });
    },

    remove (constraint) {
        api.constraints.delete({
            formulaId: state.routeData.id,
            constraintId: constraint.id
        })
            .then(function () {
                state.removalRequested('formula-constraint');
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