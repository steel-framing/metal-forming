CREATE PROCEDURE [dbo].[ActualizarEstadoOrdenProduccion]
(
	@Id int,
	@Estado varchar(50)
)
AS
BEGIN

SET NOCOUNT ON;

UPDATE OrdenProduccion SET Estado = @Estado WHERE Id = @Id;

END
