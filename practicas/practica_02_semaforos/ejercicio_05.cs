- Ejercicio 05

sem mutexEligieron = 1; eligieronTodos = 0, grupoTerminado = 0, comenzar = 0;
sem tareasPorGrupo [10] = ([10] 1);
sem puntajesListos [10];

int eligieron = 0;
int tareasFinalizadas [10] = ([10] 0);
int puntajes [10];

process Alumno [ id: 0..49 ] {
  int tarea;

  tarea = elegir();
  P(mutexEligieron);
  eligieron++;
  if (eligieron = 50) V(eligieronTodos);
  V(mutexEligieron);

  P(comenzar);
  // realizar tarea

  P(tareasPorGrupo[tarea]);
  tareasFinalizadas[tarea]++;
  if (tareasFinalizadas[tarea] = 10) {
    V(grupoTerminado);

    P(mutexColaTerminados);
    colaTerminados.push(tarea);
    V(mutexColaTerminados);
  }
  V(tareasPorGrupo[tarea]);

  P(mutexVerPuntajes[tarea]);
  //  el alumno ve el puntaje
}

process Profesor {
  int i, auxTarea;
  int actualPuntaje = 1;

  P(eligieronTodos);

  for i = 1 to 50 V(comenzar);
  for i = 1 to 10 {
    P(grupoTerminado);

    P(mutexColaTerminados);
    auxTarea = colaTerminados.pop();
    V(mutexColaTerminados);

    // corregir tarea

    puntajes[tarea] = puntajeActual;
    for i = 1 to 10 V(verPuntajes[tarea]);
    puntajeActual++;    }
}

// Puntaje actual no es necesario, se podría usar el valor de i. Pero es para que quede más claro.
