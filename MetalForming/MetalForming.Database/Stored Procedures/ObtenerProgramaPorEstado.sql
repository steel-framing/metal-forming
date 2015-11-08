CREATE PROCEDURE dbo.ObtenerProgramaPorEstado
(
	@Estado varchar(50)
)
AS
BEGIN

SET NOCOUNT ON;

SELECT
TOP 1
Id,
Numero,
FechaInicio,
FechaFin,
CantidadOV,
Estado,
IdPlan
FROM Programa
WHERE Estado = @Estado
ORDER BY Id DESC

END
