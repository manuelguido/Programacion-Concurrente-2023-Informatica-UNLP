- Ejercicio 03

-- a)

Process Productor::
{
  while(true) {
    // genera elemento
    <await (cant < N)>;
    buffer[pri_vacia] = elemento;
    pri_vacia = pri_vacia++;
    <cant++>;
  }
}

Process Consumidor::
{
  while(true) {
    <await (cant > 0)>;
    elemento = buffer[pri_ocupada];
    pri_ocupada = pri_llena++;
    <cant-->;
    // consume elemento
  }
}

-- b)

int cant = 0; int pri_vacia = 0; int pri_ocupada = 0;
int buffer[N];

Process Productor::[ i=1 to P ]
{
  while(true) {
    // genera elemento
    <await (cant < N)
    buffer[pri_vacia] = elemento;
    pri_vacia = pri_vacia++;
    cant++>;
  }
}

Process Consumidor::[ i=1 to C ]
{
  while(true) {
    <await (cant > 0)
    elemento = buffer[pri_ocupada];
    pri_ocupada = pri_llena++;
    cant-->;
    // consume elemento
  }
}
