import { Inject, Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import UserRegister from '../models/user-register';

@Injectable({
  providedIn: 'root',
 })
export class UserService {
  constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) {}

  tryRegisterUser(model : UserRegister) : Observable<any> {
    return this.http.post(this.baseUrl + 'user/register', model);
  }
}
