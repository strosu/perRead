import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Constants } from '../constants';
import { Section } from '../models/section/section.model';

@Injectable({
  providedIn: 'root'
})
export class SectionsService {

  constructor(private httpClient: HttpClient) { }

  getSection(sectionId: string) : Observable<Section> {
    return this.httpClient.get<Section>(`${Constants.BACKENDURL}/section/${sectionId}/articles`)
  }
}
