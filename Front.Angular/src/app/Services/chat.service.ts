import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ChatService {
  private apiUrl = 'https://localhost:7183/api/Chat';

  constructor(private http: HttpClient) { }

  sendMessage(receiver: string, message: string): Observable<any> {
    return this.http.post<any>(`${this.apiUrl}/SendMessage`, { receiver, message });
  }

  getChatHistory(receiver: string): Observable<any> {
    return this.http.get<any>(`${this.apiUrl}/ChatHistory?receiver=${receiver}`);
  }
}
