import { Component, EventEmitter, Output } from '@angular/core';
import { PostService } from '../../services/post.service';
import { AuthService } from '../../services/auth.service';
import { CreatePostRequest } from '../../models/post.model';

@Component({
  selector: 'app-create-post',
  templateUrl: './create-post.component.html',
  styleUrls: ['./create-post.component.css'],
  standalone: false
})
export class CreatePostComponent {
  @Output() postCreated = new EventEmitter<void>();

  newPost: CreatePostRequest = {
    content: '',
     userProfileImage: '' // Keep this for backward compatibility, but we'll use file upload instead
  };

  selectedFile: File | null = null; // Add file handling
  isCreatingPost = false;
  errorMessage = '';
  successMessage = '';
  currentUser: any;

  constructor(
    private postService: PostService,
    private authService: AuthService
  ) {
    this.currentUser = this.authService.getCurrentUser();
  }

  // Handle file selection
  onFileSelected(event: any): void {
    const file = event.target.files[0];
    if (file) {
      // Validate file type (optional)
      if (file.type.startsWith('image/')) {
        this.selectedFile = file;
        console.log('Image selected:', file.name);
      } else {
        this.errorMessage = 'Please select an image file';
        this.selectedFile = null;
      }
    }
  }

  onSubmit(): void {
    if (!this.isValidForm()) {
      this.errorMessage = 'Please enter some content for your post';
      return;
    }

    this.isCreatingPost = true;
    this.errorMessage = '';
    this.successMessage = '';

    console.log('Submitting post with file:', this.newPost, this.selectedFile);

    // Pass both post data and file to service
    this.postService.createPost(this.newPost, this.selectedFile || undefined).subscribe({
      next: (createdPost) => {
        console.log('✅ Post created successfully:', createdPost);
        this.successMessage = 'Post created successfully!';
        this.resetForm();
        this.postCreated.emit();
        this.isCreatingPost = false;

        setTimeout(() => {
          this.successMessage = '';
        }, 3000);
      },
      error: (error) => {
        console.error('❌ Error creating post:', error);
        this.errorMessage = error.error?.message || 'Failed to create post. Please try again.';
        this.isCreatingPost = false;
      }
    });
  }

  isValidForm(): boolean {
    return this.newPost.content.trim().length > 0;
  }

  resetForm(): void {
    this.newPost = {
      content: '',
      userProfileImage: ''
    };
    this.selectedFile = null;
    
    // Reset file input
    const fileInput = document.getElementById('imageFile') as HTMLInputElement;
    if (fileInput) {
      fileInput.value = '';
    }
  }

  clearMessages(): void {
    this.errorMessage = '';
    this.successMessage = '';
  }
}
