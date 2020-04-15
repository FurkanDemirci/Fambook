import { Component } from '@angular/core';
import { FormBuilder, Validators, FormControl, FormGroup } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
import { AuthService } from 'src/app/core/services/auth/auth.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {
  loginForm: FormGroup;
  invalidLogin: boolean;
  hide = true;

  constructor(private fb: FormBuilder, private router: Router, private route: ActivatedRoute, private authService: AuthService) {
    this.loginForm = this.fb.group({
      email: ['', Validators.email],
      password: ['', Validators.required],
    });
  }

  onSubmit() {
    let casted = this.loginForm.value as AuthenticateUser
    if (casted.email == '' || casted.password == '') {
      console.log('returned');
      return;
    }

    this.authService.login(this.loginForm.value)
      .subscribe(
        response => {
          if (response) {
            const returnUrl = this.route.snapshot.queryParamMap.get('returnUrl');
            this.router.navigate([returnUrl || '/home']);
          }
        }, error => {
          this.invalidLogin = true;
        }
      );
  }
}

class AuthenticateUser {
  email: string;
  password: string;
}
