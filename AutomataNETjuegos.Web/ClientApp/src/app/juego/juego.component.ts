import { Component, Inject, OnInit } from '@angular/core';
import { Tablero } from './modelos/tablero';
import { FilaTablero } from './modelos/filaTablero';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-juego-component',
  templateUrl: './juego.component.html',  
})
export class JuegoComponent implements OnInit {
  
  public filas: Array<FilaTablero>;

  ngOnInit(): void {
    this.http.get<Array<Tablero>>(this.baseUrl + 'api/Tablero/GetTablero').subscribe(result => {
      this.filas = result[result.length-1].filas;
    }, error => console.error(error));
  }

  constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) {
    
  }
}
