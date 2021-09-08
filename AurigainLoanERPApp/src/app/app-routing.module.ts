import { NgModule } from '@angular/core';
import { NoPreloading, RouterModule, Routes } from '@angular/router';
import { PageNotFoundComponent } from './Content/Common/page-not-found/page-not-found.component';
import { AuthenticationGuard } from './Shared/Helper/authentication.guard';

const routes: Routes = [
 { path: '',
  redirectTo: '',
  pathMatch: 'full'
},

{ path: '**',
component : PageNotFoundComponent,
pathMatch: 'full',
canActivate : [AuthenticationGuard]
},


{path : "admin", loadChildren :()=> import('./Content/admin/admin.module').then(m=> m.AdminModule)  },
{path : "agent", loadChildren :()=> import('./Content/agent/agent.module').then(m=> m.AgentModule)  },
{path : "customer", loadChildren :()=> import('./Content/customer/customer.module').then(m=> m.CustomerModule)  }


];

@NgModule({
  imports: [RouterModule.forRoot(routes , {  preloadingStrategy: NoPreloading  }) ],
  exports: [RouterModule]
})
export class AppRoutingModule { }
