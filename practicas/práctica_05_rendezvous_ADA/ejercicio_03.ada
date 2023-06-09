Procedure Sistema is
begin

	---------------------
	-- Header Central
	---------------------
  task Central is
    entry enviarSeñal1;
    entry enviarSeñal2;
    entry finConteo;
  end Central;

  ---------------------
	-- Header Counter
	---------------------
  task Contador is
    entry iniciarConteo;
  end Contador;

	---------------------
	-- Header Procesos
	---------------------
  task  Proceso1;
  task  Proceso2;

  ---------------------
	-- Body Central
	---------------------
  task body Central is
    loopEnSeñal2: boolean = false;
    auxSeñal: string;
  begin
    accept Proceso1(señal); do
      auxSeñal := señal;
    end Proceso1;

    loop
      select
        accept Proceso1(señal) do
          auxSeñal := señal;
        end Proceso1;
      or
        accept Proceso2(señal) do
          auxSeñal := señal;
          loopEnSeñal2 := true;
          Contador.iniciarConteo();
        end Proceso1;
      end select;

      while (loopEnSeñal2) loop
        select
          when (finConteo'count = 0) accept Proceso2(señal) do
            auxSeñal:= señal;
          end Proceso2;
        or
          accept finConteo() do
            loopEnSeñal2 := false;
          end finConteo();
        end select;
      end loop;

    end loop;
  end Central;

  ---------------------
	-- Body Contador
	---------------------
  task body Contador is
  begin
    loop
      accept iniciarConteo;
      delay(180);
      Central.finConteo();
    end loop;
  end Contador;

  ---------------------
	-- Body Proceso1
	---------------------
  task body Proceso1 is
    señal: string;
  begin
    loop
      señal:= generarSeñal();
      select
        Central.enviarSeñal1(señal);
      or delay(120);
      end select;
    end loop;
  end Proceso1;

  ---------------------
	-- Body Proceso2  
	---------------------
  task body Proceso2 is
    señal: string;
    puedoGenerar: boolean = true;
  begin
    loop
      if (puedoGenerar) señal:= generarSeñal();
      select
        Central.enviarSeñal2(señal);
        puedoGenerar := true;
      or delay(60)
        puedoGenerar := false;
    end loop;
  end Proceso2;

Begin
	null;
End Sistema;