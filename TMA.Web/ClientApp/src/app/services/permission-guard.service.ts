import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, Router } from '@angular/router';
import { AuthService } from './auth.service';

@Injectable({
  providedIn: 'root'
})
export class PermissionGuard implements CanActivate {

  constructor(private authService: AuthService, private router: Router) { }

  canActivate(
    next: ActivatedRouteSnapshot,
    state: RouterStateSnapshot
  ): boolean {
    const requiredPermissions = next.data.permission; // Get permissions array from route data
    const userPermissions = this.authService.getUserPermissions();

    // Check if any of the user's permissions match the required permissions
    const hasPermission = requiredPermissions.some((requiredPermission: any) =>
      userPermissions.some((userPermission: any) =>
     
        userPermission.area === requiredPermission.area
        // && userPermission.role === requiredPermission.role
        // &&userPermission.access === requiredPermission.access
      )
    );

    if (hasPermission) {
      return true; 
    } else {
      this.router.navigate(['/login']); 
      return false;
    }
  }
}
