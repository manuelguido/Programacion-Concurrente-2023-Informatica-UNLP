- Ejercicio 04

-- a)

sem mutex = 1;

process Persona [id:1..N] {
    P(mutex);
    Imprimir(documento);
    P(mutex);
}

-- b)

sem mutex = 1;
sem espera[N] = ([N]0);
boolean libre = false;
queue cola;

process Persona [id:1..N] {
  int aux;
  P(mutex)
  if (not libre) {
    cola.push(id);
    V(mutex);
    P(espera[id);
  else {
    libre = false;
    V(mutex);
  }
  Imprimir(documento);
  V(mutex);
  P(mutex);

  if (empty cola) {
    libre: true;
  }
  else {
  aux = cola.pop();
    V(espera[aux]);
  }
  V(mutex);
}

-- c)

sem mutex = 1;
sem espera[N] = ([N]0);
sig = 1;

process Persona [id:1..N] {
  P(mutex);
  if (sig != id) {
    V(mutex);
    P(espera[id]);
  }
  Imprimir(documento);
  P(mutex);
  sig = sig + 1;
  V(mutex);
  V(espera[sig]);
}

-- d)

sem mutexCola = 1, mutexColaNoVacia = 0, mutexListo = 0;
sem espera[N] = ([N]0);
int finalizados = 0;
queue cola;

process Persona [id:1..N] {
  P(mutexCola);
  cola.push(id);
  V(mutexCola);
  V(mutexColaNoVacia);
  P(espera[id]);
  Imprimir(documento);
  finalizados = finalizados + 1; // No necesita semáforo
  V(mutexListo);
}

process Coordinador {
  int auxId;
  while (finalizados < N) {
    P(mutexColaNoVacia);
    P(mutexCola);
    auxId = cola.pop();
    V(mutexCola);
    V(espera[auxId]);
    P(mutexListo);
  }    
}

-- e)

sem mutexCola = 1, mutexFinalizados = 1, mutexColaNoVacia = 0;
mutexColaImpresoras = 0, mutexImpresorasLibres = 5;
sem espera[N] = ([N]0);
int finalizados = 0;
int impresora_persona[N];
queue cola;
queue impresoras = {1,2,3,4,5};

process Persona [id:1..N] {
  int impresora;

  P(mutexCola);
  cola.push(id);
  V(mutexCola);
  V(mutexColaNoVacia);
  P(espera[id]);

  impresora = impresora_persona[id];
  Imprimir(documento, impresora);

  P(mutexFinalizados)
  finalizados = finalizados + 1;
  V(mutexFinalizados);

  P(mutexImpresoras);
  impresoras.push(impresora);
  V(mutexImpresoras);

  V(mutexImpresorasLibres);
}

process Coordinador {
  int auxId, auxImp;

  P(mutexFinalizados);
  while (finalizados < N) {
    V(mutexFinalizados);

    P(mutexColaNoVacia);
    P(mutexCola);
    auxId = cola.pop();
    V(mutexCola);

    P(mutexImpresorasLibres);

    P(mutexImpresoras);
    auxImp = impresoras.pop();
    V(mutexImpresoras);

    impresora_persona[auxId] = auxImp;
    V(espera[auxId]);
    P(mutexFinalizados);
  }
  V(mutexFinalizados);
}
