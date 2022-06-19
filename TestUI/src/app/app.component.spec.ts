import { TestBed, fakeAsync, tick, inject } from '@angular/core/testing';
import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';
import { RouterTestingModule } from '@angular/router/testing';
import { AppComponent } from './app.component';
import { Client, WeatherForecast } from './weatherapp.swagger';
import { of } from 'rxjs';

const mockWeatherData: WeatherForecast[] = [{
  date: new Date('December 17, 1995 03:24:00'),
  temperatureC: 0,
  summary: "Freezing"
},{
  date: new Date('December 17, 1995 03:24:00'),
  temperatureC: 0,
  summary: "Freezing"  
},{
  date: new Date('December 17, 1995 03:24:00'),
  temperatureC: 0,
  summary: "Freezing"  
},{
  date: new Date('December 17, 1995 03:24:00'),
  temperatureC: 0,
  summary: "Freezing"  
},{
  date: new Date('December 17, 1995 03:24:00'),
  temperatureC: 0,
  summary: "Freezing"  
}];

describe('AppComponent', () => {

  beforeEach(async () => {

    await TestBed.configureTestingModule({
      imports: [
        HttpClientTestingModule,
        RouterTestingModule
      ],
      providers: 
        [Client],
      declarations: [
        AppComponent
      ],
    }).compileComponents();
  }); 

  it('should create the app', () => {
    const fixture = TestBed.createComponent(AppComponent);
    const app = fixture.componentInstance;
    expect(app).toBeTruthy();
  });
 
  it(`should have as title 'TestUI'`, () => {
    const fixture = TestBed.createComponent(AppComponent);
    const app = fixture.componentInstance;
    expect(app.title).toEqual('TestUI'); 
  });   
 
  it('should render title', () => {
    const fixture = TestBed.createComponent(AppComponent);
    fixture.detectChanges();
    const compiled = fixture.nativeElement as HTMLElement;
    expect(compiled.querySelector('span')!.textContent).toContain('TestUI app is running!');
  });     

  //tests getColor()
  it('should return a valid color string when getting color for description', () => {
    const fixture = TestBed.createComponent(AppComponent);
    const app = fixture.componentInstance;
    // possible inputs from api
    const dummyInputData = ['Freezing', 'Bracing', 'Chilly', 
                            'Mild', 'Balmy', 'Cool', 
                            'Warm', 'Hot', 
                            'Sweltering', 'Scorching'];
    // possible results that method should return  
    const dummyResults = ['cyan', 'green', 'orange', 'red'];
    const randomValue = Math.floor(Math.random() * dummyInputData.length);
    const result = app.getColor(dummyInputData[randomValue]);    
    expect(dummyResults).toContain(result);
  });  

  // it('tests if client service was called by weatherservice', fakeAsync(() => {
  //   const fixture = TestBed.createComponent(AppComponent); 
  //   const app = fixture.componentInstance; 
  //   const clientService = TestBed.inject(Client);
  //   fixture.detectChanges();
  //   const clientSpy = spyOn(clientService,'unauthenticated');
  //   app.ngOnInit();
  //   tick(1);
  //   expect(clientSpy).toHaveBeenCalled(); 
  // })); 

});
