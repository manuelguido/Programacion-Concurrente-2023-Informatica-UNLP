- Ejercicio 03

-- a)

process alumno [ id = 1 to N ] {
  tipoExamen examen;
  int nota;
 
  examen = realizarExamen();
  coordinador ! entregar(id, examen);
  profesor ? recibirNota(nota);
}

process profesor {
  int total = 0;
  int auxId, auxNota;
  tipoExamen auxExamen;

  while (total < N) {
    coordinador ! pedido();
    coordinador ? recibirExamen(auxId, auxExamen);
    auxNota = corregir(auxExamen);
    alumno[auxId] ! notaExamen(auxNota);
    total ++;
  }
}

process coordinador {
  cola examenes;
  tipoExamen auxExamen;
  int auxId;

  do
    alumno [*] ? entregar(auxExamen) -> 
      examenes.push(auxId, auxExamen);
    (not empty (examenes)); profesor ? pedido() ->
      examenes.pop(auxId, auxExamen);
      profesor ! recibirExamen(auxId, auxExamen);
  od 
}

-- b)

process alumno [ id = 1 to N ] {
  tipoExamen examen;
  int nota;
 
  examen = realizarExamen();
  coordinador ! entregar(id, examen);
  profesor ? recibirNota(nota);
}

process profesor [ id = 1 to P ] {
  int idAlumno, auxNota;
  tipoExamen auxExamen;

  while (true) {
    coordinador ! pedido(id);
    coordinador ? recibirExamen(idAlumno, auxExamen);
    auxNota = corregir(auxExamen);
    alumno[idAlumno] ! notaExamen(auxNota);
  }
}

process coordinador {
  cola examenes;
  tipoExamen auxExamen;
  int idAlumno, idProfesor;

  do
    alumno [*] ? entregar(auxExamen) -> 
      examenes.push(idAlumno, auxExamen);
    (not empty (examenes)); profesor[*] ? pedido(idProfesor) ->
      examenes.pop(idAlumno, auxExamen);
      profesor[idProfesor] ! recibirExamen(idAlumno, auxExamen);
  od 
}

-- c)
process alumno [ id = 1 to N ] {
  tipoExamen examen;
  int nota;

  coordinador ! llego();
  coordinador ? comenzar();
  examen = realizarExamen();
  coordinador ! entregar(id, examen);
  profesor ? recibirNota(nota);
}

process profesor [ id = 1 to P ] {
  int idAlumno, auxNota;
  tipoExamen auxExamen;

  while (true) {
    coordinador ! pedido(id);
    coordinador ? recibirExamen(idAlumno, auxExamen);
    auxNota = corregir(auxExamen);
    alumno[idAlumno] ! notaExamen(auxNota);
  }
}

process coordinador {
  cola examenes;
  tipoExamen auxExamen;
  int idAlumno, idProfesor;
  int totalLlegados = 0;


  // Opcion A
  do (totalLlegados < N); alumno [*] ? llego() -> totalLlegados ++;

  if (totalLlegados == N) { // me parece que si if
    for [i = 1 to N] alumno[i] ! comenzar();
  }

  // Opcion B
  for i in 1 to N alumno [*] ? llego();
  for i in 1 to N alumno [i] ! comenzar();

  do
    [ ] alumno [*] ? entregar(auxExamen) -> 
      examenes.push(idAlumno, auxExamen);

    [ ] (not empty (examenes)); profesor ? pedido(idProfesor) ->
      examenes.pop(idAlumno, auxExamen);
      profesor[idProfesor] ! recibirExamen(idAlumno, auxExamen);
  od 
}
