import { Component, OnInit } from '@angular/core';
import { AdminService } from '../../services/admin.service';
import { AuthService } from '../../services/auth.service';
import { Post } from '../../models/post.model';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../../environments/environment';

@Component({
  selector: 'app-admin-dashboard',
  templateUrl: './admin-dashboard.component.html',
  styleUrls: ['./admin-dashboard.component.css'],
  standalone: false
})
export class AdminDashboardComponent implements OnInit {
  pendingPosts: Post[] = [];
  allPosts: Post[] = [];
  activeTab = 'pending';
  isLoading = false;
  errorMessage = '';
  currentUser: any;

  constructor(
    private adminService: AdminService,
    private authService: AuthService
  ) {}

  ngOnInit() {
    this.currentUser = this.authService.getCurrentUser();
    
    // Check if user is admin
    if (!this.authService.isAdmin()) {
      this.errorMessage = 'Access denied. Admin privileges required.';
      return;
    }

    this.loadPendingPosts();
  }

  setActiveTab(tab: string) {
    this.activeTab = tab;
    if (tab === 'pending') {
      this.loadPendingPosts();
    } else if (tab === 'all') {
      this.loadAllPosts();
    }
  }

  loadPendingPosts() {
    this.isLoading = true;
    this.errorMessage = '';

    this.adminService.getPendingPosts().subscribe({
      next: (posts) => {
        this.pendingPosts = posts;
        console.log('Pending posts loaded:', posts);
        this.isLoading = false;
      },
      error: (error) => {
        console.error('Error loading pending posts:', error);
        this.errorMessage = 'Failed to load pending posts';
        this.isLoading = false;
      }
    });
  }

  loadAllPosts() {
    this.isLoading = true;
    this.errorMessage = '';

    this.adminService.getAllPostsForAdmin().subscribe({
      next: (posts) => {
        this.allPosts = posts;
        console.log('All posts loaded:', posts);
        this.isLoading = false;
      },
      error: (error) => {
        console.error('Error loading all posts:', error);
        this.errorMessage = 'Failed to load posts';
        this.isLoading = false;
      }
    });
  }


   /* approvePost(postId: number) {
  if (confirm('Are you sure you want to approve this post?')) {
    console.log('Attempting to approve post:', postId); // Debug log
    
    this.adminService.approvePost(postId).subscribe({
      next: (response) => {
        console.log('✅ Post approved successfully:', response);
        
        // ✅ OPTION 1: Remove from current list (immediate UI update)
        this.pendingPosts = this.pendingPosts.filter(p => p.postId !== postId);
        
        // ✅ OPTION 2: Reload entire list from server (more reliable)
         //this.loadPendingPosts();
        
        alert('Post approved successfully!');
      },
      error: (error) => {
        console.error('❌ Error approving post:', error);
        
        let errorMessage = 'Failed to approve post'; 
        if (error.status === 401) {
          errorMessage += ': Unauthorized. Please login again.';
        } else if (error.status === 403) {
          errorMessage += ': Access denied. Admin privileges required.';
        } else if (error.status === 404) {
          errorMessage += ': API endpoint not found.';
        } else if (error.error?.message) {
          errorMessage += ': ' + error.error.message;
        }
        
         alert(errorMessage);
      }
    });
  } 
 } */
 approvePost(postId: number) {
  if (confirm('Are you sure you want to approve this post?')) {
    console.log('Attempting to approve post:', postId);
    
    this.adminService.approvePost(postId).subscribe({
      next: (response) => {
        console.log('✅ Post approved successfully:', response);
        
        // Option 1: Remove from current list (immediate UI update)
        this.pendingPosts = this.pendingPosts.filter(p => p.postId !== postId);
        
        // Option 2: Reload entire list from server (more reliable)
        // this.loadPendingPosts();
        
        alert('Post approved successfully!');
      },
      error: (error) => {
        console.error('❌ Error approving post:', error);
        
        let errorMessage = 'Failed to approve post';
        if (error.status === 401) {
          errorMessage += ': Unauthorized. Please login again.';
        } else if (error.status === 403) {
          errorMessage += ': Access denied. Admin privileges required.';
        } else if (error.status === 404) {
          errorMessage += ': API endpoint not found.';
        } else if (error.error?.message) {
          errorMessage += ': ' + error.error.message;
        }
        
        alert(errorMessage);
      }
    });
  }
 }
 

  rejectPost(postId: number) {
    if (confirm('Are you sure you want to reject this post?')) {
      this.adminService.rejectPost(postId).subscribe({
        next: () => {
          console.log('Post rejected successfully');
          // Remove from pending list
          this.pendingPosts = this.pendingPosts.filter(p => p.postId !== postId);
          
          // Show success message
          alert('Post rejected successfully!');
        },
        error: (error) => {
          console.error('Error rejecting post:', error);
          alert('Failed to reject post');
        }
      });
    }
  }



}
