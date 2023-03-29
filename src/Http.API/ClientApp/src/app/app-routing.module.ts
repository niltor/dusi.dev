import { NgModule } from '@angular/core';
import { Routes, RouterModule, RouteReuseStrategy } from '@angular/router';
import { CustomRouteReuseStrategy } from './custom-route-strategy';
import { LoginComponent } from './pages/system/login/login.component';

const routes: Routes = [
    { path: '', redirectTo: 'news', pathMatch: 'full' },
    { path: 'index', redirectTo: 'index', pathMatch: 'full' },
    { path: 'account', redirectTo: 'account', pathMatch: 'full' },
    { path: 'login', redirectTo: 'login', pathMatch: 'full' },
    { path: 'system/login', component: LoginComponent },
    {
        path: 'system',
        loadChildren: () => import('./pages/system/system.module')
            .then(m => m.SystemModule),
        data: { reuse: true },
    },
    {
        path: 'workspace',
        loadChildren: () => import('./pages/workspace/workspace.module')
            .then(m => m.WorkspaceModule),
        data: { reuse: true },
    }
];

@NgModule({
    imports: [RouterModule.forRoot(routes)],
    exports: [RouterModule],
    providers: [{
        provide: RouteReuseStrategy,
        useClass: CustomRouteReuseStrategy
    }]
})
export class AppRoutingModule { }