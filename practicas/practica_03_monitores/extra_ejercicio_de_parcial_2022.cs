- Ejercicio de parcial 2022

En un sistema operativo se ejecutan 20 procesos que periódicamente realizan cierto cómputo mediante la función Procesar().
Los resultados de dicha función son persistidos en un archivo, para lo que se requiere de acceso al subsistema de E/S.
Sólo un proceso a la vez puede hacer uso del subsistema de E/S, y el acceso al mismo se define por
la prioridad delproceso (menor valor indica mayor prioridad).

process proceso [ id:0..19 ] {
  typeT resultado;
  int prioridad = obtenerPrioridad();

  while (true) {
    resultado = Procesar();
    accesoES.solicitarES(id, prioridad);
    Persistir(resultado);
    accesoES.abandonarES(id);
  }
}

monitor accesoES:: {
  cond vcEspera[20];
  cola colaEspera[20];
  int esperando = 0;
  libre = true;

  procedure solicitarES(int in id) {
    if (libre)
      libre = false;
    else {
      InsertarOrdenado(colaEspera, id);
      esperando++;
      wait(vcEspera[id]);
    }
  }

  procedure abandonarES() {
    ind auxId;
    if (esperando > 0) {
      pop(colaEspera, auxId);
      esperando--;
      signal(vcEspera[auxId];
    }
    else {
      libre = true;
    }
  }
}
