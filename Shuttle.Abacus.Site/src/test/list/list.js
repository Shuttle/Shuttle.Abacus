import Component from 'can-component/';
import DefineMap from 'can-define/map/';
import DefineList from 'can-define/list/';
import view from './list.stache!';
import resources from '~/resources';
import Permissions from '~/permissions';
import router from '~/router';
import localisation from '~/localisation';
import state from '~/state';
import Api from 'shuttle-can-api';

resources.add('test', {action: 'list', permission: Permissions.Manage.Tests});

export const ResultMap = DefineMap.extend({});

export const Map = DefineMap.extend({
    queue(){
        this.status = "pending";
    },
    result: {
        type: 'string',
        default: ''
    },
    status: {
        type: 'string',
        default: 'none'
    },
    remove () {
        api.tests.delete({id: this.id})
            .then(function () {
                state.removalRequested('test');
            });
    },
    arguments () {
        router.goto({
            resource: 'test',
            item: 'argument',
            action: 'list',
            id: this.id
        });
    }
});

export const api = {
    tests: new Api({
        endpoint: 'tests/{id}',
        Map
    }),
    search: new Api({
        endpoint: 'tests/search',
        Map
    }),
    run: new Api({
        endpoint: 'tests/{id}/run',
        ResultMap
    })
};

export const ViewModel = DefineMap.extend({
    _active: {
        type: 'boolean',
        default: true
    },

    runner(){
        const self = this;
        let poll = true;

        if (!this._active){
            return;
        }

        if (!this.tests.filter(function (item) {
            return item.status == 'running';
        }).length){
            this.tests.forEach(function (item) {
                if (item.status == 'pending'){
                    poll = false;

                    item.status = 'running';

                    api.run.get({id: item.id})
                        .then(function(response){
                            item.status = "done";
                            self.runner();
                        })
                }
            });
        }

        if (poll) {
            setTimeout(function () {
                self.runner.call(self);
            }, 1000);
        }
    },

    queueAll() {
        const self = this;

        this.tests.forEach(function(item){
            item.queue();
        });
    },

    tests: {
        Type: DefineList,
        default: []
    },

    name: {
        type: 'string',
        default: ''
    },

    columns: {
        Default: DefineList
    },

    refreshTimestamp: {
        type: 'string'
    },

    list () {
        const self = this;
        const refreshTimestamp = this.refreshTimestamp;

        api.search.list({
            name: this.name
        }, {
            post: true
        })
            .then(function (response) {
                self.tests = response;
            });
    },

    init () {
        const columns = this.columns;

        if (!columns.length) {
            columns.push({
                data: this,
                columnTitle: 'status',
                columnClass: 'col-1',
                stache: `
{{#switch(status)}}
{{#case('none')}}
<cs-button text:raw="queue" click:from="queue" elementClass:raw="btn-sm"/>
{{/case}}
{{#case('pending')}}
{{i18n('pending')}}
{{/case}}
{{/switch}}
`
            });

            columns.push({
                columnTitle: 'arguments',
                columnClass: 'col-1',
                stache: '<cs-button text:raw="arguments" click:from="arguments" elementClass:raw="btn-sm"/>'
            });

            columns.push({
                columnTitle: 'name',
                columnClass: 'col',
                attributeName: 'name'
            });

            columns.push({
                columnTitle: 'formula',
                columnClass: 'col',
                attributeName: 'formulaName'
            });

            columns.push({
                columnTitle: 'expected-result',
                columnClass: 'col',
                attributeName: 'expectedResult'
            });

            columns.push({
                columnTitle: 'data-type-name',
                columnClass: 'col',
                attributeName: 'expectedResultDataTypeName'
            });

            columns.push({
                columnTitle: 'comparison',
                columnClass: 'col',
                attributeName: 'comparison'
            });

            columns.push({
                columnTitle: 'remove',
                columnClass: 'col-1',
                stache: '<cs-button-remove click:from="remove" elementClass:raw="btn-sm"/>'
            });
        }

        state.title = 'tests';

        state.navbar.addButton({
            type: 'add',
            viewModel: this,
            permission: Permissions.Manage.Tests
        });

        state.navbar.addButton({
            type: 'refresh',
            viewModel: this
        });

        this.list();
        this.runner();
    },

    add: function () {
        router.goto({
            resource: 'test',
            action: 'item'
        });
    },

    refresh: function () {
        this.refreshTimestamp = Date.now();
    },

    disconnectedCallback(){
        this._active = false;
    }
});

export default Component.extend({
    tag: 'abacus-test-list',
    ViewModel,
    view
});