- Ejercicio 01

process robot [ i = 1 to R ] {
  string sitio_web;

  while (true) {
    sitio_web = obtener_sitio_web();
    coordinador ! (sitio_web);
  }
}

process analizador {
  string sitioWeb;

  while (true) {
    coordinador ! disponible();
    coordinador[*] ? (sitio_web);
    analizar_infectado(sitio_web);
  }
}

process coordinador {
  string cola[];
  string sitio_aux;

  do
    robot[*] ? (sitio_aux) ->
      push(cola, sitio_aux);
    (not empty(cola)); analizador ? disponible() ->
      pop(cola, sitio_aux);
      analizador ! (sitio_aux);
  od
}
