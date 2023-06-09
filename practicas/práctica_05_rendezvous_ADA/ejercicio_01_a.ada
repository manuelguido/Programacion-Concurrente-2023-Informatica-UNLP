Procedure PuenteEjercicio1A is
begin

	---------------------
	-- Header Puente
	---------------------
	Task Puente is
		entry EntrarAuto();
		entry EntrarCamioneta();
		entry EntrarCamion();
		entry SalirAuto();
		entry SalirCamioneta();
		entry SalirCamion();
	End Puente;

	---------------------
	-- Headers Vehíiculos
	---------------------
	Task type Auto;
	Task type Camioneta;
	Task Camion;

	---------------------
	-- Variables de vehículos
	---------------------
	autos: array(1..A) of Auto;
	camionetas: array(1..B) of Camioneta;
	camiones: array(1..C) of Camion;

	---------------------
	-- Body Puente
	---------------------
	Task Body Puente is
		total: integer = 0;
	Begin
		loop
			select
				when (total < 5) =>
					accept EntrarAuto() is
						total := total + 1;
					end EntrarAuto;
			or
				when (total < 4) =>
					accept EntrarCamioneta() is
						total := total + 2;
					end EntrarCamioneta;
			or
				when (total < 3) =>
					accept EntrarCamioneta() is
						total := total + 3;
					end EntrarCamioneta;
			or
				accept SalirAuto() is
					total := total - 1;
				end SalirAuto;
			or
				accept SalirCamioneta() is
					total := total - 2;
				end SalirCamioneta;
			or
				accept SalirCamion() is
					total := total - 3;
				end SalirCamion;
			end select;
		end loop;
	End Puente;

  ---------------------
  -- Body Auto
  ---------------------
	Task Body Auto is
	Begin
		Puente.EntrarAuto();
		-- Cruza por el puente
		Puente.SalirAuto();
	End Auto;

  ---------------------
  -- Body Camioneta
  ---------------------
	Task Body Camioneta is
	Begin
		Puente.EntrarCamioneta();
		-- Cruza por el puente
		Puente.SalirCamion();
	End Camioneta;

  ---------------------
	-- Body Camion
	---------------------
	Task Body Camion is
	Begin
		Puente.EntrarCamion();
		-- Cruza por el puente
		Puente.SalirCamioneta();
	End Camion;
	
Begin
	null;
End PuenteEjercicio1A;