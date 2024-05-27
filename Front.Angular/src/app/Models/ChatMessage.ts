import { ApplicationUser } from "./ApplicationUser";
import { Chat } from "./Chat";

export interface ChatMessage {
  id: string;
  content: string;
  sentAt: Date;
  senderId: string;
  sender: ApplicationUser;
  receiverId: string;
  chatId: string;
  chat: Chat;
  receiver: ApplicationUser;
}
