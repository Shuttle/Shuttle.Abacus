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

export const Map = DefineMap.extend({
    select() {
        this.viewModel.select(this);
    },
    queue () {
        this.status = 'pending';
    },
    passed: {
        type: 'boolean',
        get(value){
            return value && !this.exception;
        }
    },
    result: {
        type: 'string',
        default: ''
    },
    results: {
        Type: DefineList
    },
    logLines: {
        Type: DefineList,
        default: []
    },
    exception: {
        type: 'string',
        default: ''
    },
    status: {
        type: 'string',
        default: 'none'
    },
    resultStatus: {
        type: 'string',
        default: '',
        get (value) {
            return localisation.value(this.passed ? 'passed' : 'failed');
        }
    },
    resultStatusModifier: {
        type: 'string',
        get () {
            return this.passed ? 'success' : 'danger';
        }
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
    },
    viewModel: {
        Type: DefineMap
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
        endpoint: 'tests/{id}/run'
    })
};

export const ViewModel = DefineMap.extend({
    _active: {
        type: 'boolean',
        default: true
    },

    selectedTest: {
        Type: DefineMap
    },

    close(){
        this.selectedTest = undefined;
    },

    select(item){
        this.selectedTest = item;
    },

    runner () {
        const self = this;
        let poll = true;

        if (!this._active) {
            return;
        }

        if (!this.tests.filter(function (item) {
            return item.status == 'running';
        }).length) {
            this.tests.forEach(function (item) {
                if (item.status == 'pending') {
                    poll = false;

                    item.status = 'running';

                    api.run.map({id: item.id})
                        .then(function (response) {
                            item.status = 'none';
                            item.passed = response.passed;
                            item.exception = response.exception;
                            item.result = response.result;
                            item.results = response.results;
                            item.logLines = response.logLines;
                            self.runner();
                        });
                }
            });
        }

        if (poll) {
            setTimeout(function () {
                self.runner.call(self);
            }, 1000);
        }
    },

    queueAll () {
        const self = this;

        this.tests.forEach(function (item) {
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

    list () {
        const self = this;

        api.search.list({
            name: this.name
        }, {
            post: true
        })
            .then(function (response) {
                self.tests = response;

                self.tests.forEach(function(map){
                    map.viewModel = self;
                });
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
<span class="badge badge-info">{{i18n('pending')}}</span>
{{/case}}
{{#case('running')}}
<span class="badge badge-info">{{i18n('running')}}</span>
{{/case}}
{{/switch}}
`
            });

            columns.push({
                columnTitle: 'result',
                columnClass: 'col-1',
                stache: `
{{#if(result)}}
<button type="button" class="btn btn-sm btn-{{resultStatusModifier}}" on:click="select()">
  {{resultStatus}} <span class="badge badge-light">{{result}}</span>
</button>
{{else}}
-
{{/if}}
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
        this.list();
    },

    disconnectedCallback () {
        this._active = false;
    }
});

export default Component.extend({
    tag: 'abacus-test-list',
    ViewModel,
    view
});