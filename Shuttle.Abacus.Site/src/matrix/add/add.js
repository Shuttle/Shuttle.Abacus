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

    add: function () {
        if (!!this.errors()) {
            return false;
        }

        matrices.post({
            name: this.name
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