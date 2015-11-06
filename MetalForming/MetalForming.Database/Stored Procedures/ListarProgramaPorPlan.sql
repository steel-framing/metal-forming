CREATE PROCEDURE dbo.ListarProgramaPorPlan
(
	@IdPlan int
)
AS
BEGIN

SET NOCOUNT ON;

SELECT
Id,
Numero,
FechaInicio,
FechaFin,
CantidadOV,
Estado,
IdPlan
FROM Programa
WHERE IdPlan = @IdPlan
ORDER BY Id DESC

END
