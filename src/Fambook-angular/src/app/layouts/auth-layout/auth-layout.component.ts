import { Component, OnInit } from '@angular/core';
import { ParticlesConfig } from './particles-config';
declare let particlesJS: any; // Required to be properly interpreted by TypeScript.

@Component({
  selector: 'app-auth-layout',
  templateUrl: './auth-layout.component.html',
  styleUrls: ['./auth-layout.component.css']
})
export class AuthLayoutComponent implements OnInit {

  constructor() { }

  ngOnInit(): void {
    this.invokeParticles();
  }

  public invokeParticles(): void {
    particlesJS('particles-js', ParticlesConfig, function () { });
  }
}
