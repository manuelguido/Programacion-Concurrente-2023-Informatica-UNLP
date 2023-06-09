- Ejercicio 07

int maxMarcos = 30, maxVidrios = 50;
sem mutexColaMarcos = 1, mutexColaVidrios = 1;
sem puedeHacerMarcos = 30, puedeHacerVidrios = 50;
sem puedeTomarMarco = 0, puedeTomarVidrio = 0;
cola colaMarcos, colaVidrios, colaVentanas;

process EmpleadoCarpintero [id=1..4] {
  tipoMarco nuevoMarco;

  while (true) {
  	P(puedeHacerMarcos);
		nuevoMarco = realizarMarco();

    P(mutexColaMarcos);
    push(colaMarcos, marco);
    V(mutexColaMarcos);

  	V(puedeTomarMarco);
  }
}


process EmpleadoVidriero {
  tipoVidrio nuevoVidrio;

  while (true) {
  	P(puedeHacerVidrios);
    nuevoVidrio = realizarVidrio();

    P(mutexColaVidrios);
    push(colaVidrio, vidrio);
    V(mutexColaVidrios);

    V(puedeTomarVidrio);
  }
}

process EmpleadoArmador [id=1..2] {
  tipoVentana nuevaVentana;
  tipoMarco marco;
  tipoVidrio vidrio;

  while (true) {
  	P(puedeTomarMarco);
    P(mutexColaMarcos);
    pop(colaMarcos, marco);
    V(mutexColaMarcos);

    V(puedeHacerMarcos);

    P(puedeTomarVidrio);
    P(mutexColaVidrios);
    pop(colaVidrios, vidrio);
    P(mutexColaVidrios);

    V(puedeHacerVidrios);

    nuevaVentana = crearVentana(marco,vidrio);
    push(colaVentanas, nuevaVentana));
  }
}
