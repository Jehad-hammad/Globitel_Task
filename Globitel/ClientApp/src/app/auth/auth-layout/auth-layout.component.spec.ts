/// <reference path="../../../../../node_modules/@types/jasmine/index.d.ts" />
import { TestBed, async, ComponentFixture, ComponentFixtureAutoDetect } from '@angular/core/testing';
import { BrowserModule, By } from "@angular/platform-browser";
import { AuthLayoutComponent } from './auth-layout.component';

let component: AuthLayoutComponent;
let fixture: ComponentFixture<AuthLayoutComponent>;

describe('authLayout component', () => {
    beforeEach(async(() => {
        TestBed.configureTestingModule({
            declarations: [ AuthLayoutComponent ],
            imports: [ BrowserModule ],
            providers: [
                { provide: ComponentFixtureAutoDetect, useValue: true }
            ]
        });
        fixture = TestBed.createComponent(AuthLayoutComponent);
        component = fixture.componentInstance;
    }));

    it('should do something', async(() => {
        expect(true).toEqual(true);
    }));
});