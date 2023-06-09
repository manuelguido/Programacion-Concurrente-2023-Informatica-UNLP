- Ejercicio 05

chan imprimir_usuario(tipoDocumento);
chan imprimir_director(tipoDocumento);
chan pedido(int);
chan documento_a_imprimir[3](tipoDocumento);

process usuario [ id = 1 to N ] {
  tipoDocumento documento;

  while (true) {
    trabajar();
    documento = generarDocumento;
    send imprimir_usuario(documento);
  }
}

 process director {
  tipoDocumento documento;

  while (true) {
    trabajar();
    documento = generarDocumento;
    send imprimir_director(documento);
  }
}

process coordinador {
  int idImpresora;

  while(true) {
    recive pedido(idImpresora);
    if (! empty(imprimir_director) {
      recieve imprimir_director(documento);
    }
    else if (! empty(imprimir_usuario) {
      recieve imprimir_usuario(documento);
    }
  }
}

process impresora [ id = 1 to 3 ] {
  tipoDocumento documento;

  while(true) {
    send pedido(id);
    recieve documento_a_imprimir[id](documento);
    imprimir(documento);
  }
}
