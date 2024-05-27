import { Component, OnInit } from '@angular/core';
import { Post } from '../Models/Post';
import { PostsService } from '../Services/posts.service';
import { AuthService } from '../Services/auth.service';
import { Router } from '@angular/router';
import { MatDialog } from '@angular/material/dialog';
import { DeleteConfirmationModalComponent } from '../delete-confirmation-modal/delete-confirmation-modal.component';

@Component({
  selector: 'app-post-list',
  templateUrl: './post-list.component.html',
  styleUrls: ['./post-list.component.css']
})
export class PostListComponent implements OnInit {
  posts: Post[] = [];
  isAdmin: boolean = false;
  currentUser: any;

  constructor(
    private postService: PostsService,
    private authService: AuthService,
    private router: Router,
    private dialog: MatDialog 
  ) { }

  ngOnInit(): void {
    this.loadPosts();
    this.checkAdminStatus();
  }

  loadPosts(): void {
    this.postService.getPosts().subscribe(response => {
      if (response && response['$values']) {
        this.posts = response['$values'];
      } else {
        this.posts = response;
      }
    });
  }

  checkAdminStatus(): void {
    this.currentUser = this.authService.getCurrentUser();
    if (this.currentUser && this.currentUser.isAdmin) {
      this.isAdmin = true;
    }
  }

  editPost(postId: string): void {
    this.router.navigate(['/edit-post', postId]);
  }

  deletePost(postId: string): void {

    const dialogRef = this.dialog.open(DeleteConfirmationModalComponent, {
      width: '250px',
      data: { postId }
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.postService.deletePost(postId).subscribe(() => {
          this.loadPosts();
        });
      }
    });
  }
}
