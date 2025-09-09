import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';
import { AuthService } from './auth.service';
import { Post } from '../models/post.model';

@Injectable({
  providedIn: 'root'
})
export class AdminService {
  private apiUrl = environment.apiUrl;

  constructor(
    private http: HttpClient,
    private authService: AuthService
  ) {}

  // Get all pending posts for admin review
  getPendingPosts(): Observable<Post[]> {
    return this.http.get<Post[]>(`${this.apiUrl}/Admin/pending-posts`, {
      headers: this.authService.getAuthHeaders()
    });
  }

  // ✅ CORRECT: Approve post using POST with request body
  approvePost(postId: number): Observable<any> {
  console.log('Approving post ID:', postId);
  
  // ✅ CORRECT: Send postId as query parameter, not in body
  const url = `${this.apiUrl}/Admin/approve-post?postId=${postId}`;
  
  return this.http.post(url, null, { // null = empty body
    headers: this.authService.getAuthHeaders()
  });
}

// ✅ CORRECT: Reject post using POST with path parameter
  rejectPost(postId: number): Observable<any> {
    console.log('Rejecting post ID:', postId);
    return this.http.post(`${this.apiUrl}/Admin/reject-post/${postId}`, 
      {}, // Empty request body
      {
        headers: this.authService.getAuthHeaders()
      }
    );
  }

  // Get all posts for admin overview
  getAllPostsForAdmin(): Observable<Post[]> {
    return this.http.get<Post[]>(`${this.apiUrl}/Admin/post`, {
      headers: this.authService.getAuthHeaders()
    });
  }
}
