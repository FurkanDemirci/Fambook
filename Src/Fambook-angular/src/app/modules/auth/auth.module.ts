import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SharedModule } from 'src/app/shared/shared.module';
import { RegisterComponent } from './pages/register/register.component';
import { AuthRoutingModule } from './auth.routing';
import { LoginComponent } from './pages/login/login.component';

@NgModule({
    declarations: [
        RegisterComponent,
        LoginComponent,
    ],
    imports: [
        CommonModule,
        AuthRoutingModule,
        SharedModule,
    ],
})
export class AuthModule { }
