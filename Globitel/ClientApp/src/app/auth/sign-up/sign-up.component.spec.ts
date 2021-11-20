/// <reference path="../../../../../node_modules/@types/jasmine/index.d.ts" />
import { TestBed, async, ComponentFixture, ComponentFixtureAutoDetect } from '@angular/core/testing';
import { BrowserModule, By } from "@angular/platform-browser";
import { SignUpComponent } from './sign-up.component';

let component: SignUpComponent;
let fixture: ComponentFixture<SignUpComponent>;

describe('signUp component', () => {
    beforeEach(async(() => {
        TestBed.configureTestingModule({
            declarations: [ SignUpComponent ],
            imports: [ BrowserModule ],
            providers: [
                { provide: ComponentFixtureAutoDetect, useValue: true }
            ]
        });
        fixture = TestBed.createComponent(SignUpComponent);
        component = fixture.componentInstance;
    }));

    it('should do something', async(() => {
        expect(true).toEqual(true);
    }));
});