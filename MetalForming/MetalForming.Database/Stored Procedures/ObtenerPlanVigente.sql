CREATE PROCEDURE dbo.ObtenerPlanVigente
AS
BEGIN

SET NOCOUNT ON;

SELECT 
TOP 1
Id,
Codigo,
FechaInicio,
FechaFin,
Estado
FROM [Plan]
ORDER BY Id DESC

END
