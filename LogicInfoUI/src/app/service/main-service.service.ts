import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { UnitMaster } from '../unit-master/unit-master';

@Injectable({
  providedIn: 'root'
})
export class MainServiceService {

  
  serviceUrl = "http://localhost:58770";
  constructor(
    private readonly httpClient: HttpClient,
  ) { }
  getAllUnitMaster(): Observable<any> {
    const url = this.serviceUrl + '/getAll/unitMaster';
    return this.httpClient.get<any>(url);
  }
  addUnitMaster(unitMaster: UnitMaster) {
    const url = this.serviceUrl + '/add/unitMaster';
    return this.httpClient.post(url, unitMaster);
  }
  deleteFromUnitMaster(unitMaster: UnitMaster) {
    const url = `${this.serviceUrl}/remove?Name=${unitMaster.name}&Group=${unitMaster.group}&Desc=${unitMaster.desc}`;
    return this.httpClient.delete(url);
  }
}
