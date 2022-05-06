CREATE DATABASE bd_Banco
USE bd_Banco
GO

CREATE TABLE tbl_Banco
(
IntNumero_Banco BIGINT NOT NULL,
vchDireccion VARCHAR(100) NOT NULL,
vchCorreo VARCHAR(100)NOT NULL,
IntTelefono BIGINT NOT NULL,
CONSTRAINT PK_tbl_Banco PRIMARY KEY(IntNumero_Banco),
CHECK(IntTelefono LIKE'[0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9]'),
)
GO

create table tbl_Cliente
(
IntNumero_Cliente BIGINT NOT NULL, 
vchNombreCompleto varchar(200) NOT NULL,
vchCorreo varchar(100)NULL,
intTelefono bigint NULL,
vchRfc varchar(50) null unique,
id_NoBanco bigint not null,
CONSTRAINT PK_tbl_Cliente PRIMARY KEY(IntNumero_Cliente ),
CONSTRAINT FK_tbl_Banco1 FOREIGN KEY(id_NoBanco) REFERENCES tbl_Banco(IntNumero_Banco),
check(intTelefono LIKE'[0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9]')
)
Go


create table tbl_Cuenta
(
IntNumero_Cuenta BIGINT NOT NULL,
vchtipo_cuenta varchar(50)
check(vchtipo_cuenta IN('Guardadito','Basica','Plus')),
fltSaldo float default 0,
id_NoBanco bigint not null,
id_NoCliente bigint not null,
CONSTRAINT PK_tbl_Cuenta PRIMARY KEY(IntNumero_Cuenta ),
CONSTRAINT FK_tbl_Banco2 FOREIGN KEY (id_NoBanco) REFERENCES tbl_Banco(IntNumero_Banco),
CONSTRAINT FK_tbl_Cliente2 FOREIGN KEY (id_NoCliente) REFERENCES tbl_Cliente(IntNumero_Cliente)
)
Go

create table tbl_Tarjeta
(
IntNumero_Tarjeta BIGINT NOT NULL,
vchtipo_tarjeta varchar(50)
check(vchtipo_tarjeta IN('Credito','Debito')),
FechaExpiracion smalldatetime not null,
Nip bigint not null,
id_NoCuenta bigint not null,
CONSTRAINT PK_tbl_Tarjeta PRIMARY KEY(IntNumero_Tarjeta ),
CONSTRAINT FK_tbl_Cuenta3 FOREIGN KEY (id_NoCuenta) REFERENCES tbl_Cuenta(IntNumero_Cuenta)
)
Go
create table tbl_Cajero
(
id_Numero_Cajero bigint not null,
fltSaldo bigint null,
id_NoBanco bigint not null,
CONSTRAINT PK_tbl_Cajero PRIMARY KEY(id_Numero_Cajero),
CONSTRAINT FK_tbl_Banco FOREIGN KEY (id_NoBanco) REFERENCES tbl_Banco(IntNumero_Banco),
)
GO
create table tbl_Transaccion
(
IntNumero_Transaccion BIGINT identity(1,1),
vchtipo_Accion varchar(50),
check(vchtipo_Accion  IN('Retiro','Deposito')),
Fecha smalldatetime default getdate(),
Cantidad float not null,
Id_NoTarjeta bigint not null,
id_NoCajero bigint not null,
CONSTRAINT PK_tbl_Transaccion PRIMARY KEY(IntNumero_Transaccion ),
CONSTRAINT FK_tbl_Tarjeta FOREIGN KEY (Id_NoTarjeta) REFERENCES tbl_Tarjeta(IntNumero_Tarjeta),
CONSTRAINT FK_tbl_Cajero FOREIGN KEY (id_NoCajero) REFERENCES tbl_Cajero(id_Numero_Cajero)
)
Go

create table tbl_DenominacionCajero
(
Id_Denominacion BIGINT identity(1,1),
intBillete int not null,
intCantidad int not null,
id_NoCajero bigint not null,
CONSTRAINT PK_tbl_DenominacionCaja PRIMARY KEY(Id_Denominacion),
CONSTRAINT FK_tbl_Cajero1 FOREIGN KEY (id_NoCajero) REFERENCES tbl_Cajero(id_Numero_Cajero)
)

create table tbl_TransaccionDetalle(
id_TransacionDetalle BIGINT identity(1,1),
id_NoTransacion bigint not null,
intBillete int not null,
intCantidad int not null,
CONSTRAINT PK_tbl_TransaccionDetalle PRIMARY KEY(id_TransacionDetalle),
)
go

INSERT [dbo].[tbl_Banco] ([IntNumero_Banco], [vchDireccion], [vchCorreo], [IntTelefono]) VALUES (101, N'Mexico Centro', N'A@azteca.com', 1234567890)
GO
INSERT [dbo].[tbl_Banco] ([IntNumero_Banco], [vchDireccion], [vchCorreo], [IntTelefono]) VALUES (102, N'Mexico Norte', N'A@azteca.com', 1234567890)
GO
INSERT [dbo].[tbl_Cliente] ([IntNumero_Cliente], [vchNombreCompleto], [vchCorreo], [intTelefono], [vchRfc], [id_NoBanco]) VALUES (1234567890, N'Karlin Cruz Ambrosio', N'karlñinkd010@gmail.com', 7711881285, N'karlin1234', 101)
GO
INSERT [dbo].[tbl_Cuenta] ([IntNumero_Cuenta], [vchtipo_cuenta], [fltSaldo], [id_NoBanco], [id_NoCliente]) VALUES (20205055, N'Plus', 5000, 101, 1234567890)
GO
INSERT [dbo].[tbl_Cajero] ([id_Numero_Cajero], [fltSaldo], [id_NoBanco]) VALUES (201, 494000, 101)
GO
INSERT [dbo].[tbl_Tarjeta] ([IntNumero_Tarjeta], [vchtipo_tarjeta], [FechaExpiracion], [Nip], [id_NoCuenta]) VALUES (417854516171, N'Debito', CAST(N'2025-06-01T00:00:00' AS SmallDateTime), 2211, 20205055)
GO
INSERT [dbo].[tbl_DenominacionCajero] ([intBillete], [intCantidad], [id_NoCajero]) VALUES (1000, 100, 201)
GO
INSERT [dbo].[tbl_DenominacionCajero] ([intBillete], [intCantidad], [id_NoCajero])  VALUES (500, 200, 201)
GO
INSERT [dbo].[tbl_DenominacionCajero] ([intBillete], [intCantidad], [id_NoCajero]) VALUES (200, 900, 201)
GO
INSERT [dbo].[tbl_DenominacionCajero] ([intBillete], [intCantidad], [id_NoCajero]) VALUES (100, 1000, 201)
GO
INSERT [dbo].[tbl_DenominacionCajero] ([intBillete], [intCantidad], [id_NoCajero]) VALUES (50, 200, 201)
GO
INSERT [dbo].[tbl_DenominacionCajero] ([intBillete], [intCantidad], [id_NoCajero]) VALUES ( 20, 200, 201)
GO