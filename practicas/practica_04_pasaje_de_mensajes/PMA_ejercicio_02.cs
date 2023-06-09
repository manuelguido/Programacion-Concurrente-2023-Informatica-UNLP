- Ejercicio 02

chan solicitar_id_caja(int);
chan recibir_id_caja[P](int);
chan solicitar_atencion[5](int);
chan recibir_atencion[5](int);
chan terminar_atencion(int);
chan comprobantes[P](tipoComprobante);

process cliente [ id = 1 to P ] {
  int idCaja;
  tipoComprobante comprobante;

  send solicitar_id_caja(id);
  receive recibir_id_caja[id](idCaja);
  send solicitar_atencion[idCaja](id);
  receive recibir_atencion[idCaja](id);
  // ser atendido
  recieve comprobantes[id];
  send terminar_atencion(idCaja);
}

process caja [ id = 1 to 5 ] {
  int idCliente;
  tipoComprobante comprobante;

  while(true) {
    recieve solicitar_atencion[id](idCliente);
    send recibir_atencion[id](idCliente);
    // atiente al cliente
    comprobante = generarComprobante();
    comprobantes[idCliente](comprobante);
  }
}

process coordinador {
  int idCliente, idCaja;
  int totalCaja[5] = ([5] 0);
  int minCaja, minIdCaja;

  while(true) {
    if (! empty(recibir_id_caja) && empty(terminar_atencion) {
      recieve solicitar_id_caja(idCliente);
      idCaja = obtener_caja_de_menor_espera();
      totalCaja[idCaja]++;
      send recibir_id_caja[idCliente](idCaja);
    }
    else {
      recieve terminar_atencion(idCaja);
      totalCaja[idCaja]--;
    }
  }
}
