import { Component, OnInit } from '@angular/core';
import { ConfigService } from '../config.service';
import { User } from '../user';
@Component({
  selector: 'app-user-list',
  templateUrl: './user-list.component.html',
  styleUrls: ['./user-list.component.css']
})
export class UserListComponent implements OnInit {

  userList!: User[];

  constructor(private configService: ConfigService) { }
  
  ngOnInit() {
    this.configService.getUser().subscribe((data)=>{
      console.log(data);
      this.userList = data;
    });
  }

}
