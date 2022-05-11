import { Component, Input, OnInit, ViewChild } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { SectionPreview } from 'src/app/models/section/section-preview.model';

@Component({
  selector: 'app-section-list',
  templateUrl: './section-list.component.html',
  styleUrls: ['./section-list.component.css']
})
export class SectionListComponent implements OnInit {

  @Input()
  sectionPreviews: SectionPreview[] = [];
  
  projects:Project[] = [];
  form: FormGroup = <FormGroup>{};

  last_selection = null;

  @ViewChild('matSelect') matSelect = null;

  constructor() { 
    this.projects = [
      new Project("Volvo"),
      new Project("Saab"),
      new Project("Mercedes"),
      new Project("Audi")
    ]

    // Setup form
    this.form = new FormGroup({
      project: new FormControl(this.projects)      
    });
  }

  okClicked() {
    this.last_selection = this.form.controls['project'].value
    // this.matSelect.close()
  }

  cancelClicked() {
    this.form.controls['project'].setValue(this.last_selection)
    // this.matSelect.close()
  }

  OnSelectChanged(event_data: any) {
    console.log(event_data)
  }

  ngOnInit() {
    this.last_selection = this.form.controls['project'].value
  }

}

export class Project{
  name:string;
  
  constructor(name:string){
    this.name=name;
  }
}