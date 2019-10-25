import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { ACLGuard } from '@delon/acl';

import { DependencyInjectionComponent } from './dependencyinjection/dependencyinjection.component';


const routes: Routes = [
    { path: 'dependencyinjection', component: DependencyInjectionComponent, canActivate: [ACLGuard], data: { title: 'DependencyInjection管理', reuse: true, titleI18n: "menu.nav.dependencyinjection", guard: 'Root.Admin.DependencyInjection.DependencyInjection.Read' } },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class DependencyInjectionRoutingModule { }
