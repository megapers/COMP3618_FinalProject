import { Component, OnInit } from '@angular/core';
import { MovieService } from 'src/app/shared/movie.service';

@Component({
  selector: 'app-movies',
  templateUrl: './movies.component.html',
  styleUrls: ['./movies.component.css']
})
export class MoviesComponent implements OnInit {

  constructor(private service: MovieService) { }

  ngOnInit() {
  }

}
