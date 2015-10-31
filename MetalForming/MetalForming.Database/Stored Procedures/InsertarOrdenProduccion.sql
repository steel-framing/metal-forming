CREATE PROCEDURE [dbo].[InsertarOrdenProduccion]
(
	@IdOrdenVenta int,
	@Estado varchar(50),
	@CantidadProducto int
)
AS
BEGIN

SET NOCOUNT ON;

INSERT INTO dbo.OrdenProduccion(Numero, IdOrdenVenta, Estado, CantidadProducto, FechaCreacion)
VALUES('', @IdOrdenVenta, @Estado, @CantidadProducto, GETDATE());

DECLARE @IdOrdeProduccion int = 0;

SELECT @IdOrdeProduccion = SCOPE_IDENTITY();

UPDATE dbo.OrdenProduccion
SET Numero = 'OP' + REPLACE(STR(Id, 6), SPACE(1), '0')
WHERE Id = @IdOrdeProduccion;

SELECT @IdOrdeProduccion;

END


