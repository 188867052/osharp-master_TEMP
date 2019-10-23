import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { ACLGuard } from '@delon/acl';

import { MsSqlComponent } from './mssql/mssql.component';


const routes: Routes = [
    { path: 'mssql', component: MsSqlComponent, canActivate: [ACLGuard], data: { title: 'Sql Online管理', reuse: true, titleI18n: "menu.nav.sqlonline.sql", guard: 'Root.Admin.SqlOnline.MsSql.Read' } },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class SqlOnlineRoutingModule { }
