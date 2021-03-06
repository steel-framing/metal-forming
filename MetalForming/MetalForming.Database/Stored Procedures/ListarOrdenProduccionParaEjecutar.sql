﻿CREATE PROCEDURE [dbo].[ListarOrdenProduccionParaEjecutar]
(
	@Estado1 varchar(50)
)
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
P.Descripcion AS DescripcionProducto,
OP.IdOperador,
U.Username AS UsernameOperador,
U.NombreCompleto AS NombreOperador
FROM dbo.OrdenProduccion OP
INNER JOIN dbo.OrdenVenta OV ON OP.IdOrdenVenta = OV.Id
INNER JOIN dbo.Producto P ON OV.IdProducto = P.Id
LEFT JOIN dbo.Usuario U ON OP.IdOperador = U.Id
WHERE OP.Estado = @Estado1

END
