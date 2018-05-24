

/************************ADDRESS TYPES***********************************************/

INSERT INTO [BusinessEntity].[AddressType]
           ([AddressTypeId],[Name],[Description])
     VALUES
           (1,'Facturación','Dirección de facturación')

INSERT INTO [BusinessEntity].[AddressType]
           ([AddressTypeId],[Name],[Description])
     VALUES
           (2,'Hogar','Lugar de residencia')

INSERT INTO [BusinessEntity].[AddressType]
           ([AddressTypeId],[Name],[Description])
     VALUES
           (3,'Oficina','Dirección de trabajo')

INSERT INTO [BusinessEntity].[AddressType]
           ([AddressTypeId],[Name],[Description])
     VALUES
           (4,'Envío','Dirección de envío')
GO

/*************************PHONE TYPES***************************************/

INSERT INTO [BusinessEntity].[PhoneType]
           ([PhoneTypeId],[Name],[Description])
     VALUES
           (1,'Celular','Teléfono Movil')

INSERT INTO [BusinessEntity].[PhoneType]
           ([PhoneTypeId],[Name],[Description])
     VALUES
           (2,'Casa','Teléfono de casa')

INSERT INTO [BusinessEntity].[PhoneType]
           ([PhoneTypeId],[Name],[Description])
     VALUES
           (3,'Trabajo','Teléfono de Oficina')

GO
/********************MAIN USER**********************************************/


INSERT INTO [Security].[AspNetUsers]
           ([Id],[Email],[EmailConfirmed],[PasswordHash],[SecurityStamp],[PhoneNumberConfirmed],[TwoFactorEnabled],[LockoutEnabled],[AccessFailedCount],[UserName])
     VALUES
           (N'384a8104-cbc6-40a5-a27c-2947263e0fcc','arkhameng@gmail.com',1,N'ALIU7GyVEDX2YlBvmSlPhe4EKwhOIwUmSpjc+K5jww/vuzjpzHgGaNDCSJCXfMxtWA==',N'c7d38da5-c068-4838-97b1-b563eaf4a53b',0,0,1,0,'System')
GO

INSERT INTO [BusinessEntity].[Person]
           ([Name],[FTR],[Email],[Phone],[TownId],[Street],[OutNumber],[Location],[ZipCode],[IsActive],[InsDate],[InsUser],[UpdDate],[UpdUser])
     VALUES
           ('Ariel Camacho Barrientos','CABA810609RN2','arkhameng@gmail.com','(229)1251761','30193','Paseo De Cuitlahuac','175','Fraccionamiento Palma Real','91826',1,GETDATE(),'System',GETDATE(),'System')
GO

INSERT INTO [BusinessEntity].[Address]
           ([AddressTypeId],[PersonId],[TownId],[Street],[OutNumber],[Location],[ZipCode],[IsActive],[InsDate],[InsUser],[UpdDate],[UpdUser])
     VALUES
           (1, 1,'30193','Paseo De Cuitlahuac','175','Fraccionamiento Palma Real','91826',1,GETDATE(),'System',GETDATE(),'System')

GO

INSERT INTO [BusinessEntity].[PhoneNumber]
           ([PhoneTypeId],[PersonId],[Phone],[InsDate],[InsUser],[UpdDate],[UpdUser])
     VALUES
           (1,1,'(229)1251761',GETDATE(),'System',GETDATE(),'System')
GO

INSERT INTO [Security].[SystemUser]
           ([SystemUserId],[UserId],[InsDate],[InsUser],[UpdDate],[UpdUser])
     VALUES
           (1,N'384a8104-cbc6-40a5-a27c-2947263e0fcc',GETDATE(),'System',GETDATE(),'System')

GO
/**********************JOB POSITIONS****************************/
INSERT INTO [HumanResources].[JobPosition]
           ([Name],[Description],[InsDate],[UpdDate],[InsUser],[UpdUser])
     VALUES
           ('GERENTE GENERAL','GERENTE GENERAL',GETDATE(),GETDATE(),'System','System')
GO
INSERT INTO [HumanResources].[JobPosition]
           ([Name],[Description],[InsDate],[UpdDate],[InsUser],[UpdUser])
     VALUES
           ('GERENTE OPERATIVO','GERENTE OPERATIVO',GETDATE(),GETDATE(),'System','System')
GO
INSERT INTO [HumanResources].[JobPosition]
           ([Name],[Description],[InsDate],[UpdDate],[InsUser],[UpdUser])
     VALUES
           ('GERENTE DE VENTAS','GERENTE DE VENTAS',GETDATE(),GETDATE(),'System','System')
GO
INSERT INTO [HumanResources].[JobPosition]
           ([Name],[Description],[InsDate],[UpdDate],[InsUser],[UpdUser])
     VALUES
           ('SUPERVISOR','GERENTE DE VENTAS',GETDATE(),GETDATE(),'System','System')
GO
INSERT INTO [HumanResources].[JobPosition]
           ([Name],[Description],[InsDate],[UpdDate],[InsUser],[UpdUser])
     VALUES
           ('VENDEDOR','VENDEDOR',GETDATE(),GETDATE(),'System','System')
GO
INSERT INTO [HumanResources].[JobPosition]
           ([Name],[Description],[InsDate],[UpdDate],[InsUser],[UpdUser])
     VALUES
           ('INSTALADOR','INSTALADOR',GETDATE(),GETDATE(),'System','System')
