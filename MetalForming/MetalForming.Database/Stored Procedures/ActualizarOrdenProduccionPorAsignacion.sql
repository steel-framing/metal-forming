
CREATE PROCEDURE dbo.ActualizarOrdenProduccionPorAsignacion
(
	@Id int,
	@IdOperador int,
	@Estado varchar(50)
)
AS
BEGIN

SET NOCOUNT ON;

UPDATE OrdenProduccion
SET Estado = @Estado,
	IdOperador = @IdOperador
WHERE Id = @Id;


END
