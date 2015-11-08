CREATE PROCEDURE [dbo].[ActualizarOrdenProduccionPorRechazo]
(
	@Id int,
	@Estado varchar(50),
	@MotivoRechazo varchar(500)
)
AS
BEGIN

SET NOCOUNT ON;

UPDATE OrdenProduccion SET Estado = @Estado, MotivoRechazo = @MotivoRechazo WHERE Id = @Id;

END
