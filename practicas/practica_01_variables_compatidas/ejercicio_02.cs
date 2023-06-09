- Ejercicio 02

-- Solución 01

Process Verificar::
{
  for[i:1 to M] {
    if (arreglo[M] = N) cant++;
  }
}

-- Solución 02

Process Verificar::[ i=1 to M ]
{
  if (arreglo[i - 1] = N) <cant++>; // El i -1 es porque el arreglo comienza en 0.
}
