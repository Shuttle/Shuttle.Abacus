import DefineMap from 'can-define/map/';
import DefineList from 'can-define/list/';
import each from 'can-util/js/each/';

var Resources = DefineMap.extend({
	_resources: { Default: DefineList },

	add: function (aggregate, options) {
		var o = options || {};

		if (this.has(aggregate, options)) {
			throw new Error(`Resource '${aggregate}' has already been registered.`);
		}

		this._resources.push({
            aggregate: aggregate,
			value: o.value,
			action: o.action,
			componentName: o.componentName,
			url: o.url,
			permission: o.permission
		});
	},

	has: function (aggregate, options) {
		return this.find(aggregate, options) != undefined;
	},

	find: function (aggregate, options) {
		var o = options || {};
		var result = undefined;

		each(this._resources, function (resource) {
			if (result) {
				return;
			}

			if (resource.aggregate === aggregate && resource.value === o.value && resource.action === o.action) {
				result = resource;
			}
		});

		return result;
	}
});

export default new Resources;
