import $ from 'jquery';
import 'popper.js';
import 'bootstrap';
import 'moment';
import 'tempusdominus';
import 'can-stache-route-helpers';
import 'can-event-dom-enter/add-global/';

import 'bootstrap/dist/css/bootstrap.css';
import 'font-awesome/css/font-awesome.css';
import './styles.css!';

import {options as apiOptions} from 'shuttle-can-api';
import loader from '@loader';

apiOptions.url = loader.serviceBaseURL;

import stache from '~/main.stache!';
import localisation from '~/localisation';
import state from '~/state';
import router from '~/router';

import canstrap from 'shuttle-canstrap';
import access from 'shuttle-access';

access.url = loader.accessBaseURL;

import '~/login/';
import '~/navigation/';
import '~/dashboard/';

import '~/argument/';
import '~/argument-value/';
import '~/formula/';
import '~/formula-constraint/';
import '~/formula-operation/';
import '~/matrix/';
import '~/matrix-constraint/';
import '~/matrix-element/';
import '~/test/';
import '~/test-argument/';

import validate from 'validate.js';

validate.extend(validate.validators.datetime, {
    // The value is guaranteed not to be null or undefined but otherwise it
    // could be anything.
    parse: function(value, options) {
        return +moment.utc(value);
    },
    // Input is a unix timestamp
    format: function(value, options) {
        var format = options.dateOnly ? "YYYY-MM-DD" : "YYYY-MM-DD hh:mm:ss";
        return moment.utc(value).format(format);
    }
});

canstrap.table.tableClass = 'mt-2';
canstrap.form.elementClass = 'mb-2';
canstrap.formGroup.elementClass = 'mb-2';
canstrap.button.remove.confirmation = function (options) {
    state.modal.confirmation.show(options);
}

$.ajaxPrefilter(function (options, originalOptions) {
    options.error = function (xhr) {
        if (xhr.status != 200) {
            if (xhr.responseJSON) {
                state.alerts.show({message: xhr.responseJSON.message, type: 'danger', name: 'ajax-prefilter-error'});
            } else {
                state.alerts.show({
                    message: xhr.status + ' / ' + xhr.statusText,
                    type: 'danger',
                    name: 'ajax-prefilter-error'
                });
            }
        }

        if (originalOptions.error) {
            originalOptions.error(xhr);
        }
    };
});

localisation.start(function(error) {
    if (error) {
        state.alerts.show({message: `Could not start localisation: ${error}`, type: 'danger', name: 'localisation-error'});
    }

    access.start()
        .then(function () {
            router.start();

            if (window.location.hash === '#!' || !window.location.hash) {
                router.goto({resource: 'dashboard'});
            }

            router.process();

            state.started = true;
        })
        .catch(function(){
            state.alerts.show({message: `Could not connect to access endpoint: ${access.url}`, type: 'danger', name: 'access-error'});
        })
        .finally(function(){
            $('#application-container').html(stache(state));
        });
});
