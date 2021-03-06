import { Component, OnInit, Injector, ViewChild } from '@angular/core';
import { STComponentBase } from '@shared/osharp/components/st-component-base';
import { SFUISchema } from '@delon/form';
import { FilterGroup } from '@shared/osharp/osharp.model';
import { STData, STColumn } from '@delon/abc';
import { FunctionViewComponent } from '@shared/components/function-view/function-view.component';

@Component({
    selector: 'app-version',
    templateUrl: './version.component.html',
    styles: []
})
export class VersionComponent extends STComponentBase implements OnInit {

    constructor(injector: Injector) {
        super(injector);
        this.moduleName = 'version';
    }

    ngOnInit() {
        super.InitBase();
    }

    protected GetSTColumns(): STColumn[] {
        return [
            {
                title: '操作', fixed: 'left', width: 65, buttons: [{
                    text: '操作', children: [
                        { text: '编辑', icon: 'edit', acl: 'Root.Admin.Identity.User.Update', iif: row => row.Updatable, click: row => this.edit(row) },
                        { text: '删除', icon: 'delete', type: 'del', acl: 'Root.Admin.Identity.User.Delete', iif: row => row.Deletable, click: row => this.delete(row) },
                        { text: '查看配置', icon: 'security-scan', acl: 'Root.Admin.Security.UserFunction', click: row => this.viewFunction(row) },
                    ]
                }]
            },
            { title: '编号', index: 'Id', type: 'number' },
            { title: '版本名称', index: 'Name', sort: true, editable: true, ftype: 'string', filterable: true, className: 'max-100' },
            { title: '是否已删除', index: 'IsLocked', sort: true, type: 'yn', editable: true, filterable: true },
            { title: '创建时间', index: 'CreatedTime', sort: true, type: 'date', filterable: true },
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

    // #region 查看配置

    functionTitle: string;
    functionVisible = false;
    functionReadUrl: string;
    @ViewChild('function', { static: false }) function: FunctionViewComponent;

    private viewFunction(row: STData) {
        this.functionTitle = `查看用户功能 - ${row.Id}. ${row.Name}`;
        this.functionVisible = true;

        this.functionReadUrl = `api/admin/userfunction/readfunctions?userId=${row.Id}`;
        let filter: FilterGroup = new FilterGroup();
        setTimeout(() => {
            this.function.reload(filter);
        }, 100);
    }

    closeFunction() {
        this.functionVisible = false;
    }
    // #endregion
}
