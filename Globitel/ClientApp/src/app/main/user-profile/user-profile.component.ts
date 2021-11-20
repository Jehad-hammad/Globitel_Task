import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { NgxSpinnerService } from 'ngx-spinner';
import { min } from 'rxjs/operators';
import { GenderListArr } from '../../shared/enums/enums';
import { UserProfileDTO } from '../../shared/models/models';
import { AuthService } from '../../shared/services/auth.service';
import { NotificationService } from '../../shared/services/notification.service';
import { UsersService } from '../../shared/services/users.service';

@Component({
    selector: 'app-user-profile',
    templateUrl: './user-profile.component.html',
    styleUrls: ['./user-profile.component.scss']
})
/** UserProfile component*/
export class UserProfileComponent implements OnInit{
  /** UserProfile ctor */
  public isAdmin : boolean = false
  public isViewMode : boolean = false
  public userId: number = 0;
  public Name: string
  public GenderList
  userProfileForm = new FormGroup({
    age: new FormControl('', [Validators.required, Validators.min(20), Validators.max(100)]),
    gender: new FormControl('', [Validators.required ]),
    fullName: new FormControl('', [Validators.required]),
    userName: new FormControl('', [Validators.required]),
    emailAddress: new FormControl('', [Validators.required]),
    phoneNumber: new FormControl('', [Validators.required, , Validators.pattern("[0-9]{10}")]),
    address: new FormControl('', [Validators.required])
  })

   model:UserProfileDTO = new UserProfileDTO();
  constructor(
    private userService: UsersService,
    private authService: AuthService,
    private spinner: NgxSpinnerService,
    private notification: NotificationService,
    private router: Router,
    public activatedRoute: ActivatedRoute,
  ) {

  }
  ngOnInit() {
    this.isAdmin = this.authService.isAdmin
    this.GenderList = GenderListArr
    if (this.router.url.includes('/user-profile/')) {
      this.activatedRoute.params.subscribe(param => {
        this.isViewMode = true
        this.userId = param.id
        this.getUsreInfo()
      })
    } else {
      this.isViewMode = false
      this.getUserProfile()
    }

  }

  bindForm() {
    this.userProfileForm.patchValue(this.model)
    this.Name = this.userProfileForm.get('fullName').value
  }
  getUsreInfo() {
    this.userService.getUserInfo(this.userId).subscribe(res => {
      this.model = res as UserProfileDTO
      this.bindForm()
    })
  }
  getUserProfile() {
    this.userService.getLoggedInUser().subscribe(response => {
      this.model = response as UserProfileDTO
      this.bindForm()
    })
  }


  onUpdate() {
    this.spinner.show()
    this.userProfileForm.markAllAsTouched()
    const profile = this.userProfileForm.getRawValue()
    this.userService.updateProfile(profile).subscribe(res => {
      this.spinner.hide();
      this.notification.showNotification('Update Profile', 'The profile has been updated successfully', 'success')
    }, error => {
      this.spinner.hide();
      this.notification.showNotification('Update Profile', 'Something went wrong while processing your request', 'error')
    })
  }
}
