import { Component } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { NgxSpinnerService } from 'ngx-spinner';
import { environment } from '../../../environments/environment';
import { loginDTO } from '../../shared/models/models';
import { AuthService } from '../../shared/services/auth.service';

@Component({
    selector: 'app-login',
    templateUrl: './login.component.html',
    styleUrls: ['./login.component.scss']
})
/** Login component*/
export class LoginComponent {
    /** Login ctor */
  hide = true;
  model = new loginDTO()

  signInForm = new FormGroup({
    userName: new FormControl(null, [Validators.required]),
    password: new FormControl(null, [Validators.required])
  });

  constructor(
    private authService: AuthService,
    private spinner: NgxSpinnerService,
    private router: Router) { }

  ngOnInit(): void {
    localStorage.removeItem(environment.token);
  }

  //methods

  setModelValues() {
    this.model.userName = this.signInForm.controls.userName.value;
    this.model.password = this.signInForm.controls.password.value;
  }

  onLoginClick() {
    this.spinner.show()
    this.setModelValues();
    this.authService.login(this.model).subscribe(response => {
      this.authService.setToken((response as any).accessToken);
      this.spinner.hide();
      this.router.navigate(['main/user-form']);
    }, error => {
      this.spinner.hide()
    })
  }
}
