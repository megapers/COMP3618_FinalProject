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

  insertMovie(formData: Movie) {
    return this.http.post(this.rootUrl + '/CRUD', formData);
  }

  updateMovie(code: string, formData: Movie){
    return this.http.post(this.rootUrl + '/CRUD/' + code, formData);
  }

  errorHandler(error: any): void {
    console.log(error);
  }

  getMovieByCode(code: string) {
    return this.http.get<Movie>(this.rootUrl + '/CRUD/' + code);
  }

}
