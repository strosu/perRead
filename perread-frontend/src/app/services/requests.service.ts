import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Constants } from '../constants';
import { CreateRequestCommand } from '../models/request/create-request-command.model';
import { RequestPreview } from '../models/request/request-preview.model';
import { Request } from '../models/request/request.model';

@Injectable({
  providedIn: 'root'
})

export class RequestsService {

  constructor(private httpClient: HttpClient) { }

  listRequests(authorId: string) : Observable<RequestPreview[]> {
    return this.httpClient.get<RequestPreview[]>(`${Constants.BACKENDURL}/author/${authorId}/requests`);
  }

  getRequest(requestId: string) : Observable<Request> {
    return this.httpClient.get<Request>(`${Constants.BACKENDURL}/requests/${requestId}`);
  }

  createRequest(requestCommand: CreateRequestCommand) : Observable<Request> {
    return this.httpClient.post<Request>(`${Constants.BACKENDURL}/requests`, requestCommand);
  }
}
