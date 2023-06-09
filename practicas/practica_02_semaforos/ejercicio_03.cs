- Ejercicio 03

El código funciona pero no de manera correcta,
dado que al hacer primero P(sem) en lugar de P(alta) y P(baja) respectivamente,
puede suceder que 6 usuarios de alta prioridad ejecuten P(sem) y solo 4 de ellos
accedan a la BD ya que 2 quedarían bloqueados en P(alta) y bloqueando la BD,
cuando en realidad habría lugar para 2 procesos de prioridad baja para acceder.

La solución correcta es la siguiente:

sem: semaphoro := 6;
alta: semaphoro := 4;
baja: semaphoro := 5;

process usuario Alta [i:1..L] {
    P(alta);
    P(sem);
    // utiliza la BD
    V(sem);
    V(alta);
}

process usuarioBaja [i:1..K] {
    P(baja);
    P(sem);
    // utiliza la BD
    V(sem);
    V(baja);
}
