import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';
import { AuthService } from './auth.service';
import { Comment, CreateCommentRequest } from '../models/comment.model';

@Injectable({
  providedIn: 'root'
})
export class CommentService {
  private apiUrl = environment.apiUrl;

  constructor(
    private http: HttpClient,
    private authService: AuthService
  ) {}

  // Get comments for a specific post
    getComments(postId: number): Observable<Comment[]> {
    console.log(`Getting comments for post ${postId}`);
    return this.http.get<Comment[]>(`${this.apiUrl}/Posts/${postId}/comments`, {
      headers: this.authService.getAuthHeaders()
    });
  }
  // Add a new comment to a post
  addComment(commentData: CreateCommentRequest): Observable<Comment> {
    console.log('Adding comment:', commentData);
    
    const requestBody = {
      CommentText: commentData.commentText 
    };
    
    console.log('Request URL:', `${this.apiUrl}/Posts/${commentData.postId}/comments`);
    console.log('Request Body:', requestBody);
    console.log('Headers:', this.authService.getAuthHeaders());
    
    return this.http.post<Comment>(`${this.apiUrl}/Posts/${commentData.postId}/comments`, 
      requestBody,
      {
        headers: this.authService.getAuthHeaders()
      }
    );
  }

  // Delete a comment (optional - for future use)
  deleteComment(commentId: number): Observable<any> {
    return this.http.delete(`${this.apiUrl}/Posts/comments/${commentId}`, {
      headers: this.authService.getAuthHeaders()
    });
  }
}
