import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Constants } from '../constants';
import { CreateRequestCommand } from '../models/request/create-request-command.model';
import { RequestCommand } from '../models/request/request-command.model';
import { RequestPreview } from '../models/request/request-preview.model';
import { ArticleRequest } from '../models/request/request.model';

@Injectable({
  providedIn: 'root'
})

export class RequestsService {

  constructor(private httpClient: HttpClient) { }

  listRequests(authorId: string) : Observable<RequestPreview[]> {
    return this.httpClient.get<RequestPreview[]>(`${Constants.BACKENDURL}/author/${authorId}/requests`);
  }

  getRequest(requestId: string) : Observable<ArticleRequest> {
    return this.httpClient.get<ArticleRequest>(`${Constants.BACKENDURL}/requests/${requestId}`);
  }

  createRequest(requestCommand: CreateRequestCommand) : Observable<ArticleRequest> {
    return this.httpClient.post<ArticleRequest>(`${Constants.BACKENDURL}/requests/add`, requestCommand);
  }

  editRequest(requestCommand: RequestCommand) : Observable<ArticleRequest> {
    return this.httpClient.post<ArticleRequest>(`${Constants.BACKENDURL}/requests/edit`, requestCommand);
  }
}
