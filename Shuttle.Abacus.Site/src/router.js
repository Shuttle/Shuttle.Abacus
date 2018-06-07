import DefineMap from 'can-define/map/';
import resources from '~/resources';
import localisation from '~/localisation';
import access from 'shuttle-access';
import stache from 'can-stache';
import route from 'can-route';
import state from '~/state';
import guard from 'shuttle-guard';
import each from 'can-util/js/each/';
import navbar from '~/navbar';

var RouteData = DefineMap.extend({
    resource: {
        type: 'string',
        default: ''
    },
    item: {
        type: 'string',
        default: ''
    },
    action: {
        type: 'string',
        default: ''
    },
    id: {
        type: 'string',
        default: ''
    },
    full: {
        get() {
            return this.resource + (!!this.id ? `/${this.id}` : '') + (!!this.item ? `/${this.item}` : '') + (!!this.action ? `/${this.action}` : '');
        }
    }
});

var routeData = new RouteData();

var Router = DefineMap.extend({
    data: {
        Type: RouteData,
        default: routeData
    },

    previousHash: 'string',

    init: function () {
        const self = this;

        this.data.on('full', function () {
            self.process.call(self);
        });
    },

    start: function () {
        route.register('{resource}');
        route.register('{resource}/{action}');
        route.register('{resource}/{id}/{action}');
        route.register('{resource}/{id}/{item}/{action}');

        route.data = this.data;

        route.start();
    },

    process: function () {
        var resource;

        if ($('#application-content').length === 0) {
            return;
        }

        if (!this.data.resource) {
            return;
        }

        resource = resources.find(this.data.resource, this.data);

        if (!resource) {
            state.alerts.show({
                message: localisation.value('exceptions.resource-not-found', {
                    resource: this.data.resource,
                    item: this.data.item|| '(-value)',
                    action: this.data.action || '(-action)',
                    id: this.data.id || '(-id)',
                    interpolation: {escape: false}
                }), type: 'warning', name: 'route-error'
            });

            return;
        }

        if (resource.permission && !access.hasPermission(resource.permission)) {
            state.alerts.show({
                message: localisation.value('security.access-denied', {
                    resource: this.data.resource,
                    item: this.data.item|| '(-value)',
                    action: this.data.action || '(-action)',
                    id: this.data.id || '(-id)',
                    permission: resource.permission,
                    interpolation: {escape: false}
                }), type: 'danger', name: 'route-error'
            });

            return;
        }

        state.alerts.clear();
        navbar.controls.clear();
        state.title = '';

        var componentName = resource.componentName || 'abacus-' + resource.resource + (!!this.data.item ? `-${this.data.item}` : '') + (!!this.data.action ? `-${this.data.action}` : '');

        $('#application-content').html(stache('<' + componentName + '></' + componentName + '>')());
    },

    goto: function (data) {
        guard.againstUndefined(data, 'data');

        if (typeof(data) !== 'object') {
            throw new Error('Call \'router.goto\' with route data: e.g. router.goto({resource: \'the-resource\', action: \'the-action\'});');
        }

        if (!data.resource) {
            throw new Error('The \'data\' argument does not contain a \'resource\' value.')
        }

        each(Object.getOwnPropertyNames(data), function (propertyName) {
                if (
                    propertyName !== 'resource'
                    &&
                    propertyName !== 'item'
                    &&
                    propertyName !== 'action'
                    &&
                    propertyName !== 'id'
                ) {
                    throw new Error('The route data contains an unknown attribute \'' + propertyName + '\'.');
                }
            }
        );

        route.data.update(data, true);
    }
});

export default new Router();