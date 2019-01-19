import { Component, Inject, OnInit } from '@angular/core';
import { Tablero } from './modelos/tablero';
import { FilaTablero } from './modelos/filaTablero';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-juego-component',
  templateUrl: './juego.component.html',  
})
export class JuegoComponent implements OnInit {

  private tableros: Tablero[];

  public filas: Array<FilaTablero>;
  public max: number;
  public actual: number;

  constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) {
    
  }

  actualizarTablero() {
    this.filas = this.tableros[this.actual].filas;
  }

  ngOnInit(): void {
    this.http.get<Array<Tablero>>(this.baseUrl + 'api/Tablero/GetTablero').subscribe(result => {
      this.max = result.length - 1;
      this.actual = this.max;

      this.tableros = result;
      this.actualizarTablero();
    }, error => console.error(error));
  }

}
