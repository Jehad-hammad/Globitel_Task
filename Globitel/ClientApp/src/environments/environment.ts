// This file can be replaced during build by using the `fileReplacements` array.
// `ng build ---prod` replaces `environment.ts` with `environment.prod.ts`.
// The list of file replacements can be found in `angular.json`.

export const environment = {
  production: false,
  token: 'acessToken',
  roleClaim: 'http://schemas.microsoft.com/ws/2008/06/identity/claims/role',
  addressClaim: 'http://schemas.microsoft.com/ws/2008/06/identity/claims/streetaddress',
  mobileClaim: 'http://schemas.xmlsoap.org/ws/2005/05/identity/claims/mobilephone',
  userName: 'http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name',
  idClaim: 'http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier',
};

/*
 * In development mode, to ignore zone related error stack frames such as
 * `zone.run`, `zoneDelegate.invokeTask` for easier debugging, you can
 * import the following file, but please comment it out in production mode
 * because it will have performance impact when throw error
 */
// import 'zone.js/dist/zone-error';  // Included with Angular CLI.
