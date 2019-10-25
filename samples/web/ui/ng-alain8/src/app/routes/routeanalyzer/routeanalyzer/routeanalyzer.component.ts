import { Component, OnInit, Injector, ViewChild } from '@angular/core';
import { STComponentBase } from '@shared/osharp/components/st-component-base';
import { SFUISchema } from '@delon/form';
import { FilterGroup } from '@shared/osharp/osharp.model';
import { STData, STColumn } from '@delon/abc';

@Component({
    selector: 'app-routeanalyzer',
    templateUrl: './routeanalyzer.component.html',
    styles: []
})
export class RouteAnalyzerComponent extends STComponentBase implements OnInit {

    constructor(injector: Injector) {
        super(injector);
        this.moduleName = 'routeanalyzer';
    }

    ngOnInit() {
        super.InitBase();
    }

    protected GetSTColumns(): STColumn[] {
        return [
            { title: 'HttpMethods', index: 'HttpMethods', sort: true, ftype: 'string', filterable: true },
            { title: 'Area', index: 'Area', sort: true, ftype: 'string', filterable: true },
            { title: 'Controller', index: 'ControllerName', sort: true, ftype: 'string', filterable: true },
            { title: 'Action', index: 'ActionName', sort: true, ftype: 'string', filterable: true },
            { title: 'Path', index: 'Path', sort: true, ftype: 'string', filterable: true },
            { title: 'Parameters', index: 'ParameterString', sort: true, ftype: 'string', filterable: true },
            { title: 'Namespace', index: 'Namespace', sort: true, ftype: 'string', filterable: true },
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
