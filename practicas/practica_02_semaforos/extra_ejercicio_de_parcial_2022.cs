- Ejercicio de parcial 2022

Resolver con SEMÁFOROS el siguiente problema.
En una planta verificadora de vehículos, existen 7 estaciones donde se dirigen 150 vehículos
para ser verificados. Cuando un vehículo llega a la planta, el coordinador de la planta le indica
a qué estación debe dirigirse. El coordinador selecciona la estación que tenga menos vehículos
asignados en ese momento. Una vez que el vehículo sabe qué estación le fue asignada, se dirige a la misma
y espera a que lo llamen para verificar. Luego de la revisión, la estación le entrega un comprobante que indica
si pasó la revisión o no. Más allá del resultado, el vehículo se retira de la planta. Nota: maximizar la concurrencia.

sem mutexColaEspera = 1, mutexColaEstacion[7] = ([N] 0), hayVehiculos = 0, mutexEstaciones = 1;
sem esperaEstacion[150] = ([150] 0), esperaAtencion[150] = ([150] 0);
sem esperaComprobante[150] = ([150] 0);
sem hayVehiculoEnEstacion[7] = ([7], 0);
cola colaEspera, colaEstacion[7];
int totalEstaciones[7] = ([7] 0);
typeC comprobante[150];

process vehiculo[ id:1..150 ] {
  int estacion;
  typeC comprobante;

  P(mutexColaEspera);
  push(colaEspera, id);
  V(mutexColaEspera);
  V(hayVehiculos);
  P(esperaEstacion[id]);

  estacion = estacionParaVehiculo[id];

  P(mutexColaEstacion[estacion]);
  push(colaEstacion[estacion], id);
  V(mutexColaEstacion[estacion]);

  V(hayVehiculoEnEstacion[estacion]);

  P(esperaAtencion[id]);
  P(esperaComprobante[id]);
  comprobante = comprobantes[id];

  P(mutexCantEstacion);
  cantEstacion[auxEstacion]--;
  V(mutexCantEstacion);
}

process coordinador:: {
  int auxId;

  while(true) {
    P(hayVehiculos);

    P(mutexColaEspera);
    pop(colaEspera, auxId);
    V(mutexColaEspera);

    P(mutexCantEstacion);
    auxEstacion = buscarMinimo();
    cantEstacion[auxEstacion]++;
    V(mutexCantEstacion);

    estacionParaVehiculo[auxId] = auxEstacion;
    V(esperaEstacion[auxId]);
  }
}

process estacion [id 1..7] {
  int auxId;
  typeC comprobante;

  while(true) { 
    P(hayVehiculoEnEstacion[id]);

    P(mutexColaEstacion[id]);
    pop(colaEstacion[id], auxId);
    V(mutexColaEstacion[id]);

    V(esperaAtencion[auxId]);
    // hace la verificacion
    comprobante = GenerarComprobante();
    comprobantes[auxId] = comprobante;
    P(esperaComprobante[auxId]);
  }
}
