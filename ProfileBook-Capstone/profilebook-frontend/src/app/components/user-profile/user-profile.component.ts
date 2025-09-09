import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { UserProfileService } from '../../services/user-profile.service';
import { AuthService } from '../../services/auth.service';
import { UserProfile } from '../../models/user-profile.model';
import { Post } from '../../models/post.model';

@Component({
  selector: 'app-user-profile',
  templateUrl: './user-profile.component.html',
  styleUrls: ['./user-profile.component.css'],
  standalone: false
})
export class UserProfileComponent implements OnInit {
  userProfile?: UserProfile;
  userPosts: Post[] = [];
  currentUser: any;
  isOwnProfile = false;
  isLoading = false;
  errorMessage = '';
  activeTab = 'posts';

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private userProfileService: UserProfileService,
    private authService: AuthService
  ) {}

  ngOnInit() {
    this.currentUser = this.authService.getCurrentUser();
    
    // Get user ID from route parameter
    this.route.params.subscribe(params => {
      const userId = +params['id'];
      if (userId) {
        this.loadUserProfile(userId);
        this.loadUserPosts(userId);
        this.isOwnProfile = this.currentUser?.userId === userId;
      }
    });
  }

  loadUserProfile(userId: number) {
    this.isLoading = true;
    this.errorMessage = '';

    this.userProfileService.getUserProfile(userId).subscribe({
      next: (profile) => {
        this.userProfile = profile;
        this.isLoading = false;
        console.log('✅ User profile loaded:', profile);
      },
      error: (error) => {
        console.error('❌ Error loading user profile:', error);
        this.errorMessage = 'Failed to load user profile';
        this.isLoading = false;
      }
    });
  }

  loadUserPosts(userId: number) {
    this.userProfileService.getUserPosts(userId).subscribe({
      next: (posts) => {
        this.userPosts = posts;
        console.log('✅ User posts loaded:', posts.length);
      },
      error: (error) => {
        console.error('❌ Error loading user posts:', error);
      }
    });
  }

  editProfile() {
    if (this.isOwnProfile && this.userProfile) {
      this.router.navigate(['/profile', this.userProfile.userId, 'edit']);
    }
  }

  setActiveTab(tab: string) {
    this.activeTab = tab;
  }
}
