Procedure Banco is
begin

	---------------------
	-- Header Emplado
	---------------------
  task Empleado is
    entry Atender();
  end

	---------------------
	-- Header Cliente
	---------------------
  task type Cliente;

  clientes: array(1..C) of Cliente;


  ---------------------
	-- Body Empleado
	---------------------
  task body Empleado is
    libre: boolean = true;
  begin
    loop
      accept Atender();
    end loop;
  end Cliente;

  ---------------------
	-- Body Cliente
	---------------------
  task body Cliente is
  begin
    select 
      Empleado.Atender();
    or delay(600); -- 600 segundos (10 minutos)
        null;
    end select;
  end Cliente;

Begin
	null;
End Banco;