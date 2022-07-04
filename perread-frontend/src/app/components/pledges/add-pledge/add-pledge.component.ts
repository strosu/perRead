import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { PledgeCommand } from 'src/app/models/pledge/pledge--command.model';
import { PledgeService } from 'src/app/services/pledge.service';

@Component({
  selector: 'app-add-pledge',
  templateUrl: './add-pledge.component.html',
  styleUrls: ['./add-pledge.component.css']
})
export class AddPledgeComponent implements OnInit {

  pledgeCommand: PledgeCommand = <PledgeCommand>{};

  constructor(private pledgeService: PledgeService,
    private router: Router,
    private route: ActivatedRoute
    ) { }

  ngOnInit(): void {
    this.pledgeCommand.requestId = String(this.route.snapshot.paramMap.get('requestId'));
  }

  addPledge() : any {
    this.pledgeService.addPledge(this.pledgeCommand).subscribe(
      {
        next: data => {
          console.log(data);
          this.router.navigate([`/requests/${this.pledgeCommand.requestId}`], { replaceUrl: true });
        },
        error : err => console.log(err)
      }
    );
  }
}
