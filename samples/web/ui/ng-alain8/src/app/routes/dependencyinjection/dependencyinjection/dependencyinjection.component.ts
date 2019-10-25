import { Component, OnInit, Injector, ViewChild } from '@angular/core';
import { STComponentBase } from '@shared/osharp/components/st-component-base';
import { SFUISchema } from '@delon/form';
import { FilterGroup } from '@shared/osharp/osharp.model';
import { STData, STColumn } from '@delon/abc';

@Component({
    selector: 'app-dependencyinjection',
    templateUrl: './dependencyinjection.component.html',
    styles: []
})
export class DependencyInjectionComponent extends STComponentBase implements OnInit {

    constructor(injector: Injector) {
        super(injector);
        this.moduleName = 'dependencyinjection';
    }

    ngOnInit() {
        super.InitBase();
    }

    protected GetSTColumns(): STColumn[] {
        return [
            { title: 'Index', index: 'Index', sort: true, type: 'number', filterable: true },
            { title: 'Instance', index: 'Instance', sort: true, ftype: 'string', filterable: true },
            { title: 'ImplementationFactory', index: 'ImplementationFactory', sort: true, ftype: 'string', filterable: true },
            { title: 'ImplementationType', index: 'ImplementationType', sort: true, ftype: 'string', filterable: true },
            { title: 'Lifetime', index: 'Lifetime', sort: true, ftype: 'string', filterable: true },
            { title: 'ServiceType', index: 'ServiceType', sort: true, ftype: 'string' },
            { title: 'ServiceAssembly', index: 'ServiceAssembly', sort: true, ftype: 'string', filterable: true },
        ];
    }

    protected GetSFUISchema(): SFUISchema {
        let ui: SFUISchema = {
            '*': { spanLabelFixed: 100, grid: { span: 12 } },
            $UserName: { grid: { span: 24 } },
            $LockoutEnd: { grid: { span: 24 } },
        };
        return ui;
    }

    adSearch(group: FilterGroup) {
        this.request.FilterGroup = group;
        this.st.reload();
    }
}
