import { Component, Input, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { UserSettings } from 'src/app/models/user/user-settings.model';
import { UserService } from 'src/app/services/user.service';

@Component({
  selector: 'app-user-preferences',
  templateUrl: './user-preferences.component.html',
  styleUrls: ['./user-preferences.component.css']
})
export class UserPreferencesComponent implements OnInit {

  userSettings?: UserSettings; 

  constructor(private userService: UserService, private router: Router) { }

  ngOnInit(): void {
    this.userService.getCurrentUserSettings()
    .subscribe({
      next: data => {
        console.log(data);
        this.userSettings = data;
      },
      error: err => console.log(err)
    });
  }

  updateSettings() : void {
    if (this.userSettings == null) {
      return;
    }
    
    this.userService.updateCurrentUserSettings(this.userSettings).subscribe(
      {
        next: data => {
          console.log(data);
          this.router.navigate(['/'], { replaceUrl: true });
        },
        error: error => console.log(error)
      }
    );
  }

}
