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

resources.add('formula', { action: 'list', permission: Permissions.Manage.Formulas});

export const Map = DefineMap.extend({
	remove() {
		api.formulas.delete({id: this.id})
			.then(function () {
				state.removalRequested('formula');
			});
	},
    operations() {
        router.goto({
            resource: 'formula',
            item: 'operation',
            action: 'list',
            id: this.id
        });
    },
    constraints() {
        router.goto({
            resource: 'formula',
            item: 'constraint',
            action: 'list',
            id: this.id
        });
    }
});

export const api = {
	formulas: new Api({
		endpoint: 'formulas/{id}',
		Map
	}),
    search: new Api({
        endpoint: 'formulas/search',
        Map
    })
};

export const ViewModel = DefineMap.extend({
    name: {
        type: 'string',
        default: ''
    },

    minimumFormulaName: {
        type: 'string',
        default: ''
    },

    maximumFormulaName: {
        type: 'string',
        default: ''
    },

    columns: {
        Default: DefineList
    },

    refreshTimestamp: {
        type: 'string'
    },

    get list () {
        const refreshTimestamp = this.refreshTimestamp;
        return api.search.list({
            name: this.name
        }, {
            post: true
        });
    },

    init() {
        const columns = this.columns;

        if (!columns.length) {
            columns.push({
                columnTitle: 'operations',
                columnClass: 'col-1',
                stache: '<cs-button text:from="\'operations\'" click:from="operations" elementClass:from="\'btn-sm\'"/>'
            });

            columns.push({
                columnTitle: 'constraints',
                columnClass: 'col-1',
                stache: '<cs-button text:from="\'constraints\'" click:from="constraints" elementClass:from="\'btn-sm\'"/>'
            });

            columns.push({
                columnTitle: 'name',
                columnClass: 'col',
                attributeName: 'name'
            });

            columns.push({
                columnTitle: 'maximum-formula-name',
                columnClass: 'col',
                attributeName: 'maximumFormulaName'
            });

            columns.push({
                columnTitle: 'minimum-formula-name',
                columnClass: 'col',
                attributeName: 'minimumFormulaName'
            });

	        columns.push({
		        columnTitle: 'remove',
		        columnClass: 'col-1',
		        stache: '<cs-button-remove click:from="remove" elementClass:from="\'btn-sm\'"/>'
	        });
        }

        state.title = 'formulas';

        state.navbar.addButton({
            type: 'add',
            viewModel: this,
            permission: Permissions.Manage.Formulas
        });

        state.navbar.addButton({
            type: 'refresh',
            viewModel: this
        });
    },

    add: function() {
        router.goto({
            resource: 'formula',
            action: 'add'
        });
    },

    refresh: function() {
        this.refreshTimestamp = Date.now();
    }
});

export default Component.extend({
    tag: 'abacus-formula-list',
    ViewModel,
    view
});