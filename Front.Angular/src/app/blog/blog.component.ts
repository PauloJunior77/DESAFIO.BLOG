import { Component, OnInit } from '@angular/core';
import { Post } from '../Models/Post';
import { PostsService } from '../Services/posts.service';
import { AuthService } from '../Services/auth.service';

@Component({
  selector: 'app-blog',
  templateUrl: './blog.component.html',
  styleUrls: ['./blog.component.css']
})
export class BlogComponent implements OnInit {
  posts: Post[] = [];
  isAdmin: boolean = false;
  currentUser: any;

  constructor(
    private postService: PostsService,
    private authService: AuthService
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
}

