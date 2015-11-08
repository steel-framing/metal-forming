CREATE PROCEDURE [dbo].[ActualizarProgramasHaciaNullOrdenVenta]
(
	@IdPrograma int,
	@Estado varchar(50)
)
AS
BEGIN

SET NOCOUNT ON;

UPDATE OrdenVenta
SET IdPrograma = NULL,
	Estado = @Estado
WHERE IdPrograma = @IdPrograma;

END
