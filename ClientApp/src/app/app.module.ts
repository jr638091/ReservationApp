import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { NavMenuDesktopComponent } from './nav-menu/nav-menu-desktop.component';
import { NavMenuMobileComponent } from './nav-menu/nav-menu-mobile.component';
import { SelectorInputComponent } from './input/selector/selector.component';
import { ReservationService } from 'src/Services/ReservationService';
import { HomeTopBannerComponent } from './home/home-top-banner/home-top-banner.component';
import { ListGroupItemComponent } from './list-group/list-group-item/list-group-item.component';
import { ReservationCreateComponent } from './reservation/reservation-create/reservation-create.component';

@NgModule({
  declarations: [
    AppComponent,
    HomeTopBannerComponent,
    ListGroupItemComponent,
    NavMenuComponent,
    NavMenuDesktopComponent,
    NavMenuMobileComponent,
    SelectorInputComponent,
    ReservationCreateComponent,
    HomeComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    RouterModule.forRoot([
      { path: '', component: HomeComponent, pathMatch: 'full' },
      { path: 'create-reservation', component: ReservationCreateComponent },
      { path: '**', redirectTo: ''}
    ]),
  ],
  providers: [ReservationService],
  bootstrap: [AppComponent]
})
export class AppModule { }
