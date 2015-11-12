CREATE PROCEDURE [dbo].[ObtenerOrdenVentaPorID]
(
	@Id int
)
AS
BEGIN

SET NOCOUNT ON;

SELECT 
OV.Id,
OV.Numero,
OV.Cliente,
OV.FechaEntrega,
OV.Estado,
OV.Cantidad,
OV.IdPrograma,
OV.IdProducto,
P.Descripcion AS DescripcionProducto,
P.Stock AS StockProducto,
P.StockMinimo AS StockMinimoProducto
FROM dbo.OrdenVenta OV
INNER JOIN dbo.Producto P ON OV.IdProducto = P.Id
WHERE OV.Id = @Id

END