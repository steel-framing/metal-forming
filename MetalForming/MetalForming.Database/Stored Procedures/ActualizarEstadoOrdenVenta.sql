CREATE PROCEDURE [dbo].[ActualizarEstadoOrdenVenta]
(
	@Id int,
	@Estado varchar(50)
)
AS
BEGIN

SET NOCOUNT ON;

UPDATE dbo.OrdenVenta SET Estado = @Estado WHERE Id = @Id

END

