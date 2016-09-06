import { Injectable } from '@angular/core';
import { Http, Response, Headers, RequestOptions } from '@angular/http';

import 'rxjs/add/operator/map';
import 'rxjs/add/operator/catch';
import { Observable } from 'rxjs/Rx';

import { ICustomer } from '../interfaces';

@Injectable()
export class DataService {
    
    private url: string = 'api/customersservice/customers/';
    
    constructor(private http: Http) { }
    
    getCustomersSummary() : Observable<ICustomer[]> {
        return this.http.get(this.url)
                   .map((resp: Response) => resp.json())
                   .catch(this.handleError);
    }
    
    updateCustomer(customer: ICustomer) {       
      return this.http.put(this.url + customer.id, customer)
                 .map((response: Response) => response.json())
                 .catch(this.handleError);
    }
    
    handleError(error: any) {
        console.error(error);
        return Observable.throw(error.json().error || 'Server error');
    }
    
}
