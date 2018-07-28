import Component from 'can-component/';
import DefineMap from 'can-define/map/';
import view from './item.stache!';
import resources from '~/resources';
import Permissions from '~/permissions';
import Api from 'shuttle-can-api';
import validator from 'can-define-validate-validatejs';
import state from '~/state';
import {OptionMap, OptionList} from 'shuttle-canstrap/select/';
import localisation from '~/localisation';

var api = {
    matrices: new Api({
        endpoint: 'matrices/{id}'
    }),
    constraints: new Api({
        endpoint: 'matrices/{id}/constraints'
    })
};

export const ViewModel = DefineMap.extend({
    matrix: {
        Type: DefineMap
    },

    axes: {
        Type: OptionList,
        default: [
            {value: 'Column', label: 'Column'},
            {value: 'Row', label: 'Row'}
        ]
    },

	axis: {
		type: 'string',
		default: 'Column',
		validate: {
			presence: true
		}
	},

	index: {
		type: 'number',
		validate: {
			presence: true
		}
	},

	comparisons: {
        Type: OptionList,
        default: [
            {value: '==', label: '=='},
            {value: '!=', label: '!='},
            {value: '>=', label: '>='},
            {value: '>', label: '>'},
            {value: '<=', label: '<='},
            {value: '<', label: '<'},
            {value: 'in', label: 'in'}
        ]
    },

    comparison: {
        type: 'string',
        default: '',
        validate: {
            presence: true
        }
    },

    value: {
        type: 'string',
        validate: {
            presence: true
        }
    },

    add: function () {
        if (!!this.errors()) {
            return false;
        }

        api.constraints.post({
            axis: this.axis,
	        index: this.index,
	        comparison: this.comparison,
	        value: this.value
        },{
            id: this.matrix.id
        })
            .then(function(){
                state.registrationRequested('matrix-operation');
            });

        return false;
    }
});

validator(ViewModel);

export default Component.extend({
    tag: 'abacus-matrix-constraint',
    ViewModel,
    view
});