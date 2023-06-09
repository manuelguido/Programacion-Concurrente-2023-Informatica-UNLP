- Ejercicio 06

int llegados = 0;
bool comenzar = false;
cola cola_entregados[P];
int notas[P] = -1;
int entregados;
int corregidos = 0;

Process Alumno[ id = 1 .. P ] {
    <llegados ++>;
    <await comenzar>;
    // Hace el examen
    <cola_entrega.push(id)>;
    <await notas[id] <> -1>;
    // ver nota
}

Process Profesor:: [ id = 1 .. Q ]{
  int id_alumno = -1;
  <await (llegados = P)>;
  <if (comenzar = false) comenzar = true>;
  while (corregidos < P) {
    <if (not cola_entrega.isEmpty()); id_alumno = cola_entrega.pop()>;
    // corregir examen
    if (id_alumno <> -1) {
      notas[id_alumno] = generarNota();
      <corregidos++>;
    }
  }
}
