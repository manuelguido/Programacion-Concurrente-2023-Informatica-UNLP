- Ejercicio 11

sem mutexColas = 1, mutexTotal = 1;
sem espera[150] = ([150] 0), hayPasajero[3] = ([3] 0);

cola colaEspera[3];
int cantCola[3] = ([3]0), total = 0;

process Pasajero [ id: 0..149 ] {
  // El siguiente if else podría ser una función obtener mínimo o algo similar.
  // Pero es para que quede más claro. 
  P(mutexColas);
  if (cantCola[0] < cantCola[1]) and (cantCola[0] < cantCola[2]) {
    push(colaEspera[0], id); cantCola[0]++;
    V(hayPasajero[0]);
  } else if (cantCola[1] < cantCola[2]) {
    push(colaEspera[1], id); cantCola[1]++;
    V(hayPasajero[1]);
  } else {
    push(colaEspera[2], id); cantCola[2]++;
    V(hayPasajero[2]);
  }
  V(mutexColas);

  P(espera[id]);
  // es atendido
  P(espera[i]);
}

process Enfermera [ id: 0..2 ] {
  int auxId;

  P(mutexTotal);
  while(total < 150) {
    total++; // Evita que ingresen a buscar mas de 150 pacientes
    V(mutexTotal);
    P(hayPasajero[id]);
    P(mutexColas);
    auxId = colaEspera[id].pop();
    cantCola[id]--;
    V(mutexColas);

    V(espera[auxId]);
    Hisopar(auxId);
    V(espera[auxId]);

    P(mutexTotal);
  }
  V(mutexTotal);
}
