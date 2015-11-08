CREATE PROCEDURE [dbo].[ActualizarProgramaOrdenVenta]
(
	@Id int,
	@IdPrograma int,
	@Estado varchar(50)
)
AS
BEGIN

SET NOCOUNT ON;

UPDATE OrdenVenta
SET IdPrograma = @IdPrograma,
	Estado = CASE WHEN Id NOT IN (SELECT IdOrdenVenta FROM OrdenProduccion) THEN @Estado ELSE Estado END
WHERE Id = @Id;

END
