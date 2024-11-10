import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { AuthService } from '../services/auth.service';
import { TokenService } from '../services/token.service';
import { ActivatedRoute, Router } from '@angular/router';
import { Subscription } from 'rxjs';
@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.css']
})
export class NavMenuComponent implements OnInit {
  isExpanded = false;
  isLoggedIn = false;
  isAdmin = false;
  userName: string = '';
  private authSubscription: Subscription = new Subscription();
  constructor(
    private authService: AuthService,
    private route: ActivatedRoute,
    private router: Router,
    private tokenService: TokenService,
    private cdRef: ChangeDetectorRef

  ) {
  }
  ngOnInit(): void {
    this.isAdmin = this.authService.isAdmin();
    this.userName = this.tokenService.getItem('userName') || '';
    this.authSubscription = this.authService.isLoggedIn$.subscribe(
      (loggedIn: boolean) => {
        this.isLoggedIn = loggedIn;
        this.cdRef.detectChanges();
      }
    );
  }

  ngOnDestroy(): void {
    this.authSubscription.unsubscribe();
  }

  collapse() {
    this.isExpanded = false;
  }

  toggle() {
    this.isExpanded = !this.isExpanded;
  }

  logout() {
    this.authService.logout();
  }
}
