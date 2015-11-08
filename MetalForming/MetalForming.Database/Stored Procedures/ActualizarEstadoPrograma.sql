CREATE PROCEDURE [dbo].[ActualizarEstadoPrograma]
(
	@IdPrograma int,
	@Estado varchar(50)
)
AS
BEGIN

SET NOCOUNT ON;

UPDATE Programa
SET Estado = @Estado
WHERE Id = @IdPrograma;

END
