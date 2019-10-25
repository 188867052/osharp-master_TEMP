import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { ACLGuard } from '@delon/acl';

import { RouteAnalyzerComponent } from './routeanalyzer/routeanalyzer.component';


const routes: Routes = [
    { path: 'routeanalyzer', component: RouteAnalyzerComponent, canActivate: [ACLGuard], data: { title: 'RouteAnalyzer管理', reuse: true, titleI18n: "menu.nav.routeanalyzer", guard: 'Root.Admin.RouteAnalyzer.RouteAnalyzer.Read' } },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class RouteAnalyzerRoutingModule { }
