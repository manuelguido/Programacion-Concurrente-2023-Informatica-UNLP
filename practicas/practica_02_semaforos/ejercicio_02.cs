- Ejercicio 02

cola recursos[5];
sem mutexCola = 1;
sem mutexRecursos = 5;

process Proceso[ id=1..N ] {
    tipoRecurso recurso;
    while(true) {
        P(mutexRecursos);
        P(mutexCola);
        recurso = recursos.pop();
        V(mutexCola);
        // utiliza el recurso
        P(mutexCola);
        recursos.push(recurso);
        V(mutexCola);
        V(mutexRecursos);
    }
}
