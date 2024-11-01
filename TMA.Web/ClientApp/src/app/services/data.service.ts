import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { Observable, throwError as observableThrowError, throwError } from 'rxjs';
import { catchError, map } from 'rxjs/operators';
 

@Injectable()
//export abstract class DataService {
//  constructor(
//    public http: HttpClient,
//    @Inject('BASE_URL') public baseUrl: string,
//    public tokenService: TokenService
//  ) { } 
   
   

//  httpHeaderWithSpecialData() {
//    const token = this.tokenService.getAccessToken();
//    let headers = new HttpHeaders();
//    headers = headers.append('content-type', 'application/json');
//    headers = headers.append('Access-Control-Allow-Origin', "*");
//    headers = headers.append('Authorization', token);
//    return headers;
//  }

//  get(name: string): Observable<any> { 
//    const headers = this.httpHeaderWithSpecialData();
//    let getTaskURL = this.baseUrl + "api/" + name;
//    return this.http.get(getTaskURL, { headers })
//      .pipe(
//        map((res: any) => {

//          return res;
//        }),
//        catchError((result: any) => {
//          return this.handleError(result);
//        })
//      );

//  }

//  post(name: string, model: any): Observable<any> {

//    let headers: any;
//    if (name == 'tasks/ValidateUserAndGetToken') {
//      headers = new HttpHeaders().set('skip', 'true');
//      headers = headers.append('content-type', 'application/json');
//      headers = headers.append('Access-Control-Allow-Origin', "*");
//    } else {
//      headers = this.httpHeaderWithSpecialData()
//    }
     
//    let createTaskURL = this.baseUrl + "api/" + name;
//    return this.http.post(createTaskURL, JSON.stringify(model), { headers })
//      .pipe(
//        map((res: any) => {

//          return res;
//        }),
//        catchError((result: any) => {
//          return this.handleError(result);
//        })
//      );
//  }

//  put(name: string, model: any): Observable<any> { 
//    const headers = this.httpHeaderWithSpecialData();

//    let updateTaskURL = this.baseUrl + "api/" + name;
//    return this.http.put(updateTaskURL, JSON.stringify(model), { headers })
//      .pipe(
//        map((res: any) => {

//          return res;
//        }),
//        catchError((result: any) => {
//          return this.handleError(result);
//        })
//      );
//  }

//  delete(name: string): Observable<any> {
//    const headers = new HttpHeaders({
//      'Content-Type': 'application/json'  // Specify JSON content type
//    });

//    let deleteTaskUrl = this.baseUrl + "api/" + name;
//    return this.http.delete(deleteTaskUrl, { headers })
//      .pipe(
//        map((res: any) => {

//          return res;
//        }),
//        catchError((result: any) => {
//          return this.handleError(result);
//        })
//      );
//  }

//  handleError(result: any) {

//    if (result.status === 400 || result.status === 404) {
//      console.log("in 400")
//      console.log(result);

//      return observableThrowError(result);
//    }
//    if (result.status === 500) {
//      console.log("in 500")
//      console.log(result);

//      return observableThrowError(result);
//    }
//    if (result.status === 403) {

//      console.log("in 403")
//      console.log(result);
//    }

//    if (result.status !== 401) {

//    }


//    if (result.headers === undefined) {
//      return observableThrowError(result);
//    }

//    const applicationError = result.headers.get('Application-Error');
//    return observableThrowError(applicationError || 'Server error');
//  }
  
//}
export abstract class DataService {
  constructor(
    public http: HttpClient,
    @Inject('BASE_URL') public baseUrl: string
  ) { }

  private basicHeaders() {
    return new HttpHeaders({
      'Content-Type': 'application/json',
      'Access-Control-Allow-Origin': '*'
    });
  }

  get(name: string): Observable<any> {
    const getURL = `${this.baseUrl}api/${name}`;
    return this.http.get(getURL, { headers: this.basicHeaders() })
      .pipe(
        map(res => res),
        catchError(result => this.handleError(result))
      );
  }

  post(name: string, model: any): Observable<any> {
    const postURL = `${this.baseUrl}api/${name}`;
    return this.http.post(postURL, model, { headers: this.basicHeaders() })
      .pipe(
        map(res => res),
        catchError(result => this.handleError(result))
      );
  }

  put(name: string, model: any): Observable<any> {
    const putURL = `${this.baseUrl}api/${name}`;
    return this.http.put(putURL, model, { headers: this.basicHeaders() })
      .pipe(
        map(res => res),
        catchError(result => this.handleError(result))
      );
  }

  delete(name: string): Observable<any> {
    const deleteURL = `${this.baseUrl}api/${name}`;
    return this.http.delete(deleteURL, { headers: this.basicHeaders() })
      .pipe(
        map(res => res),
        catchError(result => this.handleError(result))
      );
  }

  handleError(result: any) {
    if (result.status === 400 || result.status === 404) {
      console.error("Client error", result);
      return throwError(result);
    }
    if (result.status === 500) {
      console.error("Server error", result);
      return throwError(result);
    }
    return throwError(result.message || 'Server error');
  }
}
