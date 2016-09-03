"use strict";
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};
var core_1 = require('@angular/core');
var data_service_1 = require('../shared/services/data.service');
var CustomersComponent = (function () {
    function CustomersComponent(dataService) {
        this.dataService = dataService;
        this.customers = [];
        this.editId = 0;
    }
    CustomersComponent.prototype.ngOnInit = function () {
        var _this = this;
        this.dataService.getCustomersSummary()
            .subscribe(function (data) { return _this.customers = data; });
    };
    CustomersComponent.prototype.save = function (customer) {
        var _this = this;
        this.dataService.updateCustomer(customer)
            .subscribe(function (status) {
            if (status) {
                _this.editId = 0;
            }
            else {
                _this.errorMessage = 'Unable to save customer';
            }
        });
    };
    CustomersComponent = __decorate([
        core_1.Component({
            moduleId: module.id,
            selector: 'customers',
            templateUrl: 'customers.component.html'
        }), 
        __metadata('design:paramtypes', [data_service_1.DataService])
    ], CustomersComponent);
    return CustomersComponent;
}());
exports.CustomersComponent = CustomersComponent;
//# sourceMappingURL=customers.component.js.map