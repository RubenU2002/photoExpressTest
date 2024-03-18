import { Component, OnInit } from '@angular/core';
import { ModificationDetailsService } from '../services/modificationDetailsService';
import { eventResponse } from '../models/eventResponse';

@Component({
  selector: 'app-modification-details',
  standalone: true,
  imports: [],
  templateUrl: './modification-details.component.html',
  styleUrl: './modification-details.component.css'
})
export class ModificationDetailsComponent implements OnInit{
  constructor(private modificationDetailsService: ModificationDetailsService) {}
  modificationDetail: { before: eventResponse, after: eventResponse } | null = null;

  ngOnInit(): void {
    this.modificationDetail = this.modificationDetailsService.getModificationDetail();
    console.log(this.modificationDetail)
  }

}
