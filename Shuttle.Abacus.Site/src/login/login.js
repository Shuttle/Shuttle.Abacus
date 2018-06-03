import Component from 'can-component/';
import DefineMap from 'can-define/map/';
import view from './login.stache!';
import resources from '~/resources';
import access from 'shuttle-access';
import validator from 'can-define-validate-validatejs';
import state from '~/state';

resources.add('login');

export const ViewModel = DefineMap.extend(
    {
        init(){
            state.title = 'sign-in';
        },
        username: {
            type: 'string',
            validate: {
                presence: true
            }
        },
        password: {
            type: 'string',
            validate: {
                presence: true
            }
        },
        working: {
            type: 'boolean',
            default: false
        },
        submitIconName: {
            get() {
                return this.working ? 'glyphicon-hourglass' : '';
            }
        },
        hasErrors: function () {
            return !!this.errors();
        },
        login: function () {
            var self = this;

            if (this.hasErrors()) {
                return false;
            }

            this.working = true;

            access.login({
                username: this.username,
                password: this.password
            })
                .then(function () {
                    window.location.hash = '#!dashboard';
                })
                .then(function () {
                    self.working = false;
                })
                .catch(function () {
                    self.working = false;
                });

            return true;
        }
    }
);

validator(ViewModel);

export default Component.extend({
    tag: 'abacus-login',
    ViewModel,
    view
});