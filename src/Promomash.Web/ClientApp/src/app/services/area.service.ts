import { Inject, Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import Country from '../models/country';
import Province from '../models/province';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
 })
export class AreaService {
  constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) {}

  getCountries() : Observable<Country[]>  {
    return this.http.get<Country[]>(this.baseUrl + 'area/getCountries');
  }

  getProvinces(countryId: string) : Observable<Province[]>  {
    return this.http.get<Province[]>(this.baseUrl + `area/getProvinces/${countryId}`);
  }
}
