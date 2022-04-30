import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Constants } from '../constants';
import { SectionCommand } from '../models/section/section-command.model';
import { SectionPreview } from '../models/section/section-preview.model';
import { Section } from '../models/section/section.model';

@Injectable({
  providedIn: 'root'
})
export class SectionsService {

  constructor(private httpClient: HttpClient) { }

  getSection(sectionId: string) : Observable<Section> {
    return this.httpClient.get<Section>(`${Constants.BACKENDURL}/section/${sectionId}/articles`)
  }

  listSections() : Observable<SectionPreview[]> {
    return this.httpClient.get<SectionPreview[]>(`${Constants.BACKENDURL}/sections`);
  }

  createSection(sectionCommand: SectionCommand) : Observable<Section> {
    return this.httpClient.post<Section>(`${Constants.BACKENDURL}/sections/add`, sectionCommand);
  }

  updateSection(sectionId: string, sectionCommand: SectionCommand) : Observable<Section> {
    return this.httpClient.post<Section>(`${Constants.BACKENDURL}/section/${sectionId}/edit`, sectionCommand);
  }
}
