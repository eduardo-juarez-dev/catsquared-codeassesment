import {Component} from '@angular/core';
import {Client, WeatherForecast} from "./weatherapp.swagger";

@Component({  
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss'],
  providers: [Client]
 })

export class AppComponent {
  title = 'TestUI';
  weatherData: WeatherForecast[] = [];

  constructor(
    private client: Client
  ) {
    this.getWeather();
  }

  public ngOnInit(){
    this.getWeather();
  }

  getClient(){
    return this.client;
  }

  /**
   * Get Current Weather
   *
   * @description Gets current weather from API
   */
  getWeather() {
    this.client.unauthenticated().subscribe({
      complete: () => {},
      error: (error: any) => {
        this.handleError(error);
      },
      next: (data: WeatherForecast[]) => {
        this.weatherData = data;
      }
    })
  }

  getColor(summary: string): string{

    if((summary === 'Freezing') || (summary === 'Bracing') || (summary === 'Chilly')){
      return "cyan";
    }else if((summary === 'Mild') || (summary === 'Balmy') || (summary === 'Cool')){
      return "green";
    }else if((summary === 'Warm') || (summary === 'Hot')){
      return "orange";
    }else if((summary === 'Sweltering') || (summary === 'Scorching')){
      return "red";
    }         
    
    return "black";
  }

  /**
   * Dummy Error Handler
   *
   * @description throws error state
   * @param error
   */
  handleError(error: string) {
    alert(error);
  }
}
