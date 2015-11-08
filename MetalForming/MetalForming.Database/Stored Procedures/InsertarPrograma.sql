CREATE PROCEDURE [dbo].[InsertarPrograma]
(
	@IdPrograma int output,
	@Numero varchar(50) output,
	@FechaInicio date,
	@FechaFin date,
	@CantidadOV int,
	@Estado varchar(50),
	@IdPlan int
)
AS
BEGIN

SET NOCOUNT ON;

INSERT INTO Programa(
	FechaInicio,
	FechaFin,
	CantidadOV,
	Estado,
	IdPlan
) 
VALUES (
	@FechaInicio,
	@FechaFin,
	@CantidadOV,
	@Estado,
	@IdPlan
)

SELECT @IdPrograma = SCOPE_IDENTITY();

SELECT @Numero = Numero FROM Programa WHERE Id = @IdPrograma;

END
