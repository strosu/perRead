import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  title = 'perread-frontend';

  constructor() {  }
  
  ngOnInit(): void { }

  // logout(): void {
  //   this.tokenService.signout();
  //   window.location.reload();
  // }
}
