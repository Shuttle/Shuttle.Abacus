import Component from 'can-component/';
import DefineMap from 'can-define/map/';
import view from './add.stache!';
import resources from '~/resources';
import Permissions from '~/permissions';
import router from '~/router';
import Api from 'shuttle-can-api';
import validator from 'can-define-validate-validatejs';
import state from '~/state';
import stack from '~/stack';
import {OptionMap, OptionList} from 'shuttle-canstrap/select/';
import localisation from '~/localisation';

resources.add('matrix', {action: 'add', permission: Permissions.Manage.Matrices});

var matrices = new Api({
    endpoint: 'matrices/{id}'
});

export const ViewModel = DefineMap.extend({
    init: function () {
        const result = stack.pop('matrix');

        state.title = 'matrix';

        if (!result) {
            return;
        }

        this.name = result.name;
    },

    name: {
        type: 'string',
        default: '',
        validate: {
            presence: true
        }
    },

	dataTypeName: {
		type: 'string',
		default: '',
		validate: {
			presence: true
		}
	},

	dataTypeNames: {
		Type: OptionList,
		default: [
			{value: 'Boolean', label: 'Boolean'},
			{value: 'Date', label: 'Date'},
			{value: 'Decimal', label: 'Decimal'},
			{value: 'Integer', label: 'Integer'},
			{value: 'Text', label: 'Text'}
		]
	},

    rowArgument: {
        Type: DefineMap,
		validate: {
			presence: true
		}
    },

    columnArgument: {
        Type: DefineMap
    },

    add: function () {
        if (!!this.errors()) {
            return false;
        }

        matrices.post({
            name: this.name,
	        rowArgumentId: this.rowArgument.id,
	        columnArgumentId: !!this.columnArgument ? this.columnArgument.id : undefined,
	        dataTypeName: this.dataTypeName
        });

        this.close();

        return false;
    },

    close: function () {
        router.goto({
            resource: 'matrix',
            action: 'list'
        });
    }
});

validator(ViewModel);

export default Component.extend({
    tag: 'abacus-matrix-add',
    ViewModel,
    view
});