import { Component } from '@angular/core';

@Component({
  selector: 'juego-fila-component',
  templateUrl: './fila.component.html'
})
export class FilaComponent {
  public currentCount = 0;

  public incrementCounter() {
    this.currentCount++;
  }
}
