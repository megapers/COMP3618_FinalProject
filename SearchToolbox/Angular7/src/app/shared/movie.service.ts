import { Injectable } from '@angular/core';
import { Movie } from './movie.model';
import { HttpClient } from '@angular/common/http';


@Injectable({
  providedIn: 'root'
})
export class MovieService {

  formData: Movie;
  readonly rootUrl = 'http://localhost:51128/api/Movies';

  constructor(private http: HttpClient) { }

  postMovie(formData: Movie) {
    return this.http.post(this.rootUrl + '/CRUD', formData);
  }
}
