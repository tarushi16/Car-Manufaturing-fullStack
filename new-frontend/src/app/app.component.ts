import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';

// import { LoginComponent } from './authentication/login/login.component';

// @Component({
//   selector: 'app-root',
//   standalone: true,
//   imports: [RouterOutlet, LoginComponent], // Combine the imports into a single array
//   templateUrl: './app.component.html',
//   styleUrls: ['./app.component.scss']
// })
@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  title = 'new-frontend';
}

