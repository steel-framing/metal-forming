CREATE PROCEDURE [dbo].[ListarOrdenVentaParaAsignar]
(
	@IdPrograma int,
	@Estado1 varchar(50),
	@Estado2 varchar(50)
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
OV.IdAsistentePlaneamiento,
P.Descripcion AS DescripcionProducto,
AP.Username AS UsernameAsistentePlaneamiento,
AP.NombreCompleto AS NombreAsistentePlaneamiento
FROM dbo.OrdenVenta OV
INNER JOIN dbo.Producto P ON OV.IdProducto = P.Id
LEFT JOIN dbo.Usuario AP ON OV.IdAsistentePlaneamiento = AP.Id
WHERE OV.IdPrograma = @IdPrograma AND OV.Estado IN (@Estado1, @Estado2)

END
