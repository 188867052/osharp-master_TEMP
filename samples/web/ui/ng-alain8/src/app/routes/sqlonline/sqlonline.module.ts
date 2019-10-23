import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { SqlOnlineRoutingModule } from './sqlonline.routing';
import { MsSqlComponent } from './mssql/mssql.component';
import { SharedModule } from '@shared';


@NgModule({
    declarations: [MsSqlComponent],
    imports: [
        CommonModule,
        SharedModule,
        SqlOnlineRoutingModule
    ],
    entryComponents: [MsSqlComponent]
})
export class SqlOnlineModule { }
