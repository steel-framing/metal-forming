CREATE PROCEDURE [dbo].[InsertarOrdenProduccionSecuencia]
(
	@Secuencia int,
	@IdOrdenProduccion int,
	@IdMaquina int,
	@FechaInicio datetime,
	@FechaFin datetime
)
AS
BEGIN

SET NOCOUNT ON;

INSERT INTO dbo.OrdenProduccionSecuencia(Secuencia, IdOrdenProduccion, IdMaquina, FechaInicio, FechaFin)
VALUES(@Secuencia, @IdOrdenProduccion, @IdMaquina, @FechaInicio, @FechaFin);

END

