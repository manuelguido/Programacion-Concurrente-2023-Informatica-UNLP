- Ejercicio 05

-- Versión 1)

espectador [ id = 1 to E ] {
  coordinador ! avisarLlegada(id);
  coordinador ? obtenerMaquina[id]();
  // utilizar máquina expendedora
  coordinador ! liberarMaquina();
}

process coordinador {
  cola buffer;
  int idEspectador;

  do
    [ ] espectador[*] ? avisarLlegada(idEspectador); ->
      if (empty (cola))
        espectador ! obtenerMaquina[idEspectador]();
      else
        push(buffer, idEspectador);
    [ ] espectador[*] ? liberarMaquina(); ->
      if (not empty (cola))
        pop(buffer, idEspectador);
        espectador ! obtenerMaquina[idEspectador]();
  od
}


-- Versión 2)

espectador [ id = 1 to E ] {
  coordinador ! encolar(id);
  coordinador ! pedido();
  coordinador ? obtenerMaquina[id]();
  // utilizar máquina expendedora
  coordinador ! liberarMaquina();
}

process coordinador {
  cola buffer;
  int idEspectador;
  bool libre = true;

  do espectador[*] ? encolar(idEspectador); ->
      push(buffer, idEspectador);

    [ ] (libre && not empty(cola));
    espectador[*] ? pedido(); ->;
      pop(buffer, idEspectador);
      espectador ! obtenerMaquina[idEspectador]();
      libre = false;

    [ ] (not libre) espectador[*] ? liberarMaquina(); ->
      libre = true;
  od
}
