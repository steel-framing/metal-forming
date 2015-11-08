CREATE PROCEDURE dbo.ObtenerProgramaPorID
(
	@Id int
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
WHERE Id = @Id
ORDER BY Id DESC

END
