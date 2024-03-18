import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class dateService {
  constructor() { }
  public formateDate(dateToFormat : string): string{
    let date = new Date(dateToFormat)
    const anio = date.getFullYear();
    const mes = (date.getMonth() + 1).toString().padStart(2, "0");
    const dia = date.getDate().toString().padStart(2, "0");
    const fechaFormateada = `${anio}-${mes}-${dia}`;

    // Formatear la hora en "hh:mm am/pm"
    const horas = date.getHours();
    const minutos = date.getMinutes();
    const amPm = horas >= 12 ? "pm" : "am";
    const horasFormateadas = (horas % 12 || 12).toString().padStart(2, "0");
    const minutosFormateados = minutos.toString().padStart(2, "0");
    const horaFormateada = `${horasFormateadas}:${minutosFormateados} ${amPm}`;
    return fechaFormateada+" "+horaFormateada;
  }
  public getCurrentDateTime(): string {
    const now = new Date();
    const year = now.getFullYear();
    const month = this.padTo2Digits(now.getMonth() + 1);
    const day = this.padTo2Digits(now.getDate());
    const hours = this.padTo2Digits(now.getHours());
    const minutes = this.padTo2Digits(now.getMinutes());

    return `${year}-${month}-${day}T${hours}:${minutes}`;
  }
  public padTo2Digits(num: number): string {
    return num.toString().padStart(2, '0');
  }
}
