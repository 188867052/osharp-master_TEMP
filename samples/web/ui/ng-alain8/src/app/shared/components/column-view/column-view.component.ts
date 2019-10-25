import { Component, OnInit, Injector, ViewChild, Input, Output, EventEmitter, OnChanges } from '@angular/core';
import { STComponentBase } from '@shared/osharp/components/st-component-base';
import { STComponent } from '@delon/abc';
import { OsharpSTColumn } from '@shared/osharp/services/alain.types';
import { FilterRule, FilterOperate, FilterGroup } from '@shared/osharp/osharp.model';

@Component({
    selector: 'osharp-column-view',
    template: `
  <div>
    <button nz-button (click)="st.reload()"><i nz-icon nzType="reload" theme="outline"></i>刷新</button>
  </div>
  <st #st [data]="ReadUrl" [columns]="columns" size="small" [req]="req" [res]="res" [(pi)]="request.PageCondition.PageIndex" [(ps)]="request.PageCondition.PageSize" [page]="page"></st>
  `,
    styles: []
})
export class ColumnViewComponent extends STComponentBase implements OnInit {

    @Input() ReadUrl: string;

    @ViewChild('st', { static: false }) st: STComponent;

    constructor(injector: Injector) {
        super(injector);
    }

    ngOnInit() {
        super.InitBase();
    }

    protected GetSTColumns(): OsharpSTColumn[] {
        return [
            { title: '序号', index: 'OrdinalPosition' },
            { title: '字段名', index: 'ColumnName', sort: true, },
            { title: '描述', index: 'Comment' },
            { title: '数据类型', index: 'StoreType' },
            { title: '是否可空', index: 'IsNullable' },
            { title: '是否是主键', index: 'IsPrimaryKey' },
            { title: '是否是外键', index: 'IsForeignKey' },
            { title: '外键表', index: 'ForeignKeyTableName' },
        ];
    }

    reload(filter: FilterGroup) {
        this.st.pi = 1;
        this.request.FilterGroup = filter;
        this.st.req.body = this.request;
        this.st.reload();
    }
}
