# Ejercicio 01

## a) En algún caso el valor de x al terminar el programa es 56

##### Verdadero. Si los procesos se ejecutan en el orden de P1, P2, P3, el valor final de x es 56

## b) En algún caso el valor de x al terminar el programa es 22

##### Verdadero. Si se ejecutan las operaciones de la siguiente forma

##### P3:: (x*3) -> x sigue valiendo 0

##### P1:: proceso completo -> Ahora x vale 10

##### P3:: (x*2) -> x sigue valiendo 10

##### P3:: +1 -> x sigue valiendo 10

##### P3:: asignación de resultado de toda la operación del proceso -> Ahora x vale -> 21

##### P2:: completo -> Ahora x vale -> 22. (x valía 21 antes de ejecutar esta operación, pero el if  puede haber dado falso si se evaluaba al principio de todas las operaciones)

## c) En algún caso el valor de x al terminar el programa es 23

##### Probablemente sea verdadera, pero no encuentro camino de ejecución posible para que el valor final de x sea 23
