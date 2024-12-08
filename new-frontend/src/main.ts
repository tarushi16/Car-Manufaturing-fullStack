import { bootstrapApplication } from '@angular/platform-browser';
import { appConfig } from './app/app.config'; // Import your appConfig
import { AppComponent } from './app/app.component'; // Import your standalone AppComponent
import { enableProdMode } from '@angular/core';
import { platformBrowserDynamic } from '@angular/platform-browser-dynamic';
import { AppModule } from './app/app.module';
// Bootstrap the standalone component using bootstrapApplication
// bootstrapApplication(AppComponent, appConfig)
//   .catch((err) => console.error(err));

  platformBrowserDynamic().bootstrapModule(AppModule).catch((err) => console.error(err));
