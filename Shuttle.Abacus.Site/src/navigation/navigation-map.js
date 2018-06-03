import Permissions from '~/permissions';

var map = [
    {
        href: '#!dashboard',
        text: 'dashboard'
    },
    {
        href: '#!formula/list',
        text: 'formulas',
        permission: Permissions.Manage.Formula
    }
];

export default map;