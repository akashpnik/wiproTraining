import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';
import { AuthService } from './auth.service';
import { UserProfile, UpdateProfileRequest } from '../models/user-profile.model';
import { Post } from '../models/post.model';

@Injectable({
  providedIn: 'root'
})
export class UserProfileService {
  private apiUrl = environment.apiUrl;

  constructor(
    private http: HttpClient,
    private authService: AuthService
  ) {}

  // Get user profile by ID
  getUserProfile(userId: number): Observable<UserProfile> {
    return this.http.get<UserProfile>(`${this.apiUrl}/Users/${userId}`, {
      headers: this.authService.getAuthHeaders()
    });
  }

  // Update user profile
  updateProfile(userId: number, profileData: UpdateProfileRequest): Observable<UserProfile> {
    return this.http.put<UserProfile>(`${this.apiUrl}/Users/${userId}`, profileData, {
      headers: this.authService.getAuthHeaders()
    });
  }

  // Get posts by user
  getUserPosts(userId: number): Observable<Post[]> {
    return this.http.get<Post[]>(`${this.apiUrl}/Users/${userId}/posts`, {
      headers: this.authService.getAuthHeaders()
    });
  }

  // Upload profile picture (if you have file upload endpoint)
  uploadProfilePicture(userId: number, file: File): Observable<any> {
    const formData = new FormData();
    formData.append('profilePicture', file);
    
    return this.http.post(`${this.apiUrl}/Users/${userId}/upload-picture`, formData, {
      headers: this.authService.getAuthHeaders()
    });
  }
}
