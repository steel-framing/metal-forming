CREATE PROCEDURE [dbo].[ListarOrdenProduccionMaterial]
(
	@IdOrdenProduccion int
)
AS
BEGIN

SET NOCOUNT ON;

SELECT 
OP.Id,
OP.IdOrdenProduccion,
OP.IdMaterial,
OP.Requerido,
OP.Comprar,
M.Descripcion,
M.Stock,
M.StockMinimo,
M.Reservado
FROM dbo.OrdenProduccionMaterial OP
INNER JOIN dbo.Material M ON OP.IdMaterial = M.Id
WHERE OP.IdOrdenProduccion = @IdOrdenProduccion

END

