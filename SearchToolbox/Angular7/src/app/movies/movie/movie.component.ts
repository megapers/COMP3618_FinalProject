import { Component, OnInit } from '@angular/core';
import { MovieService } from 'src/app/shared/movie.service';
import { NgForm } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-movie',
  templateUrl: './movie.component.html',
  styleUrls: ['./movie.component.css']
})
export class MovieComponent implements OnInit {

  constructor(private service: MovieService, private toastr: ToastrService) { }

  ngOnInit() {
    this.resetForm();
  }

  resetForm(form?: NgForm) {
    if (form != null) {
      form.resetForm();
    }
    this.service.formData = {
      Code: '',
      TitleType: '',
      PrimaryTitle: '',
      OriginalTitle: '',
      IsAdult: false,
      StartYear: null,
      EndYear: null,
      RuntimeMinutes: null,
      Genres: ''
    };
  }

  onSubmit(form: NgForm) {
    this.insertRecord(form);
  }

  insertRecord(form: NgForm) {
    this.service.postMovie(form.value).subscribe(res => {
      this.toastr.success('Record added successfully');
      this.resetForm(form);
    });
  }
}
