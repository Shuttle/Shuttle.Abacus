import Component from 'can-component/';
import DefineMap from 'can-define/map/';
import view from './input.stache!';
import Api from 'shuttle-can-api';
import {Map} from "../list/list";

var api = {
	search: new Api({
		endpoint: ''
	})
}

export const ViewModel = DefineMap.extend({
	map: {
		Type: DefineMap
	},

	searchValue: {
		type: 'string',
		default: ''
	},

	get searchPromise() {
		return api.search.list({search: encodeURIComponent(this.searchValue)});
	},

	search: function (el) {
		this.searchValue = el.value;
		this.uri = el.value;
		this.value = el.value;

		$(el).dropdown();
	},

	select: function (map) {
		this.map = map;
	}
});

export default Component.extend({
	tag: 'abacus-argument-input',
	ViewModel,
	view
});