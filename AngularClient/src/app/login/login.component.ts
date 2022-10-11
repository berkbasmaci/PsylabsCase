import { Component, OnInit } from '@angular/core';
import { ConfigService } from '../config.service';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { LoginRequest } from '../loginrequest';
import { LoginResponse } from '../login-response';
import { HttpHeaders } from '@angular/common/http';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  loginForm!: FormGroup;
usernameTxt: any;
passwordTxt: any;
loginResponse!: LoginResponse;

  constructor(private configService: ConfigService, private formBuilder: FormBuilder) {}

  ngOnInit() {
    this.loginForm = this.formBuilder.group({
      usernameTxt: ['', Validators.required],
      passwordTxt: ['', [Validators.required]]
  });
}

onSubmit() {
  /*
    this.register.FullName = 'asd3333';
    this.register.Username = 'asd3ssssSAD';
    this.register.Password = 'aASDass33';
    this.register.IsAdmin = false;
    */
   const login : LoginRequest = {Username: this.usernameTxt, Pwd:this.passwordTxt};
  //console.log(reg);

    const headers = { 'content-type': 'application/json'} 
    this.configService.postLogin(login,headers).subscribe((data)=>{
      //console.log(data);
      localStorage.setItem ('token', data.token);
    });
  }
  }
  