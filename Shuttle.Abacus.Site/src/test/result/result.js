import Component from 'can-component/';
import DefineMap from 'can-define/map/';
import view from './result.stache!';
import resources from '~/resources';
import Permissions from '~/permissions';
import router from '~/router';
import Api from 'shuttle-can-api';
import validator from 'can-define-validate-validatejs';
import state from '~/state';
import stack from '~/stack';
import localisation from '~/localisation';
import {OptionMap, OptionList} from 'shuttle-canstrap/select/';

export const ViewModel = DefineMap.extend({
    test: {
        Type: DefineMap
    },

});

validator(ViewModel);

export default Component.extend({
    tag: 'abacus-test-result',
    ViewModel,
    view
});