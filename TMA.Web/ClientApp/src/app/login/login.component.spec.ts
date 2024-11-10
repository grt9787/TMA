import { ComponentFixture, TestBed, fakeAsync, tick } from '@angular/core/testing';
import { LoginComponent } from './login.component';
import { AuthService } from '../services/auth.service';
import { TokenService } from '../services/token.service';
import { Router } from '@angular/router';
import { FormBuilder, ReactiveFormsModule, FormsModule } from '@angular/forms';
import { of, throwError } from 'rxjs';
import { RouterTestingModule } from '@angular/router/testing';

describe('LoginComponent', () => {
  let component: LoginComponent;
  let fixture: ComponentFixture<LoginComponent>;
  let mockAuthService: jasmine.SpyObj<AuthService>;
  let mockTokenService: jasmine.SpyObj<TokenService>;
  let mockRouter: jasmine.SpyObj<Router>;

  beforeEach(async () => {
    mockAuthService = jasmine.createSpyObj('AuthService', ['login', 'logout', 'updateLoginStatus']);
    mockTokenService = jasmine.createSpyObj('TokenService', ['updateToken']);
    mockRouter = jasmine.createSpyObj('Router', ['navigate']);

    await TestBed.configureTestingModule({
      imports: [ReactiveFormsModule, FormsModule, RouterTestingModule],
      declarations: [LoginComponent],
      providers: [
        { provide: AuthService, useValue: mockAuthService },
        { provide: TokenService, useValue: mockTokenService },
        { provide: Router, useValue: mockRouter },
        FormBuilder
      ]
    }).compileComponents();

    fixture = TestBed.createComponent(LoginComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create the component', () => {
    expect(component).toBeTruthy();
  });

  it('should initialize the login form on ngOnInit', () => {
    component.ngOnInit();
    expect(component.loginForm).toBeDefined();
    expect(component.loginForm.controls['email']).toBeDefined();
    expect(component.loginForm.controls['password']).toBeDefined();
  });

  it('should call authService.login and navigate on successful login', fakeAsync(() => {
    const mockResponse = { token: 'test_token' };
    mockAuthService.login.and.returnValue(of(mockResponse));

    component.loginForm.setValue({ email: 'test@example.com', password: 'password123' });

    component.login();
    tick();  // Complete async operations
    fixture.detectChanges();  // Directly apply change detection on fixture

    expect(mockAuthService.login).toHaveBeenCalledWith({ email: 'test@example.com', password: 'password123' });
    expect(mockAuthService.updateLoginStatus).toHaveBeenCalledWith(true);
    expect(mockTokenService.updateToken).toHaveBeenCalledWith(mockResponse);
    expect(mockRouter.navigate).toHaveBeenCalledWith(['/task-list']);
  }));

  it('should set errorMessage on failed login', fakeAsync(() => {
    const mockError = { error: 'Invalid credentials' };
    mockAuthService.login.and.returnValue(throwError(() => mockError));

    component.loginForm.setValue({ email: 'test@example.com', password: 'password123' });

    component.login();
    tick();  // Complete async operations
    fixture.detectChanges();

    expect(component.errorMessage).toBe('Invalid credentials');
  }));

  it('should call authService.logout on component creation', () => {
    expect(mockAuthService.logout).toHaveBeenCalled();
  });
});
