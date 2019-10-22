import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { ReleaseRoutingModule } from './release.routing';
import { VersionComponent } from './version/version.component';
import { SharedModule } from '@shared';


@NgModule({
    declarations: [VersionComponent],
    imports: [
        CommonModule,
        SharedModule,
        ReleaseRoutingModule
    ],
    entryComponents: [VersionComponent]
})
export class ReleaseModule { }
