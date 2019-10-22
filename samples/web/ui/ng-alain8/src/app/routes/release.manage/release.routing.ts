import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { ACLGuard } from '@delon/acl';

import { VersionComponent } from './version/version.component';


const routes: Routes = [
  { path: 'version', component: VersionComponent, canActivate: [ACLGuard], data: { title: '版本管理', reuse: true, titleI18n: "menu.nav.release.version", guard: 'Root.Admin.Release.Version.Read' } },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class ReleaseRoutingModule { }
