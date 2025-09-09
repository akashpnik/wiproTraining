export interface UserProfile {
  userId: number;
  username: string;
  firstName: string;
  lastName: string;
  email: string;
  bio?: string;
  profilePicture?: string;
  dateJoined: Date;
  postsCount: number;
  role: string;
}

export interface UpdateProfileRequest {
  firstName: string;
  lastName: string;
  bio?: string;
  profilePicture?: string;
}
