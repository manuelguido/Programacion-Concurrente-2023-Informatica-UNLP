- Ejercicio 01

process Lector [ id:1..N ] {
  Acceso.solicitarBD();
  // Utilizan la BD
  Acceso.abandonarBD();
}

monitor Accceso {
  int total = 0;
  cond vc;

  procedure solicitarBD () {
    while (total == 5) wait(vc);
    total = total + 1;
  }

  procedure abandonarBD () {
    total = total -1;
    signal(vc);
  }
}
