- Ejercicio 04

tipo_impresora cola_impresoras[5];
int usando_imp = 0;

Process UtilizarImpresora::[ i=0 to N-1 ] {
  tipo_impresora impresora;
  while(true) {
    // genera documento para imprimir
    <await (usando_imp < 5); usando_imp++; >;
    impresora = cola_impresoras.pop();
    impresora.imprimir();
    <cola_impresoras.push(impresora); usando_imp-–;>
  }
}
