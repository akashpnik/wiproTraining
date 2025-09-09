export interface Comment {
  commentId: number;
  postId: number;
  userId: number;
  commentText: string;
  content: string;
  createdAt: Date;
  user: {
    userId: number;
    firstName: string;
    lastName: string;
    profilePicture?: string;
    username: string;
  };
}

export interface CreateCommentRequest {
  postId: number;
  commentText: string;
}
