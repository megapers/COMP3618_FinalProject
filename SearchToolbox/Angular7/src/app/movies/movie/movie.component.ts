import { Component, OnInit} from '@angular/core';
import { MovieService } from 'src/app/shared/movie.service';
import { NgForm } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { Movie } from 'src/app/shared/movie.model';

@Component({
  selector: 'app-movie',
  templateUrl: './movie.component.html',
  styleUrls: ['./movie.component.css']
})
export class MovieComponent implements OnInit {
  disableId = false;

  constructor(private service: MovieService, private toastr: ToastrService) {}
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
    this.disableId = false;
  }

  onSubmit(form: NgForm) {
    if (this.disableId) {
      this.updateRecord(this.service.formData.Code, form);
    } else {
      this.insertRecord(form);
    }
  }

  insertRecord(form: NgForm) {
    this.service.insertMovie(form.value).subscribe(res => {
      this.toastr.success('Record added successfully');
      this.resetForm(form);
    }, error => this.toastr.error(error.error));
  }

  getByCode(val) {
    this.service.getMovieByCode(val).subscribe((mov: Movie) => {
      this.service.formData = {
        Code: mov.code,
        TitleType: mov.titleType,
        PrimaryTitle: mov.primaryTitle,
        OriginalTitle: mov.originalTitle,
        IsAdult: mov.isAdult,
        StartYear: mov.startYear,
        EndYear: mov.endYear,
        RuntimeMinutes: mov.runtimeMinutes,
        Genres: mov.genres
      };
      this.disableId = true;
    });
  }

  updateRecord(code, form) {
    this.service.updateMovie(code, form.value).subscribe(res => {
      this.toastr.info('Record updated successfully');
      this.resetForm(form);
    }, error => this.toastr.error(error.error));
  }

  deleteRecord(form, code) {
    if (confirm('Do you want to delete this record?')) {
      this.service.deleteMovie(code).subscribe(res => {
        this.toastr.warning('Record deleted successfully');
        this.resetForm(form);
      }, error => this.toastr.error(error.error));
    }
  }

  clearField(form) {
    this.resetForm(form.value);
  }
}
