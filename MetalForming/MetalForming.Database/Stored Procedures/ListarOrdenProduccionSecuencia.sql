CREATE PROCEDURE [dbo].[ListarOrdenProduccionSecuencia]
(
	@IdOrdenProduccion int
)
AS
BEGIN

SET NOCOUNT ON;

SELECT 
OP.Id,
OP.Secuencia,
OP.IdMaquina,
OP.FechaInicio,
OP.FechaFin,
M.Descripcion,
M.PorcentajeFalla,
M.Tiempo
FROM dbo.OrdenProduccionSecuencia OP
INNER JOIN dbo.Maquina M ON OP.IdMaquina = M.Id
WHERE OP.IdOrdenProduccion = @IdOrdenProduccion

END

