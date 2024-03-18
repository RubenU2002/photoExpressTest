import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ApiService } from '../services/api-service.service';
import { FormGroup, FormControl, Validators, ReactiveFormsModule } from '@angular/forms'; 
import { eventResponse } from '../models/eventResponse';
import { environment } from '../../environment/environment';
import { HigherEducationInstitutionResponse } from '../models/HigherEducationInstitution';
import { Router } from '@angular/router';
import { EventRequest } from '../models/eventRequest';


@Component({
  selector: 'app-edit-event',
  standalone:true,
  templateUrl: './edit-event.component.html',
  styleUrls: ['./edit-event.component.css'],
  imports: [ReactiveFormsModule], 
})
export class EditEventComponent implements OnInit {
  eventId: string = '';
  eventForm: FormGroup;
  higherInsitutions : HigherEducationInstitutionResponse[] = []
  constructor(private activatedRoute: ActivatedRoute, private apiService: ApiService,private router: Router) {
    this.eventForm = new FormGroup({
      NumberOfStudents: new FormControl(1, [Validators.required, Validators.min(1)]),
      StartTime: new FormControl('', Validators.required),
      CapAndGown: new FormControl(false),
      ServiceCost: new FormControl(1, [Validators.required, Validators.min(1)]),
      InstitutionId: new FormControl('',Validators.required)
    });
  }

  ngOnInit() {
    this.eventId = this.activatedRoute.snapshot.paramMap.get('eventId') as string;
    this.loadInstitutions();
    this.loadEventDetails();
  }
  private loadInstitutions(): void {
    this.apiService.Get<HigherEducationInstitutionResponse[]>(environment.higherEducationInstitutionsEndpoint).subscribe({
      next: (response) => {
        if (response.isSuccess && response.data) {
          this.higherInsitutions = response.data.filter(a => a.institutionId !== this.eventId);
        } else {
          console.error('Failed to load events:', response.errorMessage);
        }
      },
      error: (error) => console.error('API error:', error)
    });
  }
  loadEventDetails() {
    if (this.eventId) {
      this.apiService.GetById<eventResponse>(environment.eventsEndpoint,this.eventId).subscribe({
        next: (resp) => {
          if (resp && resp.data) {
            let response = resp.data
            this.eventForm.setValue({
              NumberOfStudents: response.numberOfStudents,
              StartTime: response.startTime,
              CapAndGown: response.capAndGown,
              ServiceCost: response.serviceCost,
              InstitutionId: response.institutionId
            });
          }
        },
        error: (error) => console.error('Error loading event details:', error)
      });
    }
  }

  formatDateTimeLocal(dateTimeString: string): string {
    const date = new Date(dateTimeString);
    return date.toISOString().slice(0, 16); // Formato 'YYYY-MM-DDTHH:MM', adecuado para input[type="datetime-local"]
  }
  saveEvent(){
    let data = this.eventForm.value;
    let eventRequest : EventRequest = {
      CapAndGown : data.CapAndGown==null? false : data.CapAndGown,
      ServiceCost : data.ServiceCost==null? 1 : data.ServiceCost,
      NumberOfStudents : data.NumberOfStudents == null ? 1 : data.NumberOfStudents,
      StartTime : data.StartTime==null? new Date : new Date(data.StartTime),
      InstitutionName : data.InstitutionName==null? "" : data.InstitutionName,
      InstitutionAddress : data.InstitutionAddress==null? "" : data.InstitutionAddress
    }
    if(data.InstitutionId!=='0' && data.InstitutionId!=null)
      eventRequest.InstitutionId = data.InstitutionId
    this.apiService.Put<EventRequest,eventResponse>(
      environment.eventsEndpoint,
      eventRequest,
      this.eventId
    ).subscribe({
      next: (response)=>{
        alert("Evento modificado correctamente")
        this.router.navigate(['/events']);
      },
      error: (err)=>{
      }
    })
  }
  cancel() {
    this.router.navigate(['/events']);
  }
  
}

