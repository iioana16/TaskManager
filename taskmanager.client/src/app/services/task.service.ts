import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Task } from '../models/task.model';
import { environment } from 'src/app/environment/environment';

@Injectable({
  providedIn: 'root'
})
export class TaskService {

  private apiUrl = environment.apiBaseUrl + '/api/tasks';  
  

  constructor(private http: HttpClient) { }

  
  getAllTasks(): Observable<Task[]> {
    return this.http.get<Task[]>(this.apiUrl);
  }


  getTask(id: string): Observable<Task> {
    return this.http.get<Task>(`${this.apiUrl}/${id}`);
  }

 
  addTask(task: Task): Observable<string> {
    return this.http.post(this.apiUrl, task, { responseType: 'text' });
  }


  updateTask(id: string, task: Task): Observable<any> {
    return this.http.put(`${this.apiUrl}/${id}`, task);
  }


  deleteTask(id: string): Observable<any> {
    return this.http.delete(`${this.apiUrl}/${id}`);
  }

  searchTasks(title: string): Observable<Task[]> {
    return this.http.get<Task[]>(
      `${this.apiUrl}/search?title=${encodeURIComponent(title)}`
    );
  }
}
