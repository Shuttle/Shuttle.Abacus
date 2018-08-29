import Permissions from '~/permissions';

var map = [
    {
        href: '#!dashboard',
        text: 'dashboard'
    },
    {
        href: '#!formula/list',
        text: 'formulas',
        permission: Permissions.Manage.Formulas
    },
    {
        href: '#!matrix/list',
        text: 'matrices',
        permission: Permissions.Manage.Matrices
    },
    {
        href: '#!argument/list',
        text: 'arguments',
        permission: Permissions.Manage.Arguments
    },
    {
        href: '#!test/list',
        text: 'tests',
        permission: Permissions.Manage.Tests
    }
];

export default map;