export interface Post {
  postId: number;
  userId: number;
  content: string;
  username: string;
  postImage?: string;
  createdAt: Date;
  updatedAt?: Date;
  likesCount: number;
  commentsCount: number;
  isLikedByCurrentUser: boolean;
  status?: string;  
   // ✅ ADD these optional properties for UI state
  isLiking?: boolean;        // For like button loading state
  showComments?: boolean;    // For comment section toggle
  
  user: {
    userId: number;
    firstName: string;
    lastName: string;
    userprofilePicture?: string;
    username: string;
    
  };
}

export interface CreatePostRequest {
  content: string;
  userProfileImage?: string;
}

export interface LikeRequest {
  postId: number;
}

