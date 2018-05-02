import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { HomeComponent } from './home/home.component';
import { NotFoundComponent } from './core/not-found/not-found.component';
import { PrivacyPolicyComponent } from './core/privacy-policy/privacy-policy.component';

const appRoutes: Routes = [
  { path: 'home', component: HomeComponent },
  { path: '', redirectTo: '/home', pathMatch: 'full' },
  { path: 'privacy-policy', component: PrivacyPolicyComponent },
  {
    path: 'account',
    loadChildren: './account/account.module#AccountModule'
  },
  {
    path: 'briefs',
    loadChildren: './briefs/briefs.module#BriefsModule'
  },
  {
    path: 'legal-resources',
    loadChildren: './legal-resources/legal-resources.module#LegalResourcesModule'
  },
  { path: '**', component: NotFoundComponent }
];

@NgModule({
  imports: [
    RouterModule.forRoot(appRoutes)
  ],
  exports: [
    RouterModule
  ]
})
export class AppRoutingModule { }
