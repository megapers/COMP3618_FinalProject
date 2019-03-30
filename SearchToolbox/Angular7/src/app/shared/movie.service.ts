import { Injectable } from '@angular/core';
import { Movie } from './movie.model';
import { HttpClient } from '@angular/common/http';
import { stringify } from '@angular/compiler/src/util';


@Injectable({
  providedIn: 'root'
})
export class MovieService {

  formData: Movie;
  list: Movie[];

  readonly rootUrl = 'http://localhost:51128/api/Movies';

  constructor(private http: HttpClient) { }

  postMovie(formData: Movie) {
    return this.http.post(this.rootUrl + '/CRUD', formData);
  }

  // getMovieByCode(code: string) {
  //   return this.http.get(this.rootUrl + '/CRUD/' + code);
  // }

  getMovieByCode(code: string) {
    return this.http.get(this.rootUrl + '/CRUD/' + code);
  }

}
