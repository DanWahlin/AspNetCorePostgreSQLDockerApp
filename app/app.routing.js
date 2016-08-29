"use strict";
var router_1 = require('@angular/router');
var customers_component_1 = require('./customers/customers.component');
var app_routes = [
    { path: '', pathMatch: 'full', redirectTo: '/customers' },
    { path: 'customers', component: customers_component_1.CustomersComponent }
];
exports.app_routing = router_1.RouterModule.forRoot(app_routes);
//# sourceMappingURL=app.routing.js.map