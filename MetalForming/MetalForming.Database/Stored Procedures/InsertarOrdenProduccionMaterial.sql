CREATE PROCEDURE [dbo].[InsertarOrdenProduccionMaterial]
(
	@IdOrdenProduccion int,
	@IdMaterial int,
	@Requerido int,
	@Comprar int
)
AS
BEGIN

SET NOCOUNT ON;

INSERT INTO dbo.OrdenProduccionMaterial(IdOrdenProduccion, IdMaterial, Requerido, Comprar)
VALUES(@IdOrdenProduccion, @IdMaterial, @Requerido, @Comprar);

END

