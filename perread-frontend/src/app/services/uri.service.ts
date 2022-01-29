import { Injectable } from '@angular/core';
import { Constants } from '../constants';

@Injectable({
  providedIn: 'root'
})
export class UriService {

  constructor() { }

  getStaticFileUri(resourceUri: string) : string {
    return `${Constants.BACKENDURL}/${resourceUri}`
  }
}
