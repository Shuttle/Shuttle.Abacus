import DefineMap from 'can-define/map/';
import DefineList from 'can-define/list/';
import each from 'can-util/js/each/';

var Resources = DefineMap.extend({
	_resources: { Default: DefineList },

	add: function (resource, options) {
		var o = options || {};

		if (this.has(resource, options)) {
			throw new Error(`Resource '${resource}' has already been registered.`);
		}

		this._resources.push({
            resource: resource,
			item: o.item,
			action: o.action,
			componentName: o.componentName,
			url: o.url,
			permission: o.permission
		});
	},

	has: function (resource, options) {
		return this.find(resource, options) != undefined;
	},

	find: function (resource, options) {
		var o = options || {};
		var result = undefined;

		each(this._resources, function (entry) {
			if (result) {
				return;
			}

			if (entry.resource === resource && (entry.item || '') === (o.item || '') && (entry.action || '') === (o.action || '')) {
				result = entry;
			}
		});

		return result;
	}
});

export default new Resources;
