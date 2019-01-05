import { Component } from '@angular/core';

@Component({
  selector: 'app-juego-component',
  templateUrl: './juego.component.html'
})
export class JuegoComponent {
  public currentCount = 0;

  public filas = [1, 2, 3, 4];

  public incrementCounter() {
    this.currentCount++;
  }
}
