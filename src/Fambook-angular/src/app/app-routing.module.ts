import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ContentLayoutComponent } from './layouts/content-layout/content-layout.component';
import { AuthLayoutComponent } from './layouts/auth-layout/auth-layout.component';
import { NoAuthGuard } from './core/guards/no-auth.guard';
import { AuthGuard } from './core/guards/auth.guard';

const routes: Routes = [
    {
        path: '',
        redirectTo: '/auth/login',
        pathMatch: 'full'
    },
    {
        path: 'home',
        component: ContentLayoutComponent,
        canActivate: [NoAuthGuard],
    },
    {
        path: 'auth',
        component: AuthLayoutComponent,
        loadChildren: () =>
            import('./modules/auth/auth.module').then(m => m.AuthModule)
    },

    // Fallback when no prior routes is matched
    // TODO: make a 404 not found page
    { path: '**', redirectTo: '/auth/login', pathMatch: 'full' }
];

@NgModule({
    imports: [
        RouterModule.forRoot(routes)
    ],
    exports: [
        RouterModule
    ],
})
export class AppRoutingModule { }
