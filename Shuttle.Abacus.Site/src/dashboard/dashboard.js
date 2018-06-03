import Component from 'can-component/';
import DefineMap from 'can-define/map/';
import view from './dashboard.stache!';
import resources from '~/resources';
import state from '~/state';

resources.add('dashboard');

export const ViewModel = DefineMap.extend({
    init() {
        state.title = 'dashboard';
    }
});

export default Component.extend({
    tag: 'abacus-dashboard',
    view: view,
    viewModel: ViewModel
});

