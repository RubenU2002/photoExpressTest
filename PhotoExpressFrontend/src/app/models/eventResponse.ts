import { HigherEducationInstitutionResponse } from "./HigherEducationInstitution";

export interface eventResponse{
    eventId : string,
    institutionId : string,
    numberOfStudents : number,
    startTime : Date,
    serviceCost : number,
    capAndGown : boolean,
    institution : HigherEducationInstitutionResponse
}