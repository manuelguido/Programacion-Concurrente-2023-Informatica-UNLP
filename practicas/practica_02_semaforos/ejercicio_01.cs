- Ejercicio 01

-- a)

sem mutex = 1;

process Persona [id=1..N] {
    P(mutex);
    – usar detector;
    V(mutex);
}

-- b)

sem mutex = 3;

process Persona [id=1..N] {
    P(mutex);
    // utiliza el detector;
    V(mutex);
}
