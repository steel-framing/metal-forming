
CREATE PROCEDURE [dbo].[ObtenerOrdenVentaPorNumero]
(
	@Numero varchar(50)
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
OV.IdProducto,
P.Descripcion AS DescripcionProducto,
P.Stock AS StockProducto,
P.StockMinimo AS StockMinimoProducto
FROM dbo.OrdenVenta OV
INNER JOIN dbo.Producto P ON OV.IdProducto = P.Id
WHERE OV.Numero = @Numero

END

