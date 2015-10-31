CREATE PROCEDURE [dbo].[ActualizarStockProducto]
(
	@IdProducto int,
	@Cantidad int
)
AS
BEGIN

SET NOCOUNT ON;

UPDATE dbo.Producto 
SET Stock = Stock + @Cantidad
WHERE Id = @IdProducto

END

