- Ejercicio 03

process Cliente [ id:1..N ] {
  tipoL listaDeProductos;
  tipoC comprobante;

  Corralon.avisarLlegada(id);
  Corralon.serAtendido(listaDeProductos, comprobante);
}

process Empleado {
  int personasQuePasaron = 0;

  while (personasQuePasaron < N) {
    Corralon.atenderPersona();
    personasQuePasaron++;
  }
}

monitor Corralon {
  int total = 0;
  cond dormidos[N], esperarComprobante;
  cola espera;
  tipoL listaActual;
  tipoC comprobanteActual;

  procedure avisarLlegada(int in id) {
    total++;
    push(espera, id);
    signal(HayPersonas);
    wait(dormidos[id]);
  }

  procedure serAtendido(tipoL in lista, tipoC out comprobante) {
    listaActual = lista;
    signal(esperarLista);
    wait(esperarComprobante);
    comprobante = comprobanteActual;
    signal(esperarQueTomeElComprobante);
  }

  procedure atenderPersona() {
    tipoT auxLista;
    int auxId;

    if (total = 0) wait(hayPersonas);
    pop(espera, auxId);
    total--;
    signal(dormidos[auxId]);
    wait(esperarLista);

    auxLista = listaActual;
    comprobanteActual = generarComprobante(auxLista);
    signal(esperarComprobante);
    wait(esperarQueTomeElComprobante);
  }
}
