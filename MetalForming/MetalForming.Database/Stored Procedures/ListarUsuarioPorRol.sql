CREATE PROCEDURE dbo.ListarUsuarioPorRol
(
	@IdRol int
)
AS
BEGIN

SET NOCOUNT ON;

SELECT
U.Id,
U.IdRol,
U.Username,
U.NombreCompleto,
U.Password,
U.Estado,
ISNULL((SELECT COUNT(OV.Id) FROM OrdenVenta OV WHERE OV.IdAsistentePlaneamiento = U.Id), 0) AS CantidadOV,
ISNULL((SELECT COUNT(OP.Id) FROM OrdenProduccion OP WHERE OP.IdOperador = U.Id), 0) AS CantidadOP
FROM Usuario U
WHERE IdRol = @IdRol

END
