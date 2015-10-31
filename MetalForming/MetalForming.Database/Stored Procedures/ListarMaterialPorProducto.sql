CREATE PROCEDURE [dbo].[ListarMaterialPorProducto]
(
	@IdProducto int
)
AS
BEGIN

SET NOCOUNT ON;

SELECT 
M.Id,
M.Descripcion,
M.Stock,
M.StockMinimo,
M.Reservado,
PM.Cantidad
FROM dbo.ProductoMaterial AS PM
INNER JOIN dbo.Material AS M ON PM.IdMaterial = M.Id
WHERE PM.IdProducto = @IdProducto

END

