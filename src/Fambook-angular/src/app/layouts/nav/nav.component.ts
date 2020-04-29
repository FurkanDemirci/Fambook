import { Component, OnInit } from '@angular/core';
import { BreakpointObserver, Breakpoints } from '@angular/cdk/layout';
import { Observable } from 'rxjs';
import { map, shareReplay } from 'rxjs/operators';
import { AuthService } from 'src/app/core/services/auth/auth.service';
import { User } from 'src/app/shared/models/user.model';
import { ProfileService } from 'src/app/core/services/profile/profile.service';
import { Profile } from 'src/app/shared/models/profile.model';
import { DomSanitizer } from '@angular/platform-browser';
import { UserService } from 'src/app/core/services/user/user.service';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {
  user: User;
  image: any;

  isHandset$: Observable<boolean> = this.breakpointObserver.observe(Breakpoints.Handset)
    .pipe(
      map(result => result.matches),
      shareReplay()
    );

  constructor(
    private breakpointObserver: BreakpointObserver,
    public authService: AuthService,
    private userService: UserService,
    private profileService: ProfileService,
    private sanitizer: DomSanitizer) {
  }

  ngOnInit(): void {
    this.getUser();
    this.getProfilePictrue();
  }

  getUser() {
    this.userService.get(this.authService.currentUser.userId)
      .subscribe(
        (response) => {
          this.user = new User().deserialize(response);
          this.decodeImg();
        }, error => {
          console.log(error);
        }
      );
  }


  getProfilePictrue() {
    this.profileService.get(this.authService.currentUser.userId)
      .subscribe(
        (response) => {
          this.user.profile = new Profile().deserialize(response);
          this.decodeImg();
        }, error => {
          console.log(error);
        }
      );
  }

  decodeImg() {
    let objectURL = 'data:image/png;base64,' + this.user.profile.profilePicture;
    this.image = this.sanitizer.bypassSecurityTrustUrl(objectURL);
  }
}
