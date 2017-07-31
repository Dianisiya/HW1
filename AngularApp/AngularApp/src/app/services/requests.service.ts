import {Injectable } from '@angular/core';
import { Headers, URLSearchParams, RequestOptions, Request, RequestMethod, Http } from '@angular/http';
import { Subscription } from 'rxjs/Subscription';
import { Observable } from 'rxjs/Rx';
import 'rxjs/Rx';
import { Router } from '@angular/router';

Injectable()
export class RequestService {
  private baseUrl = 'http://localhost:31657';

  constructor(private router: Router, private http: Http) { }

  post(path: string,
       body?: Object,
       withCredentials?: boolean,
       searchParams?: URLSearchParams,
       useDataProperty?: boolean,
       urlEncode?: boolean): Observable<any> {
        const url: string = this.baseUrl + path;

        const headers: Headers = new Headers({
            'Accept': 'application/json',
            'Content-Type': urlEncode ? 'application/x-www-form-urlencoded' : 'application/json',
        });

        const options: RequestOptions = new RequestOptions({
            url: url,
            method: RequestMethod.Post,
            headers: headers,
            body: body,
            search: searchParams,
            withCredentials: withCredentials
        });

        const request = new Request(options);

        return this.makeRequest(request);
    }

    get(path: string, params?: Object, withCredentials?: boolean): Observable<any>{
        const url: string = this.baseUrl + path;
        const headers: Headers = new Headers({
            'Accept': 'application/json'
        });

        const searchParams = new URLSearchParams();

        for (const param in params) {
            if (params.hasOwnProperty(param)) {
                searchParams.set(param, params[param]);
            }
        }

        const options: RequestOptions = new RequestOptions({
            url: url,
            method: RequestMethod.Get,
            headers: headers,
            search: searchParams,
            withCredentials: withCredentials
        });

        const request = new Request(options);

        return this.makeRequest(request);
    }

    delete(path: string,
       body?: Object,
       withCredentials?: boolean,
       searchParams?: string,
       useDataProperty?: boolean,
       urlEncode?: boolean): Observable<any> {
        const url: string = this.baseUrl + path;

        const headers: Headers = new Headers({
            'Accept': 'application/json',
            'Content-Type': urlEncode ? 'application/x-www-form-urlencoded' : 'application/json',
        });

        const options: RequestOptions = new RequestOptions({
            url: url,
            method: RequestMethod.Delete,
            headers: headers,
            body: body,
            search: new URLSearchParams(searchParams),
            withCredentials: withCredentials
        });

        const request = new Request(options);

        return this.makeRequest(request);
    }

    makeRequest(request: Request) {
        return this.http.request(request).map(res => res.json());
    }
}

export function requestServiceFactory(http: Http, router: Router) {
    return new RequestService(router, http);
};