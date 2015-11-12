CREATE PROCEDURE dbo.ActualizarOrdenVentaPorAsignacion
(
	@Id int,
	@IdAsistentePlaneamiento int,
	@Estado varchar(50)
)
AS
BEGIN

SET NOCOUNT ON;

UPDATE OrdenVenta
SET Estado = @Estado,
	IdAsistentePlaneamiento = @IdAsistentePlaneamiento
WHERE Id = @Id;


END
