import { Component, Input, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Pledge } from 'src/app/models/pledge/pledge.model';
import { PledgeService } from 'src/app/services/pledge.service';
import { TokenStorageService } from 'src/app/services/token-storage.service';

@Component({
  selector: 'app-pledge-details',
  templateUrl: './pledge-details.component.html',
  styleUrls: ['./pledge-details.component.css']
})
export class PledgeDetailsComponent implements OnInit {
  
  pledge: Pledge = <Pledge>{};
  editable: boolean = false;

  constructor(private route: ActivatedRoute,
    private pledgeService: PledgeService,
    private tokenService: TokenStorageService) { }

  ngOnInit(): void {
    const id = String(this.route.snapshot.paramMap.get('id'));
    this.pledgeService.getPledge(id).subscribe({
      next: data => {
        console.log(data);
        this.pledge = data
        this.editable = this.pledge.pledger.authorId === this.tokenService.getUserId()
      },
      error: err => console.log(err)
    });
  }

}
