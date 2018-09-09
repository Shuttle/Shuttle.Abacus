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
import localisation from '~/localisation';

resources.add('formula', {action: 'add', permission: Permissions.Manage.Formulas});

var formulas = new Api({
    endpoint: 'formulas/{id}'
});

export const ViewModel = DefineMap.extend({
    init: function () {
        const result = stack.pop('formula');

        state.title = 'formula';

        if (!result) {
            return;
        }

        this.name = result.name;
        this.minimumFormulaName = result.minimumFormulaName;
        this.maximumFormulaName = result.maximumFormulaName;
    },

    name: {
        type: 'string',
        default: '',
        validate: {
            presence: true
        }
    },

    add: function () {
        if (!!this.errors()) {
            return false;
        }

        formulas.post({
            name: this.name
        })
            .then(function(){
                state.registrationRequested('formula');
            });

        this.close();

        return false;
    },

    close: function () {
        router.goto({
            resource: 'formula',
            action: 'list'
        });
    }
});

validator(ViewModel);

export default Component.extend({
    tag: 'abacus-formula-add',
    ViewModel,
    view
});