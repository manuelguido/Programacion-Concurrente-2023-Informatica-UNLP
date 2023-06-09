# Ejercicio 01

##### 1) Exclusión mutua: cumple dado que nunca va a haber más de un proceso ejecutando su sección crítica a la vez.

##### 2) Ausencia de deadlock: cumple dado que si 2 o más procesos intentan acceder a su sección crítica, al menos uno tendrá éxito. En este caso el proceso 1 es el que siempre tendrá éxito y el proceso 2 nunca ingresará a su sección crítica.

##### 3) Ausencia de demora innecesaria: cumple dado que ningún proceso bloquea al otro de ingresar a su SC cuando el proceso bloqueante está en su SNC.

##### 4) Eventual entrada: no cumple, dado que el proceso 2 nunca podrá ingresar a su SC, debido a que la variable turno nunca tendrá el valor 2.
