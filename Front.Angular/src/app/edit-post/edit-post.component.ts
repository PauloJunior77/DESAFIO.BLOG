import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { PostsService } from '../Services/posts.service';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ApplicationUser } from '../Models/ApplicationUser';
import { jwtDecode } from 'jwt-decode'; // Importando jwt_decode corretamente
import { AuthService } from '../Services/auth.service';

@Component({
  selector: 'app-edit-post',
  templateUrl: './edit-post.component.html',
  styleUrls: ['./edit-post.component.css']
})
export class EditPostComponent implements OnInit {
  postId: string = '';
  postForm!: FormGroup;
  userId: string = '';

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private postsService: PostsService,
    private formBuilder: FormBuilder,
    private authService: AuthService
  ) { }

  ngOnInit(): void {
    this.postId = this.route.snapshot.paramMap.get('id') || '';
    this.createForm();
    this.loadPost();
    this.getUserIdFromToken();

    if (!this.authService.isAuthenticated()) {
      this.router.navigate(['/login']);
    }
  }

  createForm(): void {
    this.postForm = this.formBuilder.group({
      title: ['', Validators.required],
      content: ['', Validators.required]
    });
  }

  loadPost(): void {
    this.postsService.getPostById(this.postId).subscribe(response => {
      if (response) {
        this.postForm.patchValue({
          title: response.title,
          content: response.content
        });
      } else {
        console.error('O post não foi encontrado!');
      }
    });
  }

  getUserIdFromToken(): void {
    const token = localStorage.getItem('token');
    if (token) {
      const decodedToken: any = jwtDecode(token);
      this.userId = decodedToken.nameid;
    }
  }

  onSubmit(): void {
    if (this.postForm.valid) {
      const postData = {
        id: this.postId,
        title: this.postForm.value.title,
        content: this.postForm.value.content,
        userId: this.userId,
        user: {} as ApplicationUser
      };

      this.postsService.updatePost(this.postId, postData).subscribe(() => {
        this.router.navigate(['/blog']);
      }, (error) => {
        console.error('Erro ao atualizar o post:', error);
      });
    }
  }
}
