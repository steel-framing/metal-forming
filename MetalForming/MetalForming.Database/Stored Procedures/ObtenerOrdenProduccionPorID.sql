CREATE PROCEDURE [dbo].[ObtenerOrdenProduccionPorID]
(
	@Id int
)
AS
BEGIN

SET NOCOUNT ON;

SELECT 
OP.Id,
OP.Numero,
OP.IdOrdenVenta,
OP.Estado,
OP.CantidadProducto,
OV.Numero AS NumeroOrdenVenta,
OV.Cliente,
OV.FechaEntrega,
OV.Cantidad AS CantidadOrdenVenta,
OV.IdProducto,
P.Descripcion AS DescripcionProducto
FROM dbo.OrdenProduccion OP
INNER JOIN dbo.OrdenVenta OV ON OP.IdOrdenVenta = OV.Id
INNER JOIN dbo.Producto P ON OV.IdProducto = P.Id
WHERE OP.Id = @Id

END
