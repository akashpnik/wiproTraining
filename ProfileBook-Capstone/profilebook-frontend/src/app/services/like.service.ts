import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';
import { AuthService } from './auth.service';

@Injectable({
  providedIn: 'root'
})
export class LikeService {
  private apiUrl = environment.apiUrl;

  constructor(
    private http: HttpClient,
    private authService: AuthService
  ) {}

  // Like a post
  likePost(postId: number): Observable<any> {
    console.log('Liking post:', postId);
    return this.http.post(`${this.apiUrl}/Posts/${postId}/like`, {}, {
      headers: this.authService.getAuthHeaders()
    });
  }

  // Unlike a post
  unlikePost(postId: number): Observable<any> {
    console.log('Unliking post:', postId);
    return this.http.delete(`${this.apiUrl}/Posts/${postId}/like`, {
      headers: this.authService.getAuthHeaders()
    });
  }

  // Get likes for a post (optional - for detailed view)
  getPostLikes(postId: number): Observable<any[]> {
    return this.http.get<any[]>(`${this.apiUrl}/Posts/${postId}/likes`, {
      headers: this.authService.getAuthHeaders()
    });
  }
}
