"use strict";
var router_1 = require('@angular/router');
var customers_component_1 = require('./customers/customers.component');
var routes = [
    { path: '', pathMatch: 'full', redirectTo: '/customers' },
    { path: 'customers', component: customers_component_1.CustomersComponent }
];
exports.APP_ROUTER_PROVIDERS = [
    router_1.provideRouter(routes)
];
//# sourceMappingURL=app.routes.js.map