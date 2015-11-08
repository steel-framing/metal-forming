CREATE PROCEDURE [dbo].[ActualizarPrograma]
(
	@IdPrograma int,
	@FechaInicio date,
	@FechaFin date,
	@CantidadOV int,
	@Estado varchar(50),
	@MotivoFinalizacion varchar(500),
	@FechaFinalizacion datetime
)
AS
BEGIN

SET NOCOUNT ON;

UPDATE Programa
SET FechaInicio = @FechaInicio,
	FechaFin = @FechaFin,
	CantidadOV = @CantidadOV,
	Estado = @Estado,
	MotivoFinalizacion = @MotivoFinalizacion,
	FechaFinalizacion = @FechaFinalizacion
WHERE Id = @IdPrograma;

END
