import { Observable } from 'rxjs';
import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { PermissionConstants } from "../constants";

@Injectable()
export class TokenService {
 
  updateToken(authenticationResult: any) {
    this.setTokenData(
      authenticationResult.tokenResult
    );
  }

  setTokenData(tokenData: any) {
    this.setAccessToken(tokenData.token);
    this.setExpiresIn(tokenData.expiresIn);
    this.setClaims(tokenData.claims);
  }

  setAccessToken(value: string): void {
    this.setItem("accessToken", value);
  }

  setExpiresIn(value: string): void {
    this.setItem("expiresin", value);
  }

  setClaims(claims: any): void {
    const parsedClaims = claims.map((claim: any) => ({
      type: claim.type,
      value: claim.value,
    }));

    const userId = parsedClaims.find((c: any) => c.type === 'UserId')?.value;
    const userRoleId = parsedClaims.find((c: any) => c.type === 'UserRoleId')?.value;
    const roleId = parsedClaims.find((c: any) => c.type === 'RoleId')?.value;
    const userName = parsedClaims.find((c: any) => c.type === 'UserName')?.value;
    const roleActions = parsedClaims
      .filter((c: any) => c.type === 'Permission')
      .map((c: any) => c.value);

    const parsedPermissions = roleActions.map((permission:any) => {
      const [role, area, access ] = permission.split('.');
      return { role, area, access, action: PermissionConstants.areaActionMap[area] || 'unknown' }; 
    });

    this.setItem('userName', userName);
    this.setItem('userId', userId);
    this.setItem('userRoleId', userRoleId);
    this.setItem('roleId', roleId);
    this.setItem('permissions', parsedPermissions);
  }

  getAccessToken(): string {
    return this.getItem("accessToken");
  }

  public setItem(key: string, value: any) {
    value = JSON.stringify(value);
    localStorage.setItem(key, value);
  }

  public getItem(key: any): string {
    var value = localStorage.getItem(key);

    return value ? JSON.parse(value) : null;
  }

}

