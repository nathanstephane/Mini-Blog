import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment.development';
import { Post } from '../models/post.model';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class PostService {

  constructor(private http: HttpClient) { }

  //member variable
  baseUrl = environment.apiBaseUrl;

  //methods
  getAllPosts(): Observable<Post[]>
  {
    return this.http.get<Post[]>(this.baseUrl + '/api/Post');
  }

  getPostById(id: string): Observable<Post>{
    return this.http.get<Post>(this.baseUrl + '/api/Post/'+id); 
  }
}
