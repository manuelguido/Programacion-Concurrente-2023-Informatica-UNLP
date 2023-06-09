Procedure Clinica is

  Task Medico is
    peticionDePersona();
    peticionDeEnfermera();
  end;

  Task Escritorio is
    recibirNota(Nota: IN string);
    entregarNota(Nota: IN string);
  end;

  Task Type Enfermera;

  Task Type Persona;

  enfermeras: array(1..E) of Enfermera;
  personas: array(1..P) of Persona;

  ---------------------
	-- Medico
	---------------------
  Task Body Medico is
    auxNota: string;
  Begin
    loop
      select
        accept peticionDePersona() do
          -- Atender a la persona
        end peticionDePersona;
        
        or when (peticionDePersona'count = 0) => accept peticionDeEnfermera() do
          -- Atender la peticion de la enfermera
        end peticionDeEnfermera();

        or when (peticionDePersona'count = 0) => accept recibirNota(nota) do
          auxNota:= nota;
        end recibirNota();

        if (auxNota != '') then
          auxNota := firmarNota(auxNota);
        endif;
      end select;

    end loop;
  End Medico;

  ---------------------
	-- Escritorio
	---------------------
  Task Body Escritorio is
    notas: queue of string;
  Begin
    loop
      select
        accept recibirNota(nuevaNota) do
          notas.push(nuevaNota);
        end recibirNota;
      or
        accept entregarNota(nota) do;
          nuevaNota = notas.pop();
        end entregarNota;
    end loop;
  End Medico;

  ---------------------
	-- Enfermera
	---------------------
  Task Body Enfermera is
    auxNota: string;
  Begin
    loop
      select
        Medico.peticionDeEnfermera();
      else
        auxNota := generarNota();
        accept Escritorio.nuevaNota(auxNota);
      end select;
    end loop;
  End Enfermera;

  ---------------------
	-- Persona
	---------------------
  Task Body Persona is
    fuiAtendido: boolean = false;
    tiempoAEsperar: integer = 300;
    misIntentos: integer = 0;
  Begin
    while ((misIntentos < 3) and (not fuiAtendido)) loop
      select 
        Medico.peticionDePersona();
        fuiAtendido:= true;
      or delay(tiempoAEsperar)
        misIntentos += 1;
        tiempoAEsperar:= 600;
      end select;
    end loop;
  End Persona;

Begin
  null;
End Clinica;