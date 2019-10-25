import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { RouteAnalyzerRoutingModule } from './routeanalyzer.routing';
import { RouteAnalyzerComponent } from './routeanalyzer/routeanalyzer.component';
import { SharedModule } from '@shared';


@NgModule({
    declarations: [RouteAnalyzerComponent],
    imports: [
        CommonModule,
        SharedModule,
        RouteAnalyzerRoutingModule
    ],
    entryComponents: [RouteAnalyzerComponent]
})
export class RouteAnalyzerModule { }
