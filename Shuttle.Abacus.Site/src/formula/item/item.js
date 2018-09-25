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

resources.add('formula', {action: 'item', permission: Permissions.Manage.Formulas});

export const Map = DefineMap.extend({
    name: {
        type: 'string',
        default: '',
        validate: {
            presence: true
        }
    }
});

validator(Map);

var api = {
    formulas: new Api({
        endpoint: 'formulas/{id}',
        Map
    })
};

export const ViewModel = DefineMap.extend({
    init: function () {
        const self = this;

        state.title = 'formula';

        if (state.routeData.id) {
            api.formulas.map({id: state.routeData.id})
                .then(function (map) {
                    self.map = map;
                });
        }
    },

    map: {
        Default: Map
    },

    register: function () {
        if (!!this.map.errors()) {
            return false;
        }

        api.formulas.post(this.map.serialize())
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

export default Component.extend({
    tag: 'abacus-formula-item',
    ViewModel,
    view
});