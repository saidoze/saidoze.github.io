import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

const routes: Routes = [
  { path: '', redirectTo: 'planning', pathMatch: 'full' },
  /*{ path: 'real-time', loadChildren: 'app/pages/real-time/real-time.module#RealTimeModule', canActivate: [] },
  { path: 'planning', loadChildren: 'app/pages/planning/planning.module#PlanningModule', canActivate: [] },
  { path: 'settings', component: SettingsComponent, data: { titleKey: _translate('title.settings') }, canActivate: [] },
  { path: 'callback', component: CallbackComponent },*/
  { path: '**', redirectTo: '', pathMatch: 'full' }
];

@NgModule({
  imports: [RouterModule.forRoot(
    routes,
    // { enableTracing: true } // <-- debugging purposes only
  )],
  providers: [],
  exports: [RouterModule]
})
export class AppRoutingModule { }
