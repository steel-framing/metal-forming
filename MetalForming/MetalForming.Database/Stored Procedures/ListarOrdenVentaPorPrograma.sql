CREATE PROCEDURE [dbo].[ListarOrdenVentaPorPrograma]
(
	@IdPrograma int
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
P.Descripcion AS DescripcionProducto
FROM dbo.OrdenVenta OV
INNER JOIN dbo.Producto P ON OV.IdProducto = P.Id
WHERE IdPrograma = @IdPrograma

END
