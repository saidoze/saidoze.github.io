import { NgModule } from '@angular/core';

import {
  MatButtonModule,
  MatSidenavModule,
} from '@angular/material';

const ANGULAR_MATERIAL_MODULES = [
  MatButtonModule,
  MatSidenavModule,
];

@NgModule({
  imports: ANGULAR_MATERIAL_MODULES,
  exports: ANGULAR_MATERIAL_MODULES
})
export class MaterialModule { }
