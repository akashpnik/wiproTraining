import { Component, OnInit } from '@angular/core';
import { PostService } from '../../services/post.service';
import { LikeService } from '../../services/like.service';
import { AuthService } from '../../services/auth.service';
import { Post } from '../../models/post.model';
import { CommentService } from '../../services/comment.service';
import { environment } from '../../../environments/environment';

// ✅ ADD THIS: Local interface extending Post for UI state
interface PostWithUIState extends Post {
  isLiking?: boolean;
  showComments?: boolean;
}

@Component({
  selector: 'app-posts',
  templateUrl: './posts.component.html',
  styleUrls: ['./posts.component.css'],
  standalone: false
})
export class PostsComponent implements OnInit {
  posts: Post[] = [];
  currentUser: any;
  isLoading = false;
  errorMessage = '';

  constructor(
    private postService: PostService,
    private likeService: LikeService,
    private commentService: CommentService,
    private authService: AuthService
  ) {}

  ngOnInit() {
    this.currentUser = this.authService.getCurrentUser();
    this.loadPosts();
  }

  loadPosts() {
  this.isLoading = true;
  this.errorMessage = '';
  
  console.log('Loading posts from backend...'); // Debug log

  this.postService.getAllPosts().subscribe({
    next: (posts) => {
       // ✅ Initialize additional properties for UI state
        this.posts = posts.map(post => ({
          ...post,
          isLiking: false,        // For loading state
          showComments: false     // For comment toggle
        }));
        console.log('✅ Posts received from backend:', this.posts);
        this.isLoading = false;
    },
    error: (error) => {
      console.error('❌ Error loading posts:', error); // Debug log
      this.errorMessage = 'Failed to load posts. Please try again.';
      this.isLoading = false;
    }
  });
}
  // ✅ NEW: Toggle like functionality
  toggleLike(post: Post & { isLiking?: boolean }) {
    if (!this.currentUser) {
      alert('Please login to like posts');
      return;
    }

    if (post.isLiking) return; // Prevent double-clicking

    post.isLiking = true;
    const wasLiked = post.isLikedByCurrentUser;

    // Optimistic UI update
    post.isLikedByCurrentUser = !wasLiked;
    post.likesCount = wasLiked ? post.likesCount - 1 : post.likesCount + 1;

    const likeAction = wasLiked ? 
      this.likeService.unlikePost(post.postId) : 
      this.likeService.likePost(post.postId);

    likeAction.subscribe({
      next: (response) => {
        console.log('✅ Like action successful:', response);
        post.isLiking = false;
        // The UI is already updated optimistically
      },
      error: (error) => {
        console.error('❌ Error with like action:', error);
        // Revert optimistic update on error
        post.isLikedByCurrentUser = wasLiked;
        post.likesCount = wasLiked ? post.likesCount + 1 : post.likesCount - 1;
        post.isLiking = false;
        alert('Failed to update like. Please try again.');
      }
    });
  }

  // ✅ NEW: Toggle comments (placeholder for Step 4.2)
  toggleComments(post: Post & { showComments?: boolean }) {
    post.showComments = !post.showComments;
  }
  
  onCommentsCountChanged(post: PostWithUIState, newCount: number) {
  post.commentsCount = newCount;
  console.log(`✅ Comments count updated for post ${post.postId}: ${newCount}`);
}

// ✅ ADD THIS: Handle post creation
  onPostCreated(): void {
    console.log('New post created, refreshing feed...');
    this.loadPosts(); // Refresh the posts feed
  }
  
  
 // ✅ ADD: Helper method to get full post image URL
  getPostImage(post: Post): string {
    if (post.postImage) {
      // If path starts with '/', it's a relative path from backend
      if (post.postImage.startsWith('/')) {
        return `http://localhost:5021${post.postImage}`;
      }
      // If it's already a full URL, use as-is
      return post.postImage;
    }
    return 'assets/default-post-image.png'; // Fallback image
  }

  // ✅ ADD: Helper method to get full user profile image URL
  getUserProfileImage(post: Post): string {
    const profileImage = post.postImage|| post.user?.username;
    
    if (profileImage) {
      // If path starts with '/', it's a relative path from backend
      if (profileImage.startsWith('/')) {
        return `http://localhost:5021${profileImage}`;
      }
      // If it's already a full URL, use as-is
      return profileImage;
    }
    return 'assets/default-avatar.png'; // Fallback avatar
  }

  // ✅ ADD: Handle image loading errors
  onImageError(event: any) {
    console.warn('❌ Image failed to load:', event.target.src);
    
    // Set fallback based on image class
    if (event.target.classList.contains('user-avatar')) {
      event.target.src = 'profilebook-frontend/src/assets/avatar.jpeg';
    } else if (event.target.classList.contains('post-image')) {
      event.target.src = 'profilebook-frontend/src/assets/download.png';
    }
  }
  
  

  refreshPosts() {
    this.loadPosts();
  }
}
