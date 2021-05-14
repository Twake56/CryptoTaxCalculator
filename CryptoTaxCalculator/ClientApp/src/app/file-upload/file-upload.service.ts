import { Component, Inject, Injectable } from '@angular/core';
import { RequestOptions, Headers } from '@angular/http';
import { Observable } from 'rxjs';
import { HttpClient, HttpParams, HttpRequest, HttpEvent, HttpEventType, HttpResponse } from '@angular/common/http';
import { DebugContext } from '@angular/core/src/view';
import { debug } from 'util';

@Component({
  selector: 'app-file-upload',
  templateUrl: './file-upload.component.html'
})
@Injectable()
export class FileUploadService {

  constructor(private http: HttpClient) { }
  uploadFile(url: string, file: File): Observable<HttpEvent<any>> {
    let formData = new FormData();
    formData.append('upload', file);
    debugger;

    let params = new HttpParams();

    const options = {
      params: params,
      reportProgress: true,

    };

    const req = new HttpRequest('POST', url, formData, options);

    return this.http.request(req);

  }

}
