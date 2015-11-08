CREATE PROCEDURE [dbo].[ActualizarProgramaOrdenVenta]
(
	@Id int,
	@IdPrograma int
)
AS
BEGIN

SET NOCOUNT ON;

UPDATE OrdenVenta
SET IdPrograma = @IdPrograma
WHERE Id = @Id;

END
