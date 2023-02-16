import { Component, OnInit } from '@angular/core';
import { HttpErrorResponse } from '@angular/common/http';
import { FormBuilder, Validators, FormGroup, FormControl } from '@angular/forms';
import { AreaService } from '../services/area.service';
import { UserService } from '../services/user.service';
import Country from '../models/country';
import Province from '../models/province';
import { RegisterSteps } from '../models/register-steps';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html'
})
export class HomeComponent implements OnInit {
  currentStep: RegisterSteps = RegisterSteps.UserInfo;

  userStepFormGroup: FormGroup;
  areaStepFormGroup: FormGroup;
  countries: Country[] = [];
  provinces: Province[] = [];
  hasError = false;
  isSuccess: boolean | null;
  errors: string[];
  selectedProvinceId: number | null;

  get email() { return this.userStepFormGroup.get('email'); }
  get password() { return this.userStepFormGroup.get('password'); }
  get confirmPassword() { return this.userStepFormGroup.get('confirmPassword'); }
  get isAgree() { return this.userStepFormGroup.get('isAgree'); }
  get countryId() { return this.areaStepFormGroup.get('countryId'); }
  get provinceId() { return this.areaStepFormGroup.get('provinceId'); }

  constructor(private _formBuilder: FormBuilder, private _areaService: AreaService, private _userService: UserService) { }

  ngOnInit(): void {

    this.initForms();

    this.countryId?.valueChanges.subscribe(id => {
      this.areaStepFormGroup.get('provinceId')?.setValue(null);
      this.getProvinces(id);
    });

    this.getCountries();
  }

  isInvalid(control: FormControl | null) {
    return control?.dirty || control?.touched || this.hasError;
  }

  initForms() {
    this.userStepFormGroup = this._formBuilder.group({
      email: ['', Validators.compose([Validators.required, Validators.email])],
      password: ['', Validators.required],
      confirmPassword: ['', Validators.required],
      isAgree: ['', Validators.requiredTrue],
    },
      {
        validators: [this.strongValidator('password'), this.confirmedValidator('password', 'confirmPassword')],
      });

    this.areaStepFormGroup = this._formBuilder.group({
      countryId: ['', Validators.required],
      provinceId: ['', Validators.required],
    });
  }

  getCountries() {
    this._areaService.getCountries()
      .subscribe(res => {
        this.countries = res;
      }, error => {
        console.log(error);
      })
  }

  getProvinces(countryId: string) {
    this._areaService.getProvinces(countryId)
      .subscribe(res => {
        this.provinces = res;
      }, error => {
        console.log(error);
      })
  }

  confirmedValidator(controlName: string, matchingControlName: string) {
    return (formGroup: FormGroup) => {
      const control = formGroup.controls[controlName];
      const matchingControl = formGroup.controls[matchingControlName];
      if (
        matchingControl.errors &&
        !matchingControl.errors.confirmedValidator
      ) {
        return;
      }
      if (control.value !== matchingControl.value) {
        matchingControl.setErrors({ confirmedValidator: true });
      } else {
        matchingControl.setErrors(null);
      }
    };
  }

  strongValidator(controlName: string) {
    return (formGroup: FormGroup) => {
      const control = formGroup.controls[controlName];

      let hasNumber = /\d/.test(control.value);
      let hasUpper = /[A-Z]/.test(control.value);
      const valid = hasNumber && hasUpper;
      if (!valid) {
        control.setErrors({ strongValidator: true });
      }
      return;
    };
  }

  validateUserForm() {
    this.email?.updateValueAndValidity();
    this.password?.updateValueAndValidity();
    this.confirmPassword?.updateValueAndValidity();
    this.isAgree?.updateValueAndValidity();

    if (this.email?.errors || this.password?.errors || this.confirmPassword?.errors || this.isAgree?.errors) {
      this.hasError = true;
      return;
    }

    this.hasError = false;
    this.currentStep = RegisterSteps.AreInfo;
  }

  save() {
    if (this.countryId?.errors || this.provinceId?.errors) {
      this.hasError = true;
      return;
    }

    this._userService.tryRegisterUser({
      email: this.email?.value,
      password: this.password?.value,
      isAgree: this.isAgree?.value,
      countryId: this.countryId?.value,
      provinceId: this.provinceId?.value,
    })
      .subscribe({
        next: (_) => this.currentStep = RegisterSteps.Completed,
        error: (e: HttpErrorResponse) => {
          if (e.status === 400) {
            this.errors = e.error.errors.map((x: { errorMessage: any; }) => x.errorMessage);
          }
          this.currentStep = RegisterSteps.Error
          console.error(e);
        },
      });
  }
}
