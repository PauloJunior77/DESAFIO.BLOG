import { Component } from '@angular/core';
import { NgForm } from '@angular/forms';
import { AuthService } from '../Services/auth.service';
import { RegisterModel } from '../Models/RegisterModel';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent {
  registerModel: RegisterModel = {
    email: '',
    password: '',
    confirmPassword: '',
    isAdmin: false // Novo campo inicializado
  };

  constructor(private authService: AuthService) { }

  onSubmit(registerForm: NgForm) {
    if (registerForm.valid) {
      this.authService.register(this.registerModel).subscribe(
        response => {
          console.log('User registered successfully');
        },
        error => {
          console.error('Error registering user', error);
        }
      );
    }
  }
}
