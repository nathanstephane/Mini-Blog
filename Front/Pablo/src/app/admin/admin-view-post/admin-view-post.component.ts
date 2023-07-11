import { Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Post } from 'src/app/models/post.model';
import { PostService } from 'src/app/services/post.service';

@Component({
  selector: 'app-admin-view-post',
  templateUrl: './admin-view-post.component.html',
  styleUrls: ['./admin-view-post.component.css']
})
export class AdminViewPostComponent {
constructor(private route: ActivatedRoute, private postService: PostService){}
post: Post | undefined;
ngOnInit(): void {
  this.route.paramMap.subscribe(
    params => {
      const id = params.get('id');

      if(id){
        this.postService.getPostById(id)
        .subscribe(
          response => {
            this.post = response;
          }
        )
      }
    }
  )
}
}
