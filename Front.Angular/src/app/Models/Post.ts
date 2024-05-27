import { ApplicationUser } from "./ApplicationUser";

export interface Post {
  id: string;
  title: string;
  content: string;
  createdAt: Date;
  createdBy: string;
  updatedAt?: Date;
  userId:string;
  user: ApplicationUser;
}
