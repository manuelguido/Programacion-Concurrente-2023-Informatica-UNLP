- Ejercicio 07

-- Opción 1 (sin cola)

sem mutexDescarga = 7, mutexCamionTrigo = 5, mutexCamionMaiz = 5;

process camionTrigo [id:1..T] {
  P(mutexCamionTrigo);
  P(mutexDescarga);
  // Descargar trigo
  V(mutexDescarga);
  V(mutexCamionTrigo);
}

process camionMaiz [id:1..T] {
  P(mutexCamionMaiz);
  P(mutexDescarga);
  // Descargar maíz
  V(mutexDescarga);
  V(mutexCamionMaiz);
}

-- Opción 2 (con cola para simular carga de datos)

sem mutexDescarga = 7, mutexCamionTrigo = 5, mutexCamionMaiz = 5;
sem mutexCola = 1;
cola descargas;

process camionTrigo [id:1..T] {
  tipoDescarga descarga;

  P(mutexCamionTrigo);
  P(mutexDescarga);

  P(mutexCola);
  cola.push(descarga);
  V(mutexCola);

  V(mutexDescarga);
  V(mutexCamionTrigo);
}


process camionMaiz [id:1..T] {
  tipoDescarga descarga;

  P(mutexCamionMaiz);
  P(mutexDescarga);

  P(mutexCola);
  cola.push(descarga);
  V(mutexCola);

  V(mutexDescarga);
  V(mutexCamionMaiz);
}
