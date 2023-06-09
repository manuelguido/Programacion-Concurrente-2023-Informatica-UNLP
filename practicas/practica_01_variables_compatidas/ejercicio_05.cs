- Ejercicio 05

-- a)

usando_impresora = 0;

Process Persona[ i = 1 .. N] {
  while(true) {
    <await (usando_impresora < 1); usando_impresora++>;
    Imprimir(documento);
    <usando_impresora-->;
  }
}

-- b)

int siguiente = -1;
int cola_espera[N];

Process Persona[ id = 1 .. N] {
  while(true) {
    <if (siguiente == -1);
      siguiente = id;
    else
      cola_espera.push(id);>
    <await siguiente == id>;
    Imprimir(documento);
    <if cola_espera.not_empty();
      sig = cola_espera.pop();
    else
      siguiente = -1;>
  }
}

-- c)

int siguiente = -1;
colaEspecial cola_espera[N];

Process Persona[ id = 0 .. N - 1 ] {
  <if (cola_espera.empty())
    siguiente = id
  else
    cola_espera.push(id)>;
  <await siguiente == id>;
  Imprimir(documento);
  <if (cola_espera.empty())
    siguiente = -1;
  else
    siguiente = cola_espera.minimo();>
}

-- d)

int siguiente = 1;

Process Persona[ id = 0 .. N-1 ] {
  <await siguiente == id>;
  Imprimir(documento);
  siguiente++;
}

-- e)

int siguiente = -1;
bool impresora_libre = true;
cola cola_espera[N];

Process Persona[ id = 1 .. N] {
  <cola_espera.push(id)>
  <await (siguiente == id)>
  Imprimir(documento);
  <impresora_libre = true>
}

Process Coordinador:: {
  for i = 1..N { // esto podría ser un while (true) también
    <await (impresora_libre)>
    <await (not cola_espera.is_empty())
    siguiente = cola_espera.pop_min(); // se espera que se elimina el valor de la cola de espera
    impresora_libre = false;>
  }
}
