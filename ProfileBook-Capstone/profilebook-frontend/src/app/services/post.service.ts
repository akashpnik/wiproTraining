import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';
import { AuthService } from './auth.service';
import { Post, CreatePostRequest } from '../models/post.model';

@Injectable({
  providedIn: 'root'
})
export class PostService {
  private apiUrl = environment.apiUrl;

  constructor(
    private http: HttpClient,
    private authService: AuthService
  ) {}

  // Get all posts (existing)
  getAllPosts(): Observable<Post[]> {
    return this.http.get<Post[]>(`${this.apiUrl}/Posts`, {
      headers: this.authService.getAuthHeaders()
    });
  }

  // ✅ ADD THIS: Create new post
 // Update the createPost method to send FormData instead of JSON
createPost(post: CreatePostRequest, imageFile?: File): Observable<Post> {
  console.log('Creating post with FormData:', post); // Debug log
  
  // Create FormData for multipart/form-data
  const formData = new FormData();
  formData.append('Content', post.content);
  
  // If image file is provided, append it
  if (imageFile) {
    formData.append('PostImage', imageFile, imageFile.name);
  }
  
  // Send FormData (don't set Content-Type header, let browser set it automatically)
  return this.http.post<Post>(`${this.apiUrl}/Posts`, formData, {
    headers: this.authService.getAuthHeaders().delete('Content-Type') // Remove Content-Type to let browser set multipart boundary
  });
}


  // Get posts by specific user (existing)
  getUserPosts(userId: number): Observable<Post[]> {
    return this.http.get<Post[]>(`${this.apiUrl}/Posts/user/${userId}`, {
      headers: this.authService.getAuthHeaders()
    });
  }
}
