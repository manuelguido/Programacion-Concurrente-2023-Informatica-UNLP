- Ejercicio 03

chan pedidosC(int), pedidosV(int);
chan pedidos_listos[C](tipoPedido);
chan cocinar_pedido(int);

process cliente [ id = 1 to C ] {
  tipoPedido pedido;

  send pedidosC(id);
  receive pedidos_listos[id](pedido);
}

process coordinador {
  int idCliente;
  int idVendedor;

  while(true) {
    recieve pedidosV(idVendedor);
    if empty(pedidosC) {
      send nuevos_pedidos[idVendedor](0);
    } else {
      recieve pedidosC(idCliente);
      send nuevos_pedidos[idVendedor](idCliente);
    }
  }
}

process cocinero [ id = 1 to 2 ] {
  int idCliente;
  tipoPedido pedido;

  while(true) {
    recieve cocinar_pedido(idCliente);
    pedido = cocinar_pedido();
    send pedidos_listos[idCliente](pedido);
  }
}

process vendedor [ id = 1 to 3 ] {
  int idCliente;
  int randomDelay;

  while(true) {
    send pedidosV(id);
    receive nuevos_pedidos[id](idCliente);
    if (idCliente == 0) {
      // reponer pack de bebidas
      randomDelay = random(60, 180);
      delay(randomDelay);
    } else {
      send cocinar_pedido(idCliente);
    }
  }
}
