Process EmpesaDeCamiones is

  Task Type Camion end;

  camiones: array(1..3) of Camion;

  Task Type Persona is
    entry recibirCamion();
  end;

  Task Type Empresa is
    entry reclamo(idPersona: IN integer);
    entry camionLibre(idPersona: OUT integer);
  end;

  personas: array(1..P) of Persona;

  ---------------------
	-- Body Persona
	---------------------
  Task Body Persona is
    id: integer;
    atendido: boolean = false;
  Begin
    recieve obtenerId(idPersona: IN integer);
      id := idPersona;
    end obtenerId

    while (not atendido) loop
      Empresa.reclamo(id);

      select
        Empresa.recibirCamion();
        atendido := true;
      or delay(900) -- 15 minutos
        null;
      end select;
    end loop;
  End Persona;

  ---------------------
	-- Body Camion
	---------------------
  Task Body Camion is
    idPersona: integer;
  Begin
    loop
      Empresa.estoyLibre(idPersona);

      personas(idPersona).recibirCamion();
    end loop;
  end Camion;

  ---------------------
	-- Body Empresa
	---------------------
  Task Body Empresa is
    reclamos: array(1..P) of integer;
    despachados: array(1..P) of boolean;
    auxIdPersona: integer;
  Begin
    for i in 1 to p loop
      reclamos(i) := 0;
    end loop;

    loop
      select 
        when (camionLibre'count > 0) => accept camionLibre(idPersona: OUT integer);
          idPersona := maxIndex(reclamos);
          auxIdPersona := idPersona;
        end camionLibre();
        reclamos(auxIdPersona) := 0;
        despachados(auxIdPersona) := true;
      or
        accept tomarReclamo(idPersona: IN integer);
          auxIdPersona := idPersona;
        end tomarReclamo;
        if(despachados(auxIdPersona))
          reclamos(idPersona) += 1;
        end if;
      end select;
    end loop;
  End Empresa;

Begin
  for i in 1..P loop
    personas(i).obtenerId(i);
  end loop;
  for i in 1..3 loop
    camiones(i).obtenerId(i);
  end loop;
End EmpesaDeCamiones;