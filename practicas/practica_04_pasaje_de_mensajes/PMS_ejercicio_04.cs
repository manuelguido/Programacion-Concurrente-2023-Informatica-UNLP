- Ejercicio 04

process persona [ id = 1 to P ] {
  coordinador ! llegue(id);
  empleado ? obtenerSimulador[id]();
  // utilizar el simulador
  empleado ! liberarSimulador[id]();
}

process empleado {
  int idPersona;

  while (true) {
    coordinador ! simuladorLibre();
    coordinador ? obtenerSiguiente(idPersona);
    persona[idPersona] ! obtenerSimulador();
    persona[idPersona] ? liberarSimulador();
  }
}

process coordinador {
  cola buffer;
  int idPersona;

  do
     [ ] persona [*] ? llegue(idPersona) ->
      push(buffer, idPersona);
     [ ] (not empty(cola)); empleado ? simuladorLibre() ->
      pop(buffer, idPersona);
      empleado ! obtenerSiguiente(idPersona);
  od
}
