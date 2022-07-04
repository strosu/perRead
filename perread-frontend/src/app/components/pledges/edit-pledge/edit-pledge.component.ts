import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { PledgeCommand } from 'src/app/models/pledge/pledge--command.model';
import { Pledge } from 'src/app/models/pledge/pledge.model';
import { PledgeService } from 'src/app/services/pledge.service';

@Component({
  selector: 'app-edit-pledge',
  templateUrl: './edit-pledge.component.html',
  styleUrls: ['./edit-pledge.component.css']
})
export class EditPledgeComponent implements OnInit {

  pledgeCommand: PledgeCommand = <PledgeCommand>{};
  pledge: Pledge = <Pledge>{};

  constructor(private pledgeService : PledgeService,
    private route: ActivatedRoute,
    private router: Router) { }

  ngOnInit(): void {
    const id = String(this.route.snapshot.paramMap.get('id'));
    this.pledgeCommand.pledgeId = id;

    this.pledgeService.getPledge(id).subscribe({
      next: data => {
        this.pledge = data;
        this.pledgeCommand.requestId = this.pledge.parentRequest.requestId;
        this.pledgeCommand.totalPledgeAmount = this.pledge.totalTokenSum;
        this.pledgeCommand.upfrontPledgeAmount = this.pledge.tokensOnAccept;
      }
    });
  }

  savePledge() {
    this.pledgeService.editPledge(this.pledgeCommand).subscribe(
      {
        next: data => {
          console.log(data);
          this.router.navigate([`/requests/${this.pledgeCommand.requestId}`], { replaceUrl: true });
        },
        error: err => console.log(err)
      }
    );
  }

}
