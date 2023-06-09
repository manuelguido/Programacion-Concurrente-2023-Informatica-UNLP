- Ejercicio 04

process alumno [ id=1..50 ] {
  int nroGrupo, nota;

  comision.formarFila(id);
  comision.obtenerNroDeGrupo(id, nroGrupo);
  // realizar la práctica
  comision.entregarPractica(id, nroGrupo);
  comision.obtenerNota(nroGrupo, nota);
}

process jefeDeTrabajosPracticos {
  int i, auxNroGrupo;
  int proximaNota =25;

  comision.esperarAlumnos();
  for i = 1 to 50 {
    auxNroGrupo = DarNumero();
    comision.asignarTp(auxNroGrupo);
  }
  for i = 1 to 25 {
    comsion.esperarGrupoYCorregir(proximaNota);
    proximaNota--;
  }
  comision.avisarAlumnosQueSeRetiren();
}

monitor comision {
  int totalFila = 0, gruposEsperandoCorreccion = 0;
  int asignacionDeGrupo[50], cantFinalizados[25];
  cond comenzar[50], esperarTodos, finalizados[25], alumnosPuedenRetirarse;
  cola esperaComenzar, colaGruposEntregados;

  procedure formarFila(int in id) {
    totalFila++;
    push(esperaComenzar, id);
    if (totalFila == 50) signal(esperarTodos);
    wait(comenzar[id]);
  }

  procedure esperarAlumnos() {
    if (totalFila < 50) wait(esperarTodos);
  }

  procedure obtenerNroDeGrupo(int in id, int out nroGrupo) {
    nroGrupo = asignacionDeGrupo[id];
  }

  procedure asignarTp(int in nroGrupo) {
    int auxId;
    pop(esperaComenzar, auxId);
    asignacionDeGrupo[auxId] = nroGrupo;
    signal(comenzar[auxId);
   }

  procedure entregarPractica(int in id, int in nroGrupo) {
    cantFinalizados[nroGrupo]++;
    if (cantFinalizados[nroGrupo] == 2) {
      push(colaGruposEntregados, nroGrupo);
      gruposEsperandoCorreccion++;
      signal(grupoTermino);
    }
    wait(finalizados[nroGrupo]);
  }

  procedure esperarGrupoYCorregir(int in nota) {
    int auxNroGrupo;
    if (gruposEsperandoCorreccion == 0) wait(grupoTermino);
    gruposEsperandoCorreccion--;
    pop(colaGruposEntregados, auxNroGrupo);
    notaDeGrupo[auxNroGrupo] = nota;
    signal_all(finalizados[nroGrupo]);
  }

  procedure obtenerNota(int in nroGrupo, int out nota) {
    nota = notaDeGrupo[nroDeGrupo];
    wait(alumnosPuedenRetirarse);
  }

  procedure avisarAlumnosQueSeRetiren() {
    signal_all(alumnosPuedenRetirarse);
  }
} 
