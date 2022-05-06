
--Valida si existe Tarjeta
CREATE PROCEDURE sp_getTarjeta(
	@NoTarjeta BIGINT = 0
)
AS
BEGIN
	DECLARE
		@MensajeError VARCHAR(300) = ''
	
	BEGIN TRY

	IF EXISTS (SELECT 1 FROM tbl_Tarjeta WHERE IntNumero_Tarjeta=@NoTarjeta)
	BEGIN

		SELECT  'Correcto' AS Estatus,
				vchNombreCompleto AS Mensaje
		FROM   tbl_Tarjeta 
			INNER JOIN dbo.tbl_Cuenta 
			ON dbo.tbl_Tarjeta.id_NoCuenta = dbo.tbl_Cuenta.IntNumero_Cuenta 
			INNER JOIN dbo.tbl_Cliente 
			ON dbo.tbl_Cuenta.id_NoCliente = dbo.tbl_Cliente.IntNumero_Cliente
		WHERE IntNumero_Tarjeta =@NoTarjeta;
		
	END
	ELSE
	BEGIN
		SELECT 'Incorrecto' AS Estatus,'¡La tarjeta no existe en la Base de Datos!' AS Mensaje;
	END 

	END TRY
	BEGIN CATCH
		IF(LEN(@MensajeError) = 0)
			BEGIN
				SET @MensajeError = (SELECT SUBSTRING(ERROR_MESSAGE(), 1, 300))
			END
		RAISERROR(@MensajeError, 16, 1)
	END CATCH
END
go

--Valida Nip
CREATE PROCEDURE sp_getNip(
	@NoNip BIGINT = 0,
	@NoTarjeta BIGINT = 0
)
AS
BEGIN
	DECLARE
		@MensajeError VARCHAR(300) = ''
	
	BEGIN TRY

	IF EXISTS (SELECT 1 FROM tbl_Tarjeta WHERE Nip=@NoNip AND IntNumero_Tarjeta=@NoTarjeta)
	BEGIN

		SELECT  'Correcto' AS Estatus,
				IntNumero_Cliente AS Mensaje
		FROM   tbl_Tarjeta 
			INNER JOIN dbo.tbl_Cuenta 
			ON dbo.tbl_Tarjeta.id_NoCuenta = dbo.tbl_Cuenta.IntNumero_Cuenta 
			INNER JOIN dbo.tbl_Cliente 
			ON dbo.tbl_Cuenta.id_NoCliente = dbo.tbl_Cliente.IntNumero_Cliente
		WHERE Nip =@NoNip AND IntNumero_Tarjeta=@NoTarjeta;
		
	END
	ELSE
	BEGIN
		SELECT 'Incorrecto' AS Estatus,'¡El número del Nip de la tarjeta no coincide!' AS Mensaje;
	END 

	END TRY
	BEGIN CATCH
		IF(LEN(@MensajeError) = 0)
			BEGIN
				SET @MensajeError = (SELECT SUBSTRING(ERROR_MESSAGE(), 1, 300))
			END
		RAISERROR(@MensajeError, 16, 1)
	END CATCH
END
go

--Valida Saldo del cliente y del cajero
CREATE PROCEDURE sp_getValidaSaldo(
	@Saldo BIGINT = 0,
	@NoTarjeta BIGINT = 0,
	@NoCajero BIGINT= 0
)
AS
BEGIN
	DECLARE
		@MensajeError VARCHAR(300) = ''
	
	BEGIN TRY

	IF EXISTS (SELECT 1 FROM dbo.tbl_Cuenta 
							INNER JOIN dbo.tbl_Tarjeta 
							ON dbo.tbl_Cuenta.IntNumero_Cuenta = dbo.tbl_Tarjeta.id_NoCuenta
						WHERE IntNumero_Tarjeta=@NoTarjeta and fltSaldo>=@Saldo)
	BEGIN
			IF EXISTS (SELECT 1 FROM tbl_Cajero WHERE id_Numero_Cajero=@NoCajero AND fltSaldo >=@Saldo)
			BEGIN
				SELECT 'Correcto' AS Estatus,'¡Estimado cliente esta en proceso el Retiro en Efectivo!' AS Mensaje;
			END

			ELSE

			BEGIN
				SELECT 'Incorrecto' AS Estatus,'¡El cajero no cuenta con suficiente Denominacionoes para realizar el retiro!' AS Mensaje;
			END	
	END

	ELSE

	BEGIN
		SELECT 'Incorrecto' AS Estatus,'¡No cuentas con lo suficiente saldo para realizar el retiro!' AS Mensaje;
	END 

	END TRY
	BEGIN CATCH
		IF(LEN(@MensajeError) = 0)
			BEGIN
				SET @MensajeError = (SELECT SUBSTRING(ERROR_MESSAGE(), 1, 300))
			END
		RAISERROR(@MensajeError, 16, 1)
	END CATCH
END
go


--Valida Saldo del cliente y del cajero
CREATE PROCEDURE sp_InsertTransaccionRetiro(
	@Saldo BIGINT = 0,
	@NoTarjeta BIGINT = 0,
	@NoCajero BIGINT= 0
)
AS
BEGIN
	DECLARE
		@MensajeError VARCHAR(300) = ''
	
	BEGIN TRY
	Insert into tbl_Transaccion (vchtipo_Accion,Fecha, Cantidad, Id_NoTarjeta,id_NoCajero) values ('Retiro', GETDATE(), @Saldo,@NoTarjeta,@NoCajero);

	UPDATE tbl_Cuenta SET fltSaldo=(fltSaldo-@Saldo) FROM tbl_Cuenta
       INNER JOIN tbl_Tarjeta
        ON tbl_Cuenta.IntNumero_Cuenta = tbl_Tarjeta.id_NoCuenta
		WHERE IntNumero_Tarjeta=@NoTarjeta;

	update tbl_Cajero set fltSaldo =(fltSaldo-@Saldo) where id_Numero_Cajero=@NoCajero;

	END TRY
	BEGIN CATCH
		IF(LEN(@MensajeError) = 0)
			BEGIN
				SET @MensajeError = (SELECT SUBSTRING(ERROR_MESSAGE(), 1, 300))
			END
		RAISERROR(@MensajeError, 16, 1)
	END CATCH
END
go



SELECT *FROM tbl_Cajero

EXEC sp_getTarjeta 4178545161711
EXEC sp_getNip 2211,4178545161711
EXEC sp_getValidaSaldo 1000, 417854516171,201
494000