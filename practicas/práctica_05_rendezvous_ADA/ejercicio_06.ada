Procedure Playa is

  Task Type Persona is
    entry recibirMiId(id: IN integer);
    entry conocerGanador(idEquipoGanador: IN integer);
  end;

  personas: array(1..20) of Persona;

  Task Type Equipo is
    entry recibirMiId(id: IN integer);
    entry sumar(nuevoValor: IN integer);
  end;

  equipos: array(1..5) of Equipo;

  Task Organizador is 
    entry llegue(idPersona: IN integer, idEquipo: OUT integer);
    entry puntajeDeEquipo(idEquipo: IN integer; puntaje IN OUT integer);
  end;

  ---------------------
	-- Persona
	---------------------
  Task Body Persona is
    id: integer;
    idEquipo: integer;
    idEquipoGanador: integer;
    miTotal: integer = 0;
  Begin
    accept recibirMiId(valorId: IN integer) do
      id := valorId;
    end obtenerId;
    Organizador.llegue(id, idEquipo);
    accept comenzar();

    for i in 1 .. 15 loop
      miTotal += moneda();
    end loop;

    equipos(idEquipo).sumar(miTotal);

    accept conocerGanador(idEquipoGanador);
  End Persona;

  ---------------------
	-- Equipo
	---------------------
  Task Body Equipo is
    id: integer;
    total: integer = 0;
    misIntegrantes: array(1..4) of integer;
  Begin
    accept recibirMiId(valorId: IN integer) do
      id := valorId;
    end obtenerId;

    -- Agrego mis integrantes al listado
    while (misIntegrantes.length < 4) loop
      accept anotarIntegrante(nuevoId) do
        misIntegrantes.push(nuevoId);
      end anotarIntegrante;
    end loop;

    for i in 1 to 4 loop
      misIntegrantes(i).comenzar();
    end loop;

    -- Recorro 4 veces para sumar los totales
    for i in 1 .. 4 loop
      accept sumar(nuevoValor) do
        total += nuevoValor;
      end sumar();
    end loop;

    Organizador.totalDeEquipo(total);
  End Equipo;

  ---------------------
	-- Organizador
	---------------------
  Task Body Organizador is
    proximoEquipo: integer = 1;
    puntajeMasAlto: integer = -1;
    idEquipoGanador: integer;
  Begin
    -- Asignación de equipos
    for i in 1..5 loop
      for j in 1..4  loop
        accept llegue(idPersona, equipo) do
          equipo = proximoEquipo;
          equipos(proximoEquipo).anotarIntegrante(idPersona);
        end llegue;
      end loop;
      proximoEquipo +=1;
    end loop;

    -- Resolver puntajes
    for i in 1..5 loop
      accept puntajeDeEquipo(idEquipo, puntaje) do
        if (puntaje > puntajeMasAlto) do
          puntajeMasAlto = puntaje;
          idEquipoGanador = idEquipo;
        end if;
      end puntajeDeEquipo;
    end loop;

    -- Avisar ganadores
    for i in 1..20 loop
      personas(i).conocerGanador(idEquipoGanador);
    end loop;

  End Organizador;

Begin
  for i in 1..20 loop
    personas(i).recibirMiId(i); 
  end loop;

  for i in 1..5 loop
    equipos(i).recibirMiId(i); 
  end loop;
End Playa;
