- Ejercicio 04

-- a) Versión 1: Modelando las cabinas como proceso

chan pedidos(int);
chan cabina_asignada[N](int);
chan cabinas[10](int);
chan facturas[N](tipoFactura);

process cliente [ id = 1 to N ] {
  int idCabina;
  tipoFactura factura;
  bool libre;

  send pedidos(id);
  recieve cabina_asignada[id](idCabina);
  send cabinas[idCabina](id);
  recieve cabina_libre[id](ibre);
  // utiliza la cabina
  send liberar(id);
  recieve facturas[id](factura);
}

process empleado {
  int idCliente;
  int idCabina;
  tipoFactura factura;

  while (true) {
    if (! empty(liberar)) {
      recieve liberar(idCliente);
      factura = Cobrar();
      facturas[idCliente](factura);
    } else if (! empty(pedidos)) {
      recieve pedidos(idCliente);
      idCabina = seleccionarCabina();
      send cabina_asignada[idCliente](idCabina);
    }
  }
}

process cabina [ id = 1 to 10 ] {
  int idCliente;

  while(true) {
    recieve cabinas[id](idCliente);
    send cabina_libre[idCliente](true);
  }
}

-- b) Versión 2: Modelando las cabinas como proceso

chan pedidos(int);
chan cabina_asignada[N](int);
chan liberar[10](int idCliente, int nroCabina);
chan facturas[N](tipoFactura);

process cliente [ id = 1 to N ] {
  int idCabina;
  tipoFactura factura;
  bool libre;

  send pedidos(id);
  recieve cabina_asignada[id](idCabina);
  // utiliza la cabina
  send liberar(id, idCabina);
  recieve facturas[id](factura);
}

process empleado {
  cola cabinas;
  int idCliente;
  int idCabina;
  int i;
  tipoFactura factura;
  for i = 1 to 10 push(cabinas, i);

  while (true) {
    if (! empty(liberar)) {
      recieve liberar(idCliente, idCabina);
      push(cabinas, idCabina);
      factura = Cobrar();
      facturas[idCliente](factura);
    }
    else if (! empty(pedidos) && (! empty(cabinas)) {
      idCabina = pop(cabinas, idCabina);
      recieve pedidos(idCliente);
      idCabina = seleccionarCabina();
      send cabina_asignada[idCliente](idCabina);
    }
  }
}
