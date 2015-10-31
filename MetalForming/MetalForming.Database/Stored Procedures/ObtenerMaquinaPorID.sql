CREATE PROCEDURE [dbo].[ObtenerMaquinaPorID]
(
	@Id int
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
Estado,
ReduacionInicio,
ReduacionFin,
CantidadRodillos,
MaximoFrio,
MaximoCaliente,
PorcentajeFalla,
Tiempo
FROM dbo.Maquina 
WHERE Id = @Id

END

