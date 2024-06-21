## Clase 1

### Introducción

- Concurrencia: es la capacidad de ejecutar múltiples actividades en paralelo o simultáneamente.

- Objetivos de los sistemas concurrentes:
    - Ajustar el modelo de arquitectura de hardware y software al problema del mundo real a resolver.
    - Incrementar la performance mejorando los tiempos de respuesta de los sistemas de cómputo, a través de un enfoque diferente de la arquitectura física y lógica de las soluciones.

### Conceptos básicos de la concurrencia

- Comunicación entre procesos
    - Memoria compartida
        - Los procesos intercambian información sobre la memoria compartida o actúan coordinadamente sobre datos residentes en ella.
        - No pueden operar simultáneamente sobre la memoria compartida, lo que obliga a bloquear y liberar el acceso a la memoria.
    - Pasaje de mensajes
        - Es necesario establecer un canal (lógico o físico) para transmitir información entre procesos.
        - El lenguaje debe proveer un protocolo adecuado.
        - Para que la comunicación sea efectiva los procesos deben saber cuándo tienen mensajes para leer y cuando deben trasmitir mensajes.

- Sincronización: es la posesión de información acerca de otro proceso para coordinar actividades. Los procesos se sincronizan por
    - Exclusión mutua
    - Por condición

- Interferencia: un proceso toma una acción que invalida las suposiciones hechas por otros procesos.

- Prioridad: un proceso con mayor prioridad puede causar la suspención de otros procesos.

- La granularidad de una aplicación está dada por la relación entre el cómputo y la comunicación.

- Fairness: es el equilibrio en el acceso a recursos compartidos por todos los procesos.

- Inanición (propiedad no deseada): es cuando un proceso no logra acceder a los recursos compartidos.

- Overloading (propiedad no deseada): es cuando la carga asignada excede su capacidad de procesamiento.

- Deadlock (propiedad no deseada): es cuando un proceso está bloqueado por la espera de otro proceso. Dos o más procesos pueden entrar en deadlock si por error de programación ambos se quedan esperando que el otro libere un recurso compartido. La AUSENCIA de deadlock es una propiedad necesaria en todos los procesos concurrentes.

    - 4 propiedades necesarias y suficientes para que exista deadlock:
        - Recursos reusables serialmente: los procesos comparten recursos que pueden usar con exclusión mutua.
        - Adquisición incremental: los procesos mantienen los recursos que poseen mientras esperan adquirir recursos adicionales.
        - No-preemption: una vez que son adquiridos por un proceso, los recursos no pueden quitarse de manera forzada por otro proceso, sino que sólo son liberados voluntariamente.
        - Espera cíclica: existe una cadena circular de procesos tal que cada uno tiene un recurso que su sucesor en el ciclo está esperando adquirir.

- Requerimientos de un lenguaje de programación concurrente:
    - Indicar las tareas o procesos que pueden ejecutarse concurrentemente.
    - Mecanismos de sincronización.
    - Mecanismos de comunicación entre los procesos.

- Concurrencia vs paralelismo:
    - Concurrencia: es un concepto de software no restringido a una arquitectura particular de hardware ni a un número determinado de procesadores. Especificar la concurrencia implica especificar los procesos concurrentes, su comunicación y su sincronización.
    - Paralelismo: Se asocia con la ejecución concurrente en múltiples procesadores con el objetivo principal de reducir el tiempo de ejecución.
