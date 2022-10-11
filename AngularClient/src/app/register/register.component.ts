import { Component, OnInit } from '@angular/core';
import { ConfigService } from '../config.service';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Register } from '../register';
import { HttpHeaders } from '@angular/common/http';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
  postId: any;
  register!: Register;
  registerForm!: FormGroup;
  fullnameTxt!:String;
  usernameTxt!:String;
  passwordTxt!:String;
  isadminChk!:boolean;
  //constructor(private httpClient: HttpClient) { }

  
  constructor(private configService: ConfigService, private formBuilder: FormBuilder) {}

  ngOnInit() {
    this.registerForm = this.formBuilder.group({
      fullnameTxt: ['', Validators.required],
      usernameTxt: ['', Validators.required],
      isadminTxt: [''],
      passwordTxt: ['', [Validators.required, Validators.minLength(6)]]
  });

    //this.httpClient.post<Register>('https://localhost:44341/Student', body, { headers }).subscribe(data => {
    //    this.postId = data.id;
   
  }

onSubmit() {
/*
  this.register.FullName = 'asd3333';
  this.register.Username = 'asd3ssssSAD';
  this.register.Password = 'aASDass33';
  this.register.IsAdmin = false;
  */
 const reg : Register = {FullName : this.fullnameTxt , IsAdmin: this.isadminChk , Password: this.passwordTxt, Username:this.usernameTxt};
//console.log(reg);

let headers = new HttpHeaders()
    .set('Authorization', 'Bearer ' + localStorage.getItem('token'))
    .set('Content-Type', 'application/json')
    .set('InstanceName', 'ORISSA');
    
//const headers = { 'content-type': 'application/json'} 
  this.configService.postRegister(reg,headers).subscribe((data)=>{
    //console.log(data);

  });
}
}
