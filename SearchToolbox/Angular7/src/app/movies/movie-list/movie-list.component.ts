import { Component, Input, ViewChild } from '@angular/core';
import { MovieService } from 'src/app/shared/movie.service';
import { Movie } from 'src/app/shared/movie.model';

@Component({
  selector: 'app-movie-list',
  templateUrl: './movie-list.component.html',
  styleUrls: ['./movie-list.component.css']
})

export class MovieListComponent {

  constructor(private service: MovieService) {
  }

  private number;
  public movieLast = false;


  @Input()
  set item(val: string) {
    this.service.getNumOfRecords(val).subscribe((num) => {
      this.number = num;
      this._item = (num > 100) ? 100 : num;
      //console.log(this._item);

      this.service.criteria = {
        searchFor: val,
        codeGreaterThan: '',
        blockSize: this._item
      };

      this.service.searchMovies(this.service.criteria).subscribe((mov) => {
        this.service.list = mov as Movie[];
       });

     });
  }

  scrollHandler(event) {
    // if (this.movieLast === true) {
    //   this._item *= 2;
    //   this.service.criteria.codeGreaterThan = this.service.list[event].code;
    //   this.service.searchMovies(this.service.criteria).subscribe((mov) => {
    //     this.service.list = mov as Movie[];
    //     console.log(this.service.list);
    //    });

    // }
    //console.log(this.movieLast);
  }
  setLast(val) {
    this.movieLast = val;
  }
}

