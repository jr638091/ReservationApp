import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { CounterComponent } from './counter/counter.component';
import { FetchDataComponent } from './fetch-data/fetch-data.component';
import { NavMenuDesktopComponent } from './nav-menu/nav-menu-desktop.component';
import { NavMenuMobileComponent } from './nav-menu/nav-menu-mobile.component';
import { HomeDesktopComponent } from './home/home-desktop.component';
import { HomeMobileComponent } from './home/home-mobile.component';
import { SelectorInputComponent } from './input/selector/selector.component';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    NavMenuDesktopComponent,
    NavMenuMobileComponent,
    SelectorInputComponent,
    HomeComponent,
    HomeDesktopComponent,
    HomeMobileComponent,
    CounterComponent,
    FetchDataComponent,
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    RouterModule.forRoot([
      { path: '', component: HomeComponent, pathMatch: 'full' },
      { path: 'counter', component: CounterComponent },
      { path: 'fetch-data', component: FetchDataComponent },
    ])
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
