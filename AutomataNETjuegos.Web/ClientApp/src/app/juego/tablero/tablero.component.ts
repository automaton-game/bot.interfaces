import { Component, Input } from '@angular/core';
import { FilaTablero } from '../modelos/filaTablero';
import { Tablero } from '../modelos/tablero';

@Component({
  selector: 'tablero-component',
  templateUrl: './tablero.component.html',
  styleUrls: []
})
export class TableroComponent {
  private _tableros: Tablero[];

  public filas: Array<FilaTablero>;
  public actual: number;

  @Input()
  set tableros(tableros: Tablero[]) {
    this._tableros = tableros;
    this.actual = tableros.length - 1;
    this.actualizarTablero();
  }

  get tableros(): Tablero[] { return this._tableros; }

  actualizarTablero() {
    this.filas = this.tableros[this.actual].filas;
  }
}
