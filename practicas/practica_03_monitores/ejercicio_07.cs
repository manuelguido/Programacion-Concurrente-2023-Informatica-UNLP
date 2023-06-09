- Ejercicio 07

process corredor [ id=0..C ] {
  expendedora.llegarParaCorrer();
  // Corre la carrera
  expendedora.solicitarBotella(int in id);
  expendedora.buscarBotella();
}


process repositor:: {
  while (true) {
    expendedora.esperarReposición();
    for i:= 1 to 20 {
      expendedora.reponer();
    }
  }
}

monitor expendedora:: {
  cond comienzoDeCarrera, esperaBotella[C], avisarRepositor;
  cola colaEspera;
  int totalBotellas = 20;
  int esperandoBotella = 0;
  int totalLlegadosParaCorrer = 0;
  bool libre = true;

  procedure llegarParaCorrer() {
    totalLlegadosParaCorrer++;
    if (totalLlegadosParaCorrer < C) wait(comienzoDeCarrera);
    else signalAll(comienzoDeCarrera);
  }

  procedure solicitarBotella(int in id) {
    if (libre) {
      libre = false;
    else {
      esperandoBotella++;
      push(colaEspera, id);
      wait(esperaBotella[id]);
    }
  }

  procedure buscarBotella() {
    int auxId;
    if (totalBotellas == 0) {
      signal(avisarRepositor);
      wait(hayBotellas);
    }

    botella--;  // Toma la botella
    if (esperandoBotella > 0) {
      pop(colaEspera, auxId);
      signal(esperaBotella[auxId]);
    }
    else {
      libre = true;
    }
  }

  procedure esperarReposicion() {
    if (totalBotellas > 0) wait(esperarReposicion);
  }

  procedure reponer() {
    totalBotellas++;
    signal(hayBotellas);
  }
}
