- Ejercicio 09

El acceso a variables compartidas mediante exclusión con semáforos está bien desarrollada en la mayoría de las variables pero no todas.

Los principales errores en el código son:

1. Un número de alumno se carga en ambas colas, una para el profesor A y otra para el profesor B,
   pero cuando es sacada por un profesor el cual luego le toma examen, el id del alumno seguirá cargado
   en la cola del otro profesor hasta que eventualmente lo saque y evalue si esta en estado "Esperando",
   lo cual podría considerarse ineficiente aunque no generaría un error.

2. Un error muy importante en cuanto a la concurrencia del programa es que el acceso por ambos profesores
   a la variables estadoAlumno[idAlumno] no es realizada de manera atomica mediante el uso de semáforos,
   por lo tanto se puede generar interferencia entre ambos procesos al momento de leer/escribir dicha variable,
   en caso de que esten manipulando el id de un mismo alumno, lo cual es suficiente para que el programa
   pueda generar un error de ejecución.

3. Un detalle menor es que la función que utiliza el proceso profesor B es "popAleatorio()" cuando el enunciado dice que el profesor
   saca el menor numero de alumno de la cola, por lo tanto el nombre de la funcion debería ser algo mas acorde y descriptivo,
   como por ejemplo "obtenerMinimo()".

Una solución a esto podría ser la siguiente:

string estado[N] = ([N] "Esperando");
queue ColaA, ColaB;
sem llegoA, llegoB = 0;
sem esperando[N] = ([N] 0);
sem mutexA, mutexB, =1;
sem mutexEsperando[N] = ([N]1);

process Alumno [ i= 0 .. N-1 ] {
  P(mutexA);
  push(colaA, i);
  V(mutexA);

  V(llegoA);

  P(mutexB);
  push(colaB, i);
  V(mutexB);

  V(llegoB);

  P(esperando[i]);
  if (estado[i] == 'A') {
    // Interactúa con el profesor a
  } else {
    // Interactúa con el profesor b
  }

  P(esperando[i]);
}

process profesorA:: {
  int idAlumno;

  whille(true) {
    P(llegoA);
    P(mutexA);
    idAlumno = pop(colaA);
    V(mutexA);

    P(mutexEsperando[idAlumno]);
    if(estado[idAlumno] == 'Esperando') {
      estado[idAlumno] = 'A';
      V(mutexEsperando[idAlumno]);

      V(esperando[idAlumno]);
      // Se toma el examen
      V(esperando[idAlumno]);
    }
  }
}

process profesorB:: {
  int idAlumno;

  whille(true) {
    P(llegoB);
    P(mutexB);
    idAlumno = obtenerMinimo(colaB);
    V(mutexB);

    P(mutexEsperando[idAlumno]);
    if(estado[idAlumno] == 'Esperando') {
      estado[idAlumno] = 'B';
      V(mutexEsperando[idAlumno]);

      V(esperando[idAlumno]);
      // Se toma el examen
      V(esperando[idAlumno]);
    }
  }
}
