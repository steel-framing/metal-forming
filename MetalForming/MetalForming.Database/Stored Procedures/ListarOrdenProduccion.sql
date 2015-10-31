CREATE PROCEDURE [dbo].[ListarOrdenProduccion]
AS
BEGIN

SET NOCOUNT ON;

SELECT 
OP.Id,
OP.Numero,
OP.IdOrdenVenta,
OP.Estado,
OV.Numero AS NumeroOrdenVenta,
OV.FechaEntrega,
P.Descripcion AS DescripcionProducto
FROM dbo.OrdenProduccion OP
INNER JOIN dbo.OrdenVenta OV ON OP.IdOrdenVenta = OV.Id
INNER JOIN dbo.Producto P ON OV.IdProducto = P.Id

END

