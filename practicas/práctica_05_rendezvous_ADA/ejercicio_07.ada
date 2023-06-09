Procedure Sistema is

  Task Type Servidor is
    entry obtenerId(id: IN integer);
    entry enviarTest(nuevoTest: IN Huella);
  end;

  servidores: array(1..8) of Servidor;

  Task Type BaseDeDatos is
    entry obtenerId(id: IN integer);
    entry Buscar(text: IN Huella; codigo: OUT integer; valor: IN integer);
  end;

  bases_de_datos: array(1..8) of BaseDeDatos;

  Task Especialista;

  ---------------------
	-- Body Servidor
	---------------------
  Task Body Servidor is
    id: integer;
    auxTest: Huella;
    auxCodigo: integer;
    auxValor: integer;
  Begin
    accept obtenerId(valorId: IN integer) do
      id := valorId;
    end obtenerId;

    loop
      accept enviarTest(nuevoTest: IN Huella) do
        auxTest := nuevoTest;
      end enviarHuella;

      bases_de_datos.(id).Buscar(auxTest, auxCodigo, auxValor);

      Especialista.darResultados(auxCodigo, auxValor);
    end loop;
  End Servidor;

  ---------------------
	-- Body Base de Datos
	---------------------
  Task Body BaseDeDatos is
    id: integer;
  Begin
    accept obtenerId(valorId: IN integer) do
      id := valorId;
    end obtenerId;

    loop
      accept Buscar(test: IN Huella, codigo: OUT integer, valor: OUT integer) do
        -- Aca la BD actualiza obtiene valores y actualiza los parámetros codigo y valor
      end Buscar;
    end loop;
  End BaseDeDatos;

  ---------------------
	-- Body Especialista
	---------------------
  Task Body Especialista is
    test: Huella;
    auxValor: integer;
    auxCodigo: integer;
    maxValor: integer;
    maxCodigo: integer;
  Begin
    loop
      maxValor := -1;
      maxCodigo := -1;
      tomarImagen(test);

      for i in 1..8 loop
        servidores(i).enviarTest(test);
      end loop;

      for i in 1..8 loop
        accept darResultados(codigo: IN integer, valor: IN integer) do
          auxCodigo := codigo;
          auxValor := valor;
        end darResultados;

        if (auxValor > maxValor) then
          maxValor := auxValor;
          maxCodigo := auxCodigo;
        end if;
      end loop;

    end loop;
  End Especialista;

Begin
  for i in 1..8 loop
    servidores(i).obtenerId(i);
    bases_de_datos(i).obtenerId(i);
  end loop;
End Sistema;