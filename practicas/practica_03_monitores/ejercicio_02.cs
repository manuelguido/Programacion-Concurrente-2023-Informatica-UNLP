- Ejercicio 02

-- a)

process Persona [ id:1..N ] {
  Acceso.solicitar();
  Fotocopiar();
  Acceso.abandonar();
}

monitor Accceso {
  bool libre = true;
  cond dormidos;


  procedure solicitar () {
    while (not libre) wait(dormidos);
    libre = false;
  }

  procedure abandonar () {
    libre = true;
    signal(dormidos);
  }
}

-- b)

process Persona [ id:1..N ] {
  Acceso.solicitar();
  Fotocopiar();
  Acceso.abandonar();
}

monitor Accceso {
  int esperando = 0;
  bool libre = true;
  cond dormidos;

  procedure solicitar () {
    if (libre) libre = false;
    else {
      esperando++;
      wait(dormidos);
    }
  }

  procedure abandonar () {
    if (esperando > 0) {
      esperando--;
      signal(dormidos);
    }
    else libre = true;
  }
}

-- c)

process Persona [ id:1..N ] {
  int edad = '...';
  Acceso.solicitar(id ,edad);
  Fotocopiar();
  Acceso.abandonar();
}

monitor Accceso {
  int esperando = 0;
  bool libre = true;
  cond dormidos[N];
  cola espera[N];

  procedure solicitar (int in id, int in edad) {
    if (libre) libre = false;
    else {
      esperando++;
      insertar_ordenado(espera, (id, edad));
      wait(dormidos[id]);
    }
  }

  procedure abandonar () {
    int auxId;
    if (esperando > 0) {
      esperando--;
      sacar(espera, auxId));
      signal(dormidos[auxId]);
    }
    else libre = true;
  }
}

-- d)

process Persona [ id:1..N ] {
  Acceso.solicitar(id);
  Fotocopiar();
  Acceso.abandonar();
}

monitor Accceso {
  int siguiente = 1;
  cond dormir[N];

  procedure solicitar (int in id) {
    if (siguiente <> id) wait dormir[id];
  }

  procedure abandonar () {
    siguiente++; 
    signal(dormir[siguiente]);
  }
}

-- e)

process Persona [ id:1..N ] {
  Acceso.solicitar();
  Fotocopiar();
  Acceso.abandonar();
}

process Empleado:: {
  int personasQuePasaron = 0;
  while (personasQuePasaron < N) {
    Acceso.esperarPersona();
    personasQuePasaron++;
  }
}

monitor Accceso {
  int esperando = 0;
  libre = true;
  cond dormidos, hayPersonas, esperarLiberacion;

  procedure solicitar () {
    esperando++;
    signal(hayPersonas);
    wait(dormidos);
  }

  procedure esperarPersona () {
    if (esperando == 0) wait(hayPersonas);
    if (not libre) wait(esperarLiberacion);
    libre = false;
    esperando--;
    signal(dormidos);
  }

  procedure abandonar () {
    libre = true;
    signal(esperarLiberacion);
  }
}

-- f)

process Persona [ id:1..N ] {
  int fotocopiadora;
  Acceso.solicitar(id, fotocopiadora);
  Fotocopiar(fotocopiadora);
  Acceso.abandonar(fotocopiadora);
}

process Empleado:: {
  int personasQuePasaron = 0;
  while (personasQuePasaron < N) {
    Acceso.esperarPersona();
    personasQuePasaron++;
  }
}

monitor Accceso {
  int esperando = 0, libres = 10, int i;
  cond dormidos[N], hayPersonas, esperarLiberacion;
  cola espera, fotocopiadoras;
  int fotocopiadorasAsignadas[N];


  // Código de inicialización (carga todos los números de fotocopiadoras)
  for i = 1 to 10 push(fotocopiadoras, i);


  procedure solicitar (int in id, int out fotocopiadora) {
    esperando++;
    push(esperar, id);
    signal(hayPersonas);
    wait(dormidos[N]);
    fotocopiadora = fotocopiadorasAsignadas[id];
  }

  procedure esperarPersona () {
    int auxFotocopiadora, auxId;

    if (esperando == 0) wait(hayPersonas);
    while (libres == 0) wait(esperarLiberacion);
    libre--;
    esperando--;
    pop(esperar, auxId);
    pop(fotocopiadoras, auxFotocopiadora);
    fotocopiadorasAsignadas[auxId] = auxFotocopiadora;
    signal(dormidos[auxId]);
  }

  procedure abandonar (int in fotocopiadora) {
    libres++;
    push(fotocopiadoras, fotocopiadora);
    signal(esperarLiberacion);
  }
}
