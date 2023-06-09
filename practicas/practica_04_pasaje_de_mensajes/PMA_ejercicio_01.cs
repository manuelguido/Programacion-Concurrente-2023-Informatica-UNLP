- Ejercicio 01

chan espera(int), atencion[N](int), listo[N](int);

process persona [ id = 1 to N ] {
  int idEmpleado;
  bool listo;

  send espera(id);
  receive atencion[id](idEmpleado);
  // ser atendido
  receive listo[id](listo);
}


process empleado [ id = 1 to 2 ] {
  int idPersona;

  while (true) {
    receive espera(idPersona);
    send atencion(id);
    // atender persona
    send listo[idPersona](true);
  }
}
