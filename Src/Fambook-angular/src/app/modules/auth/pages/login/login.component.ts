import { Component } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {
  addressForm = this.fb.group({
    userName: [null, Validators.required],
    password: [null, Validators.required],
    shipping: ['rememberMe']
  });

  constructor(private fb: FormBuilder, private router: Router) {}

  onSubmit() {
    this.router.navigate(['/home']);
  }
}
