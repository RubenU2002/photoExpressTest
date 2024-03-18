import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../environment/environment'; // Asegúrate de que la ruta es correcta
import { Response } from '../models/response'; // Asume una interfaz Response existente

@Injectable({
  providedIn: 'root'
})
export class ApiService {
  private baseUrl = environment.apiUrl;

  constructor(private http: HttpClient) {}

  // GET que devuelve una respuesta de tipo Q
  public Get<Q>(endpoint: string): Observable<Response<Q>> {
    return this.http.get<Response<Q>>(`${this.baseUrl}${endpoint}`);
  }

  // GET por ID que devuelve una respuesta de tipo Q
  public GetById<Q>(endpoint: string, id: string): Observable<Response<Q>> {
    return this.http.get<Response<Q>>(`${this.baseUrl}${endpoint}${id}`);
  }

  // POST con solicitud de tipo T y respuesta de tipo Q
  public Post<T, Q>(endpoint: string, request: T): Observable<Response<Q>> {
    return this.http.post<Response<Q>>(`${this.baseUrl}${endpoint}`, request);
  }

  // PUT con solicitud de tipo T y respuesta de tipo Q
  public Put<T, Q>(endpoint: string, request: T, id: string): Observable<Response<Q>> {
    return this.http.put<Response<Q>>(`${this.baseUrl}${endpoint}${id}`, request);
  }

  // DELETE que asume la petición no lleva cuerpo y devuelve una respuesta de tipo Q
  public Delete<Q>(endpoint: string,id : string): Observable<Response<Q>> {
    return this.http.delete<Response<Q>>(`${this.baseUrl}${endpoint}${id}`);
  }
}
