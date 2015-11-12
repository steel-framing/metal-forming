CREATE PROCEDURE [dbo].[ActualizarProgramasHaciaNullOrdenVenta]
(
	@IdPrograma int,
	@Estado varchar(50)
)
AS
BEGIN

SET NOCOUNT ON;

UPDATE OrdenVenta
SET IdPrograma = CASE WHEN Id NOT IN (SELECT IdOrdenVenta FROM OrdenProduccion) THEN NULL ELSE IdPrograma END,
	Estado = CASE WHEN Id NOT IN (SELECT IdOrdenVenta FROM OrdenProduccion) THEN @Estado ELSE Estado END
WHERE IdPrograma = @IdPrograma;

END
