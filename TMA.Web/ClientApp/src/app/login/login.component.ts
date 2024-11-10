import { ChangeDetectorRef, Component } from '@angular/core';
import { AuthService } from '../services/auth.service';
import { Router } from '@angular/router';
import { TokenService } from '../services/token.service';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {
  loginForm!: FormGroup;
  errorMessage: string | null = null;
  constructor(
    private authService: AuthService,
    private fb: FormBuilder,
    private router: Router,
    private tokenService: TokenService,
    private cdRef: ChangeDetectorRef
  ) {
     this.authService.logout();
  }


  ngOnInit(): void {
    this.initializeForm();
  }
  private initializeForm() {
    this.loginForm = this.fb.group({
      email: ["", [Validators.required, Validators.email]],
      password: ["", Validators.required],
    });
  }

  login(): void {
    const credentials = { email: this.loginForm.value.email, password: this.loginForm.value.password };

    this.authService.login(credentials).subscribe({
      next: (response) => {
        this.authService.updateLoginStatus(true);
        this.tokenService.updateToken(response);
        this.router.navigate(['/task-list']);
        this.cdRef.detectChanges();
      },
      error: (error) => {
        this.errorMessage = error.error;
      },
    });
  }
}
