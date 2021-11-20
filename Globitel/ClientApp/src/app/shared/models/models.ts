export class loginDTO {
  userName: number;
  password: string;
}


export class RegisterDTO {
  userName: string;
  fullName: string;
  phoneNumber: string;
  emailAddress: string
  password: string
}

export class UserProfileDTO {
  userName: string;
  fullName: string;
  phoneNumber: string;
  emailAddress: string;
  age: string;
  gender: string;
  status: number;
}

export class chartDto {
  maleAgeRange: number;
  femaleAgeRange: number;

}

