- Ejercicio 06

cola tareas;
sem mutexCola = 1, mutexLlegados = 1;

int tareasFinalizadas[E] = ([E] 0);
int llegados = 0;

process Empleado [ id:1..E-1 ] {
  int i;
  tipoTarea tarea;
    
  P(mutexLlegados);
  llegados++;
  if (llegados = E) for i:= 1 to E V(comenzar);
  V(llegados);
  P(comenzar);

  P(mutexCola);
  while(not tareas.empty()) {
    tarea = tareas.pop();
    V(mutexCola);

    tarea.realizar();
    tareasFinalizadas[id]++;
    P(mutexCola);
    if (tareas.empty()) V(finalizaron);
  }
  V(mutexCola);
}
