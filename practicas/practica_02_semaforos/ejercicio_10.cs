- Ejercicio 10

sem hayCincoPersonas = 0, mutexCola = 1;
sem esperarVacunacion[N] = ([N] 0);
int totalEspera = 0;
cola colaLlegada;

process Persona [ id: 1..50 ] {
  P(mutexCola);
  push(colaLlegada, id);
  totalEspera++;
  if (totalEspera = 5) {
    totalEspera = 0;
    V(hayCincoPersonas);
  }
  V(mutexCola);
  P(esperarVacunacion[i]);
}

process Empleado {
  int i, j, idPersona, auxId;
  int totalAtendidos = 0;
  cola idsActuales;

  while (totalAtentidos < 50) {
    P(hayCincoPersonas);
    for i = 1 to 5 {
      P(mutexCola);
      pop(colaLlegada, idPersona);
      V(mutexCola);

      VacunarPersona();
      totalAtendidos++;

      // Guarda el id para liberar en el
      // siguiente bucle for.
      push(idsActuales, idPersona);
    }

    for j = 1 to 5 {
      pop(idsActuales, auxId);

      // Libera a la persona para que se vaya
      V(esperarVacunacion[auxId]);
    }
  }
}

Aclaración de la resolución:

En el proceso Empleado hay dos bucles for porque en el enunciado interpreto que dice que las 5 personas no se van hasta que las 5
se hayan vacunado. En caso de no ser así, es decir que cada persona solo espera a ser vacunada a ella misma solamente.
Podría hacerse todo en un bucle for sin necesidad utilizar la variable local que guarda los ids.

Quedaría de la siguiente forma:

sem hayCincoPersonas = 0, mutexCola = 1;
sem esperarVacunacion[N] = ([N] 0);
int totalEspera = 0;
cola colaLlegada;

process Persona [ id: 1..50 ] {
  P(mutexCola);
  push(colaLlegada, id);
  totalEspera++;
  if (totalEspera = 5) {
    totalEspera = 0;
    V(hayCincoPersonas);
  }
  V(mutexCola);
  P(esperarVacunacion[i]);
}

process Empleado {
  int i, idPersona;
  int totalAtendidos = 0;
  cola idsActuales;

  while (totalAtentidos < 50) {
    P(hayCincoPersonas);
    for i = 1 to 5 {
      P(mutexCola);
      pop(colaLlegada, idPersona);
      V(mutexCola);

      VacunarPersona();
      totalAtendidos++;
      V(esperarVacunacion[i]);
    }
  }
}
