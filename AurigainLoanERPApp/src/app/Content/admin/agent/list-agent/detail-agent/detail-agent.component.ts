import { Component, OnInit } from "@angular/core";
import { DomSanitizer } from "@angular/platform-browser";
import { ActivatedRoute, Router } from "@angular/router";
import { Routing_Url } from "src/app/Shared/Helper/constants";
import { AgentViewModel } from "src/app/Shared/Model/Agent/agent.model";
import { UserViewModel, UserKYCViewModel, UserNomineeViewModel, UserBankDetailViewModel, DocumentViewModel } from "src/app/Shared/Model/doorstep-agent-model/door-step-agent.model";
import { AgentService } from "src/app/Shared/Services/agent-services/agent.service";

@Component({
  selector: 'app-detail-agent',
  templateUrl: './detail-agent.component.html',
  styleUrls: ['./detail-agent.component.scss'],
  providers: [AgentService]

})
export class DetailAgentComponent implements OnInit {

 //#region <<Variable>>
 Id: number = 0;
 model = {} as AgentViewModel;
 get routing_Url() { return Routing_Url }

 //#endregion

 constructor(private readonly _userService: AgentService, private _activatedRoute: ActivatedRoute,
   private _router: Router, public domSanitizer: DomSanitizer) {
   if (this._activatedRoute.snapshot.params.id) {
     this.Id = this._activatedRoute.snapshot.params.id;
   }
   this.model.User = {} as UserViewModel;
   this.model.UserKYC = [] as UserKYCViewModel[];
   this.model.UserNominee = {} as UserNomineeViewModel;
   this.model.BankDetails = {} as UserBankDetailViewModel;
   this.model.Documents = [] as DocumentViewModel[];



 }
 //#region <<Method>>
 ngOnInit(): void {
   this.getDetail();
 }
 getDetail() {
   let serviceCall = this._userService.GetAgent(this.Id).subscribe(response => {
     serviceCall.unsubscribe();
     debugger
     if (response.IsSuccess) {
       if (response?.Data) {
         this.model = response?.Data as AgentViewModel;

       } else {
         this._router.navigate([`../${Routing_Url.AgentListUrl}`]);
       }
     }
   });

 }
 safeURL(url: string) {
   console.log(url)
   return this.domSanitizer.bypassSecurityTrustUrl(url);
 }

 //#endregion

}
