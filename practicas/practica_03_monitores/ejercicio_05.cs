- Ejercicio 05

process jugador [ id:0..19 ] {
  int equipo, cancha;

  equipo = DarEquipo();
  entrenamiento.ingresar(equipo, cancha);
  cancha[cancha].llegada();
}

process partido[ id:0..1 ] {
  int i;

  entrenamiento.esperarEquipos(id);
  cancha[id].iniciar();
  delay(50);
  cancha[id].terminar();
}


monitor entrenamiento {
  int i;
  int totalEquipos = 0;
  int llegados[4];
  cond espera[4];
  cola equiposCompletos[2];
  int nroCancha[4];


  procedure ingresar(int in equipo, int out cancha) {
    int llegados[equipo]++;
    if (llegados[equipo] == 5) {
      totalEquipos++;

      // Para asegurar que los primeros dos                      
      equipos vayan a la cancha 1
      if (totalEquipos < 2)
        push(equiposCompletos[0], equipo);
      else
        push(equiposCompletos[1], equipo);

      if (totalEquipos mod 2 == 0)
        signal(hayEquiposParaJugar());
    }
    wait(espera[equipo]);
    cancha = nroCancha[equipo];
  }
}

monitor cancha[ id:0..1 ] {
  cond vcJugadores;
  int cant;

  procedure llegada() {
    cant++;
    if (cant == 10) signal(comenzar);
    wait(vcJugadores);
  }

  procedure iniciar()
  {
    if (cant < 10) wait(comenzar);
  }

  procedure terminar()
  {
    signal_all(vcJugadores);
  }

  procedure esperarEquipos(int in id) {
    int equipo1, equipo2;

    while (totalEquipos < 2) {
      wait(hayEquiposParaJugar);
    }

    totalEquipos = totalEquipos - 2;
    pop(equiposCompletos, equipo1);
    pop(equiposCompletos, equipo2);
    nroCancha[equipo1] = id;
    nroCancha[equipo1] = id;
    signal_all(espera[equipo1]);
    signal_all(espera[equipo2]);
  }
}
