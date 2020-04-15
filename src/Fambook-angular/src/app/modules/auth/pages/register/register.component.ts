import { Component, ChangeDetectorRef } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { UserService } from 'src/app/core/services/user/user.service';
import { ServiceError } from 'src/app/shared/services/service.error';
import { ConnectionError } from 'src/app/shared/services/connection.error';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent {
  registerForm = this.fb.group({
    email: ['', Validators.email],
    password: ['', Validators.required],
    firstName: ['', Validators.required],
    lastName: ['', Validators.required],
    birthdate: ['', Validators.required]
  });
  hide = true;

  constructor(private fb: FormBuilder, private userService: UserService) { }

  onSubmit() {
    console.log(this.registerForm.value);
    this.userService.create(this.registerForm.value)
      .subscribe(
        response => {
          console.log(response);
        }, (error: ServiceError) => {
          if (error instanceof ConnectionError) {
            console.log('Connection error!');
          } else {
            console.log(error.originalError.error);
          }
        }
      );
  }
}
