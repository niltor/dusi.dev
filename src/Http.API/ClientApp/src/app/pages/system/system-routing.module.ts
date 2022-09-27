import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AuthGuard } from 'src/app/auth/auth.guard';

const routes: Routes = [
    {
        path: 'system',
        // component: LayoutComponent,
        // data: { reuse: true },
        canActivate: [AuthGuard],
        canActivateChild: [AuthGuard],
        children:
            [
                // { path: '', pathMatch: 'full', redirectTo: 'resource/index' },
            ]
    }
];

@NgModule({
    imports: [RouterModule.forRoot(routes)],
    exports: [RouterModule]
})
export class SystemRoutingModule { }
