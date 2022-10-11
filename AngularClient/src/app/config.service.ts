import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { User } from './user';
import { Register } from './register';
import { Observable } from 'rxjs';
import { LoginRequest } from './loginrequest';
import { LoginResponse } from './login-response';

@Injectable({
  providedIn: 'root'
})
export class ConfigService {

apiUrl : String = "https://localhost:44341"

  constructor(private httpClient: HttpClient) { }

  public getUser(){
    return this.httpClient.get<User[]>(this.apiUrl+'/Exam/GetUser');
  }

  public postRegister(register: Register,headers:any): Observable<Register> {
    //const headers = { 'content-type': 'application/json'}  
    console.log(register);
    return this.httpClient.post<Register>(this.apiUrl+'/User/Create', register, {'headers':headers});
  }
  public postLogin(login: LoginRequest,headers:any): Observable<LoginResponse> {
    return this.httpClient.post<LoginResponse>(this.apiUrl+'/Login/Login',login,{'headers':headers});
  }
  
}
