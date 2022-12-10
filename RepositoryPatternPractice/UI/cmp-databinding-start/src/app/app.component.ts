import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { map } from 'rxjs/operators';
import {DomSanitizer, SafeHtml}  from "@angular/platform-browser";
@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {


  

  loadedPosts = [];
  alphas:any; 
  htmlview: SafeHtml | undefined;
  serverElements = [{type:'server',name:'TestServer',content:'Just Test!'}];


  constructor(private http: HttpClient,private sanitizer:DomSanitizer) {}

  ngOnInit() {
    this.fetchPosts();
  }


  // onCreatePost(postData: { title: string; content: string }) {
  //   // Send Http request
  //   this.http
  //     .post(
  //       'https://localhost:44396/api/ProductAPI/SendFormModule',
  //       postData
  //     )
  //     .subscribe(responseData => {
  //       console.log(responseData);
  //     });
  // }

  // onFetchPosts() {
  //   // Send Http request
  //   this.fetchPosts();
  // }

  private fetchPosts() {
    this.http
      .get('https://localhost:44396/api/ProductAPI/SendFormModule')
      .pipe(
        map(responseData => {
          
          this.alphas=responseData as string
          this.htmlview=this.sanitizer.bypassSecurityTrustHtml(this.alphas);
          //console.log(this.alphas)
          
        })
      )
      .subscribe(posts => {
        // ...
        console.log("Hello");
      });
  }



  
  onServerAdded(server: {serverName: string, serverContent: string}) {
    this.serverElements.push({
      type: 'server',
      name: server.serverName,
      content: server.serverContent
    });
  }

  onBlueprintAdded(blueprint: {serverName: string, serverContent: string}) {
    this.serverElements.push({
      type: 'blueprint',
      name: blueprint.serverName,
      content: blueprint.serverContent
    });
  }
  
}
