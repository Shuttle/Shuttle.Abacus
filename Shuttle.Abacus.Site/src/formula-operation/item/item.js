import Component from 'can-component/';
import DefineMap from 'can-define/map/';
import view from './item.stache!';
import Api from 'shuttle-can-api';
import validator from 'can-define-validate-validatejs';
import state from '~/state';
import { OptionMap, OptionList } from 'shuttle-canstrap/select/';

export const Map = DefineMap.extend({
    id: {
        type: 'string'
    },

    operation: {
        type: 'string',
        default: '',
        validate: {
            presence: true
        }
    },

    valueProviderName: {
        type: 'string',
        default: '',
        validate: {
            presence: true
        },
        set (value) {
            if (value === 'RunningTotal') {
                this.inputParameter = 'RunningTotal';
            }

            return value;
        }
    },

    constant: {
        type: 'string',
        default: '',
        set (value) {
            this.inputParameter = value;

            return value;
        },
        serialize: false
    },

    selectedArgument: {
        Type: DefineMap,
        set (value) {
            if (!value) {
                return;
            }

            this.inputParameter = value.id;

            return value;
        },
        serialize: false
    },

    selectedMatrix: {
        Type: DefineMap,
        set (value) {
            this.inputParameter = value.id;

            return value;
        },
        serialize: false
    },

    selectedFormula: {
        Type: DefineMap,
        set (value) {
            this.inputParameter = value.id;

            return value;
        },
        serialize: false
    },

    inputParameter: {
        type: 'string',
        validate: {
            presence: true
        }
    },

    inputParameterMessage: {
        type: 'string',
        default: '',
        serialize: false
    },

    remove: {
        type: 'any'
    },

    edit: {
        type: 'any'
    }
});

var api = {
    search: new Api({
        endpoint: '{type}/search'
    }),
    formulas: new Api({
        endpoint: 'formulas/{id}'
    }),
    operations: new Api({
        endpoint: 'formulas/{id}/operations'
    })
};

validator(Map);

export const ViewModel = DefineMap.extend({
    adding: {
        type: 'boolean',
        get () {
            return !this.map ||
                !this.map.id ||
                this.map.id === '00000000-0000-0000-0000-000000000000';
        }
    },

    formula: {
        Type: DefineMap
    },

    map: {
        Default: Map,
        set (map) {
            if (!map || !map.inputParameter) {
                return map;
            }

            switch (map.valueProviderName) {
                case 'Argument': {
                    api.search.list({
                        id: map.inputParameter
                    }, {
                        post: true,
                        parameters: {type: 'arguments'}
                    }).then(function (list) {
                        if (!list.length) {
                            return;
                        }

                        map.selectedArgument = list[0];
                    });
                }
                case 'Matrix': {
                    api.search.list({
                        id: map.inputParameter
                    }, {
                        post: true,
                        parameters: {type: 'matrices'}
                    }).then(function (list) {
                        if (!list.length) {
                            return;
                        }

                        map.selectedMatrix = list[0];
                    });
                }
                case 'Formula': {
                    api.search.list({
                        id: map.inputParameter
                    }, {
                        post: true,
                        parameters: {type: 'formulas'}
                    }).then(function (list) {
                        if (!list.length) {
                            return;
                        }

                        map.selectedFormula = list[0];
                    });
                }
                case 'Constant': {
                    map.constant = map.inputParameter;
                }
            }

            return map;
        }
    },

    operations: {
        Type: OptionList,
        default: [
            {value: 'Addition', label: 'Addition'},
            {value: 'Division', label: 'Division'},
            {value: 'Multiplication', label: 'Multiplication'},
            {value: 'Rounding', label: 'Rounding'},
            {value: 'Subtraction', label: 'Subtraction'}
        ]
    },

    valueProviderNames: {
        Type: OptionList,
        default: [
            {value: 'Argument', label: 'Argument'},
            {value: 'Constant', label: 'Constant'},
            {value: 'Matrix', label: 'Matrix'},
            {value: 'Formula', label: 'Formula'},
            {value: 'RunningTotal', label: 'RunningTotal'}
        ]
    },

    register: function () {
        if (!!this.map.errors()) {
            return false;
        }

        api.operations.post(this.map.serialize(), {
            id: this.formula.id
        })
            .then(function () {
                state.registrationRequested('formula-operation');
            });

        return false;
    },

    cancel () {
        this.map = new Map();
    }
});

export default Component.extend({
    tag: 'abacus-formula-operation',
    ViewModel,
    view
});