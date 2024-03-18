import { Injectable } from '@angular/core';
import { eventResponse } from '../models/eventResponse';

@Injectable({
  providedIn: 'root'
})
export class ModificationDetailsService {
  private modificationDetail: { before: eventResponse, after: eventResponse } | null = null;

  constructor() { }

  setModificationDetail(before: string, after: string) {
    const beforeObj = JSON.parse(before);
    const afterObj = JSON.parse(after);

    this.modificationDetail = {
        before: this.mapEventResponse(beforeObj),
        after: this.mapEventResponse(afterObj)
    };
}

private mapEventResponse(obj: any): eventResponse {
    return {
        eventId: obj.EventId,
        institutionId: obj.InstitutionId,
        numberOfStudents: obj.NumberOfStudents,
        startTime: new Date(obj.StartTime),
        serviceCost: obj.ServiceCost,
        capAndGown: obj.CapAndGown,
        institution: {
            institutionId: obj.Institution.InstitutionId,
            institutionName: obj.Institution.InstitutionName,
            institutionAddress: obj.Institution.InstitutionAddress
        }
    };
}


  getModificationDetail() {
    return this.modificationDetail;
  }
}
