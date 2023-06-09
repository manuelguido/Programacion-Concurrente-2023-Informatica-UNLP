- Ejercicio 06

process persona [ id=0..19 ] {
  int i;
  int nroGrupo = '...'; // Se supone que ya se sabe
  int monedas[15];
  int valorTotal = 0;
  int valorTotalDeGrupo = 0;

  juego.llegar(nroGrupo);
  for i = 1 to 15 {
    int monedas[i-1] = Moneda();
    valorTotal = valorTotal + monedas[i-1];
  }
  juego.guardarValor(valorTotal);
  juego.obtenerValorTotalDeGrupo(nroGrupo, valorTotalDeGrupo);
}


monitor juego {
  cond esperaGrupo[5], esperaTotal[4];
  int totalLlegados[5] = ([5] 0);  // Quienes llegan para jugar
  int totalSumados[5] = ([5] 0);  // Quienes ya almacenaron su valor
  int valorTotal[5] = ([5] 0);     // Valores totales de los grupos

  procedure llegar(int in nroGrupo) {
    totalLlegados[nroGrupo]++;
    if (totalLlegados[nroGrupo] < 4) wait(esperaGrupo[nroGrupo]);
    else signal_all(esperaGrupo[nroGrupo]);
  }

  procedure guardarValor(int in valor, int in nroGrupo) {
    valorTotal[nroGrupo] = valorTotal[nroGrupo] + valor;
    totalSumandos[nroGrupo]++;
    if (totalSumados[nroGrupo] < 4) wait(esperaTotal[nroGrupo]);
    else signal_all(esperaTotal[nroGrupo]);
  }

  procedure obtenerValorTotalDeGrupo(int in nroGrupo, int out valorTotalDeGrupo) {
    valorTotalDeGrupo = valorTotal[nroGrupo];
  }
}
