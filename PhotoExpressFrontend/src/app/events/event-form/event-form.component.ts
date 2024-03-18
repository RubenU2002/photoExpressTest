import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { ReactiveFormsModule } from '@angular/forms';
import { HigherEducationInstitutionResponse } from '../../models/HigherEducationInstitution';
import { ApiService } from '../../services/api-service.service';
import { environment } from '../../../environment/environment';
import { AlertComponent } from '../../alert/alert.component';
import { EventRequest } from '../../models/eventRequest';
import { eventResponse } from '../../models/eventResponse';
import { dateService } from '../../services/dateService';

@Component({
  selector: 'app-event-form',
  standalone: true,
  imports: [ReactiveFormsModule,AlertComponent],
  templateUrl: './event-form.component.html',
  styleUrl: './event-form.component.css'
})
export class EventFormComponent implements OnInit{
  @Output() eventAdded = new EventEmitter<boolean>();
  showForm : boolean = false;
  showNewInsitutionForm : boolean = false;
  showAlert : boolean = false;
  alertMsj : string = ''
  alertType : string = 'success'
  higherInsitutions : HigherEducationInstitutionResponse[] = []
  eventForm = new FormGroup({
    InstitutionId: new FormControl(''),
    NumberOfStudents: new FormControl(1,[Validators.required,Validators.min(1)]),
    StartTime: new FormControl(this.dateService.getCurrentDateTime()),
    CapAndGown: new FormControl(false),
    ServiceCost: new FormControl(200,[Validators.required,Validators.min(1)]),
    InstitutionName : new FormControl(''),
    InstitutionAddress : new FormControl('')
  });
  constructor(private apiService: ApiService, public dateService : dateService) {}

  ngOnInit(): void {
    this.loadInstitutions();
  }
  private loadInstitutions(): void {
    this.apiService.Get<HigherEducationInstitutionResponse[]>(environment.higherEducationInstitutionsEndpoint).subscribe({
      next: (response) => {
        if (response.isSuccess && response.data) {
          this.higherInsitutions = response.data;
        } else {
          console.error('Failed to load events:', response.errorMessage);
        }
      },
      error: (error) => console.error('API error:', error)
    });
  }
  toggleNewInsitutionForm(){
    if(this.eventForm.value.InstitutionId==='0'){
      this.showNewInsitutionForm=true;
    }else
    this.showNewInsitutionForm = false;
  }
  toggleForm(){
    this.showForm = !this.showForm;
  }
  saveEvent(){
    if(!this.AreInputsCorrect()){
      this.showAlert = true;
      return;
    }
    this.showAlert = false;
    this.createEvent();
  }
  private createEvent(){
    let data = this.eventForm.value;
    let eventRequest : EventRequest = {
      CapAndGown : data.CapAndGown==null? false : data.CapAndGown,
      ServiceCost : data.ServiceCost==null? 1 : data.ServiceCost,
      NumberOfStudents : data.NumberOfStudents == null ? 1 : data.NumberOfStudents,
      StartTime : data.StartTime==null? new Date : new Date(data.StartTime),
      InstitutionName : data.InstitutionName==null? "" : data.InstitutionId=='0'? data.InstitutionName : "",
      InstitutionAddress : data.InstitutionAddress==null? "" :  data.InstitutionId=='0'? data.InstitutionAddress : ""
    }
    if(data.InstitutionId!=='0' && data.InstitutionId!=null)
      eventRequest.InstitutionId = data.InstitutionId
    this.apiService.Post<EventRequest,eventResponse>(
      environment.eventsEndpoint,
      eventRequest
    ).subscribe({
      next: (response)=>{
        this.alertMsj="Se creo correctamente el evento"
        this.alertType='success';
        this.showAlert = true;
        this.showForm=false;
        this.eventAdded.emit(true);
      },
      error: (err)=>{
        this.alertMsj="Error en la creación del evento"
        this.alertType='error';
        this.showAlert = true;
        this.showForm=false;
      }
    })
  }  
  private AreInputsCorrect(): boolean {
    const emailRegex = new RegExp(/^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$/);
    let data = this.eventForm.value;
    if(data.InstitutionId ==''){
      this.alertMsj = "Seleccione una institución";
      this.alertType = 'error';
      return false;
    }
    if (data.InstitutionId === '0') {
      if (data.InstitutionName === "" || data.InstitutionAddress === "" || !emailRegex.test(data.InstitutionAddress == null ? "": data.InstitutionAddress)) {
        this.alertMsj = "Escriba un nombre válido para la institución y asegúrese de que el correo electrónico sea correcto.";
        this.alertType = 'error';
        return false;
      }
    }
    if(data.NumberOfStudents == null || data.NumberOfStudents<1){
      this.alertMsj = "Deben haber más de un estudiantes";
      this.alertType = 'error';
      return false;
    }
    if(data.ServiceCost==null || data.ServiceCost<1){
      this.alertMsj = "El costo debe ser mayor a 1$";
      this.alertType = 'error';
      return false;
    }
    return true;
  }
}
