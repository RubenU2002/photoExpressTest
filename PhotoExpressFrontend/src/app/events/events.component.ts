import { Component, OnInit } from '@angular/core';
import { ApiService } from '../services/api-service.service';
import { eventResponse } from '../models/eventResponse';
import { environment } from '../../environment/environment';
import { EventFormComponent } from './event-form/event-form.component';
import { Router } from '@angular/router';
import { dateService } from '../services/dateService';

@Component({
  imports:[EventFormComponent],
  standalone:true,
  selector: 'app-events',
  templateUrl: './events.component.html',
  styleUrls: ['./events.component.css'],
})
export class EventsComponent implements OnInit {
  events: eventResponse[] = [];

  constructor(private eventService: ApiService, private router: Router,public dateService : dateService) {}


  ngOnInit(): void {
    this.loadEvents();
  }

  public loadEvents(): void {
    this.eventService.Get<eventResponse[]>(environment.eventsEndpoint).subscribe({
      next: (response) => {
        if (response.isSuccess && response.data) {
          this.events = response.data;
        } else {
          console.error('Failed to load events:', response.errorMessage);
        }
      },
      error: (error) => console.error('API error:', error)
    });
  }
  confirmDelete(eventId: string): void {
    const isConfirmed = confirm('¿Está seguro de eliminar este evento?');
    if (isConfirmed) {
      this.deleteEvent(eventId);
    }
  }
  
  
  deleteEvent(eventId: string): void {
    this.eventService.Delete<eventResponse>(environment.eventsEndpoint,eventId).subscribe({
      next:(response)=>{
        this.loadEvents();
      },
      error : (err)=>{
      }
    })
  }
  editEvent(eventId: string) {
    this.router.navigate(['/EditEvent', eventId]);
  } 
  modificationLog(eventId : string){
    this.router.navigate(['/EventModificationLog', eventId]);
  } 
}
