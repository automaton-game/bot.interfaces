import { Component, Inject, OnInit } from '@angular/core';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { JuegoManualResponse } from './modelos/juegoManualResponse';
import { AccionRobot } from './modelos/accionRobot';

@Component({
  selector: 'app-juegoManual-component',
  templateUrl: './juegoManual.component.html',
})
export class JuegoManualComponent implements OnInit {

  public juegoManualResponse: JuegoManualResponse;

  public ganador: string;
  public errores: string[];
  public idTablero: string;
  public idJugador: string;

  constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) {
  }

  crearTablero() {
    this.http.get<JuegoManualResponse>(this.baseUrl + 'api/Tablero/CrearTablero', {} )
      .subscribe(result => {
        this.juegoManualResponse = result;
        this.idTablero = result.idTablero;
        this.idJugador = result.jugadores[0];
      }, (err: HttpErrorResponse) => this.errores = err.error.errors.map(m => m.message));
  }

  obtenerTablero() {
    this.http.get<JuegoManualResponse>(this.baseUrl + 'api/Tablero/ObtenerTablero?idTablero=' + this.idTablero)
      .subscribe(result => {
        this.juegoManualResponse = result;
        this.idJugador = result.jugadores[0];
      }, (err: HttpErrorResponse) => this.errores = err.error.errors.map(m => m.message));
  }
  
  accionarTablero(accionRobot: AccionRobot) {
    this.http.post<JuegoManualResponse>(this.baseUrl + 'api/Tablero/AccionarTablero', { idTablero: this.idTablero, idJugador: this.idJugador, accionRobot: accionRobot  })
      .subscribe(result => {
        this.juegoManualResponse = result;
      }, (err: HttpErrorResponse) => this.errores = err.error.errors.map(m => m.message));
  }

  ngOnInit(): void {
  }
}
