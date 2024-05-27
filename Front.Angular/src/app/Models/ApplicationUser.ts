import { ChatMessage } from "./ChatMessage";
import { Post } from "./Post";

export interface ApplicationUser {
  id: string;
  userName: string;
  email: string;
  emailConfirmed: boolean;
  phoneNumber: string;
  phoneNumberConfirmed: boolean;
  twoFactorEnabled: boolean;
  lockoutEnd: Date;
  lockoutEnabled: boolean;
  accessFailedCount: number;
  name: string;
  jwtToken: string;
  posts: Post[];
  chatMessagesSent: ChatMessage[];
  chatMessagesReceived: ChatMessage[];
}
