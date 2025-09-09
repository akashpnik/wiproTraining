import { Component, Input, Output, EventEmitter, OnInit } from '@angular/core';
import { CommentService } from '../../services/comment.service';
import { AuthService } from '../../services/auth.service';
import { Comment, CreateCommentRequest } from '../../models/comment.model';

@Component({
  selector: 'app-comment',
  templateUrl: './comment.component.html',
  styleUrls: ['./comment.component.css'],
  standalone: false
})
export class CommentComponent implements OnInit {
  @Input() postId!: number;
  @Input() initialCommentsCount: number = 0;
  @Output() commentsCountChanged = new EventEmitter<number>();

  comments: Comment[] = [];
  newComment: string = '';
  isLoading = false;
  isSubmitting = false;
  errorMessage = '';
  currentUser: any;

  constructor(
    private commentService: CommentService,
    private authService: AuthService
  ) {}

  ngOnInit() {
    this.currentUser = this.authService.getCurrentUser();
    this.loadComments();
  }

  loadComments() {
    this.isLoading = true;
    this.errorMessage = '';

    this.commentService.getComments(this.postId).subscribe({
      next: (comments) => {
        this.comments = comments;
        this.isLoading = false;
        console.log(`✅ Loaded ${comments.length} comments for post ${this.postId}`);
        
        // Emit updated comments count
        this.commentsCountChanged.emit(comments.length);
      },
      error: (error) => {
        console.error('❌ Error loading comments:', error);
        this.errorMessage = 'Failed to load comments';
        this.isLoading = false;
      }
    });
  }

  addComment() {
    if (!this.currentUser) {
      alert('Please login to comment');
      return;
    }

    if (!this.newComment.trim()) {
      alert('Please enter a comment');
      return;
    }

    this.isSubmitting = true;
    this.errorMessage = '';

    const commentData: CreateCommentRequest = {
      postId: this.postId,
      commentText: this.newComment.trim()
    };

    this.commentService.addComment(commentData).subscribe({
      next: (newComment) => {
        console.log('✅ Comment added successfully:', newComment);
        
        // Add new comment to the beginning of the list
        this.comments.unshift(newComment);
        
        // Clear the input
        this.newComment = '';
        
        // Update comments count
        this.commentsCountChanged.emit(this.comments.length);
        
        this.isSubmitting = false;
      },
      error: (error) => {
        console.error('❌ Error adding comment:', error);
        this.errorMessage = 'Failed to add comment. Please try again.';
        this.isSubmitting = false;
      }
    });
  }

  onKeyPress(event: KeyboardEvent) {
    if (event.key === 'Enter' && !event.shiftKey) {
      event.preventDefault();
      this.addComment();
    }
  }
}
