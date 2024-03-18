export interface EventRequest{
    InstitutionId? : string,
    NumberOfStudents : number,
    StartTime : Date,
    CapAndGown : boolean,
    ServiceCost : number,
    InstitutionName? : string,
    InstitutionAddress? : string
}