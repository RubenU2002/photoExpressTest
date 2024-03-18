import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ApiService } from '../services/api-service.service';
import { environment } from '../../environment/environment';
import { eventModificationLog } from '../models/eventModificationsLog';
import { ModificationDetailsService } from '../services/modificationDetailsService';

@Component({
  selector: 'app-event-modification-log',
  standalone: true,
  imports: [],
  templateUrl: './event-modification-log.component.html',
  styleUrl: './event-modification-log.component.css'
})
export class EventModificationLogComponent implements OnInit{
  eventId : string = '';
  modificationsLog : eventModificationLog[] = []
  constructor(private activatedRoute: ActivatedRoute, private apiService: ApiService, private modificationDetailsService: ModificationDetailsService, private router: Router) {}

  ngOnInit(): void {
    this.eventId = this.activatedRoute.snapshot.paramMap.get('eventId') as string;
    this.loadModificationLog();
  }
  loadModificationLog(){
    this.apiService.GetById<eventModificationLog[]>(environment.modificationLog+"AllEvents/",this.eventId).subscribe({
      next:(response)=>{
        if (response.isSuccess && response.data) {
          this.modificationsLog = response.data;
        } else {
          console.error('Failed to load events:', response.errorMessage);
        }
      }
    })
  }
  cargarDetalles(before: string, after: string) {
    this.modificationDetailsService.setModificationDetail(before, after);
    this.router.navigate(['/modificationDetails']);
  }
  
}
