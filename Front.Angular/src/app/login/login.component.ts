import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from '../Services/auth.service';
import { LoginModel } from '../Models/LoginModel';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  loginModel: LoginModel = {
    email: '',
    password: '',
    rememberMe: false
  };
  error: string = '';

  constructor(
    private authService: AuthService,
    private router: Router
  ) { }

  ngOnInit(): void {

    if (this.authService.isAuthenticated()) {
      this.router.navigate(['/home']);
    }
  }

  login(): void {
    this.authService.login(this.loginModel).subscribe(
      response => {
        this.router.navigate(['/home']);
      },
      error => {
        this.error = 'Credenciais inválidas';
      }
    );
  }
}
