Procedure Sistema is

  Task Servidor is
    recibirDocumento(documento: IN string; debeCorregir: OUT boolean)
  end;

  Task Type Usuario;

  usuarios: array(1..U) of Usuario;

  ---------------------
	-- Servidor
	---------------------
  Task Body Servidor is
  Begin
    loop
      select
        accept recibirDocumento(documento, corregir) documento
          corregir := corregirDocumento(documento);
        end recibirDocumento;
      end select;
    end loop;
  End Servidor;

  ---------------------
	-- Usuario
	---------------------
  Task Body Usuario is
    documento: string;
    deboCorregir: boolean := true;
    puedoTrabajarEnDoc: boolean := true;
  Begin
    while (deboCorregir) loop
      if (puedoTrabajarEnDoc) then
        documento := trabajarEnDocumento(documento);
      end if;

      select
        Servidor.recibirDocumento(documento, deboCorregir);
        puedoTrabajarEnDoc:= true;
      or delay(120)
        delay(60);
        select 
          Servidor.recibirDocumento(documento, deboCorregir);
          puedoTrabajarEnDoc:= true;
        else
          puedoTrabajarEnDoc:= false;
        end select;
      end select;
    end loop;
  End Usuario;

Begin
  null;
End Sistema;
