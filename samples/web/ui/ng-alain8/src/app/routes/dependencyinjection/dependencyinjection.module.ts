import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { DependencyInjectionRoutingModule } from './dependencyinjection.routing';
import { DependencyInjectionComponent } from './dependencyinjection/dependencyinjection.component';
import { SharedModule } from '@shared';


@NgModule({
    declarations: [DependencyInjectionComponent],
    imports: [
        CommonModule,
        SharedModule,
        DependencyInjectionRoutingModule
    ],
    entryComponents: [DependencyInjectionComponent]
})
export class DependencyInjectionModule { }
