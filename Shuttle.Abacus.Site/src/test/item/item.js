import Component from 'can-component/';
import DefineMap from 'can-define/map/';
import view from './item.stache!';
import resources from '~/resources';
import Permissions from '~/permissions';
import router from '~/router';
import Api from 'shuttle-can-api';
import validator from 'can-define-validate-validatejs';
import state from '~/state';
import stack from '~/stack';
import localisation from '~/localisation';
import {OptionMap, OptionList} from 'shuttle-canstrap/select/';

resources.add('test', {action: 'item', permission: Permissions.Manage.Tests});

var tests = new Api({
    endpoint: 'tests/{id}'
});

export const ViewModel = DefineMap.extend({
    init: function () {
        const result = stack.pop('test');

        state.title = 'test';

        if (!result) {
            return;
        }

        this.name = result.name;
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

    name: {
        type: 'string',
        default: '',
        validate: {
            presence: true
        }
    },

    selectedFormula: {
        Type: DefineMap,
        validate: {
            presence: true
        }
    },

    comparison: {
        type: 'string',
        default: '',
        validate: {
            presence: true
        }
    },

    expectedResult: {
        type: 'string',
        validate: {
            presence: true
        }
    },

    expectedResultDataTypeName: {
        type: 'string',
        default: '',
        validate: {
            presence: true
        }
    },

    save: function () {
        if (!!this.errors()) {
            return false;
        }

        tests.post({
            id: state.routeData.id,
            name: this.name,
            formulaId: this.selectedFormula.id,
            comparison: this.comparison,
            expectedResult: this.expectedResult,
            expectedResultDataTypeName: this.expectedResultDataTypeName
        })
            .then(function(){
                state.registrationRequested('test');
            });

        this.close();

        return false;
    },

    close: function () {
        router.goto({
            resource: 'test',
            action: 'list'
        });
    }
});

validator(ViewModel);

export default Component.extend({
    tag: 'abacus-test-item',
    ViewModel,
    view
});