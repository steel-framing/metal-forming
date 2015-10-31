CREATE PROCEDURE [dbo].[ListarMaquinaPorBusqueda]
(
	@Descripcion varchar(50),
	@Tipo varchar(50),
	@PLD varchar(50),
	@Configuracion varchar(50)
)
AS
BEGIN

SET NOCOUNT ON;

SELECT 
Id,
Descripcion,
Tipo,
PLD,
Configuracion,
PorcentajeFalla,
Tiempo
FROM dbo.Maquina 
WHERE Descripcion LIKE '%' + @Descripcion + '%' 
	AND Tipo LIKE '%' + @Tipo + '%' 
	AND PLD LIKE '%' + @PLD + '%' 
	AND Configuracion LIKE '%' + @Configuracion + '%' 

END

