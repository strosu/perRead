import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Constants } from '../constants';
import { PledgeCommand } from '../models/pledge/pledge--command.model';
import { Pledge } from '../models/pledge/pledge.model';

@Injectable({
  providedIn: 'root'
})
export class PledgeService {

  constructor(private httpClient: HttpClient) { }

  getPledge(pledgeId: string) : Observable<Pledge> {
    return this.httpClient.get<Pledge>(`${Constants.BACKENDURL}/pledges/${pledgeId}`);
  }

  addPledge(pledgeCommand: PledgeCommand) : Observable<Pledge> {
    return this.httpClient.post<Pledge>(`${Constants.BACKENDURL}/pledges/add`, pledgeCommand);
  }

  editPledge(pledgeCommand: PledgeCommand) : Observable<Pledge> {
    return this.httpClient.post<Pledge>(`${Constants.BACKENDURL}/pledges/edit`, pledgeCommand);
  }

  deletePledge(pledgeId: string) : Observable<Request> {
    return this.httpClient.delete<Request>(`${Constants.BACKENDURL}/pledges/${pledgeId}}`);
  }
}
