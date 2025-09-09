import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable, BehaviorSubject } from 'rxjs';
import { tap } from 'rxjs/operators';
import { environment } from '../../environments/environment';

export interface LoginRequest {
  username: string;  // matching your backend
  password: string;
}

export interface RegisterRequest {
  firstName: string;
  lastName: string;
  email: string;
  password: string;
}

export interface AuthResponse {
  token: string;
  user: {
    id: number;
    firstName: string;
    lastName: string;
    email: string;
    role: string;
  };
}

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private apiUrl = environment.apiUrl;
  private tokenKey = 'profilebook_token';
  private userKey = 'profilebook_user';
  
  private currentUserSubject = new BehaviorSubject<any>(this.getCurrentUser());
  public currentUser$ = this.currentUserSubject.asObservable();

  private httpOptions = {
    headers: new HttpHeaders({
      'Content-Type': 'application/json'
    })
  };

  constructor(private http: HttpClient) {}

  // Make sure your login method calls the right endpoint:
/*login(credentials: LoginRequest): Observable<AuthResponse> {
  console.log('Calling backend login with:', credentials);
  return this.http.post<AuthResponse>(`${this.apiUrl}/Auth/login`, credentials, this.httpOptions)
    .pipe(
      tap(response => {
        console.log('Backend login response:', response);
        this.setToken(response.token);
        this.setCurrentUser(response.user);
      })
    );
}*/

// ✅ ENSURE this method exists and returns Observable
  register(registerData: any): Observable<any> {
    console.log('🔍 Registering user:', registerData);
    
    return this.http.post(`${this.apiUrl}/Auth/register`, registerData, {
      headers: new HttpHeaders({
        'Content-Type': 'application/json'
      })
    });
  }

  logout(): void {
    localStorage.removeItem(this.tokenKey);
    localStorage.removeItem(this.userKey);
    this.currentUserSubject.next(null);
  }

  getToken(): string | null {
    return localStorage.getItem(this.tokenKey);
  }

  private setToken(token: string): void {
    localStorage.setItem(this.tokenKey, token);
  }

  // FIXED: Safe JSON parsing with error handling
  getCurrentUser(): any {
    const userStr = localStorage.getItem(this.userKey);
    console.log('Getting user from localStorage:', userStr);
    if (userStr && userStr !== 'undefined ' && userStr !== 'null') {
      try {
        const user = JSON.parse(userStr);
      console.log('Parsed user:', user); // Debug log
      return user;
        //return JSON.parse(userStr);
      } catch (error) {
        console.error('Error parsing user data:', error);
        localStorage.removeItem(this.userKey); // Clear corrupted data
        return null;
      }
    }
    return null;
  }

  private setCurrentUser(user: any): void {
    console.log('Setting user in localStorage:', user);
    localStorage.setItem(this.userKey, JSON.stringify(user));
    this.currentUserSubject.next(user);
  }

  isAuthenticated(): boolean {
    return !!this.getToken() && !!this.getCurrentUser();
  }

  isAdmin(): boolean {
    const user = this.getCurrentUser();
    return user && user.role === 'Admin';
  }

  /*getAuthHeaders(): HttpHeaders {
    const token = this.getToken();
    return new HttpHeaders({
      //'Content-Type': 'application/json',
      'Authorization': token ? `Bearer ${token}` : '',
      'Content-Type': 'application/json' // Ensure Content-Type is set
    });
  }*/
getAuthHeaders(): HttpHeaders {
  const token = this.getToken();
  return new HttpHeaders({
    'Authorization': token ? `Bearer ${token}` : '',
    'Content-Type': 'application/json'
  });
}



  // Add this method to your AuthService
private extractUserFromToken(token: string): any {
  try {
    // Decode JWT token payload (middle part)
    const payload = token.split('.')[1];
    const decodedPayload = JSON.parse(atob(payload));
    
    // Extract user info from token claims
    return {
      id: decodedPayload.nameid || decodedPayload.sub,
      firstName: decodedPayload.given_name || decodedPayload.unique_name || 'User',
      lastName: decodedPayload.family_name || '',
      email: decodedPayload.email,
      role: decodedPayload.role,
      username: decodedPayload.unique_name
    };
  } catch (error) {
    console.error('Error decoding JWT token:', error);
    return null;
  }
}

// Update your login method:
login(credentials: LoginRequest): Observable<AuthResponse> {
  console.log('Calling backend login with:', credentials);
  return this.http.post<any>(`${this.apiUrl}/Auth/login`, credentials, this.httpOptions)
    .pipe(
      tap(response => {
        console.log('Backend login response:', response);
        
        if (response && response.token) {
          console.log('Valid response with token, extracting user info');
          this.setToken(response.token);
          
          // Extract user info from JWT token
          const userFromToken = this.extractUserFromToken(response.token);
          if (userFromToken) {
            console.log('Extracted user from token:', userFromToken);
            this.setCurrentUser(userFromToken);
          }
        } else {
          console.error('No token in response:', response);
          throw new Error('No token received from backend');
        }
      })
    );
}

}
