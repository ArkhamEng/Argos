

/************************ADDRESS TYPES***********************************************/

INSERT INTO [Business].[AddressType]
           ([AddressTypeId],[Name],[Description])
     VALUES
           (1,'Facturaci�n','Direcci�n de facturaci�n')

INSERT INTO [Business].[AddressType]
           ([AddressTypeId],[Name],[Description])
     VALUES
           (2,'Hogar','Lugar de residencia')

INSERT INTO [Business].[AddressType]
           ([AddressTypeId],[Name],[Description])
     VALUES
           (3,'Oficina','Direcci�n de trabajo')

INSERT INTO [Business].[AddressType]
           ([AddressTypeId],[Name],[Description])
     VALUES
           (4,'Env�o','Direcci�n de env�o')
GO

/*************************PHONE TYPES***************************************/

INSERT INTO [Business].[PhoneType]
           ([PhoneTypeId],[Name],[Description])
     VALUES
           (1,'Celular','Tel�fono Movil')

INSERT INTO [Business].[PhoneType]
           ([PhoneTypeId],[Name],[Description])
     VALUES
           (2,'Casa','Tel�fono de casa')

INSERT INTO [Business].[PhoneType]
           ([PhoneTypeId],[Name],[Description])
     VALUES
           (3,'Trabajo','Tel�fono de Oficina')

GO
/********************MAIN USER**********************************************/


INSERT INTO [Security].[AspNetUsers]
           ([Id],[Email],[EmailConfirmed],[PasswordHash],[SecurityStamp],[PhoneNumberConfirmed],[TwoFactorEnabled],[LockoutEnabled],[AccessFailedCount],[UserName])
     VALUES
           (N'384a8104-cbc6-40a5-a27c-2947263e0fcc','arkhameng@gmail.com',1,N'ALIU7GyVEDX2YlBvmSlPhe4EKwhOIwUmSpjc+K5jww/vuzjpzHgGaNDCSJCXfMxtWA==',N'c7d38da5-c068-4838-97b1-b563eaf4a53b',0,0,1,0,'System')
GO

INSERT INTO [Business].[Entity]
           ([IsActive],[InsDate],[InsUser],[UpdDate],[UpdUser])
     VALUES
           (1,GETDATE(),'System',GETDATE(),'System')
GO

INSERT INTO [Business].[Entity]
           ([IsActive],[InsDate],[InsUser],[UpdDate],[UpdUser])
     VALUES
           (1,GETDATE(),'System',GETDATE(),'System')
GO


INSERT INTO [Business].[Person]
           (EntityId,[Name],[FTR])
     VALUES
           (1,'Ariel Camacho Barrientos','CABA810609RN2')
GO


INSERT INTO [Config].[Branch]
           ([EntityId],[Name],[ShortName])
     VALUES
           (2,'Veracruz','VER')
GO


INSERT INTO Operative.Supplier
(EntityId, BusinessName,WebSite)
VALUES
(1,'Ariel Camacho Barrientos','http://www.arkhamnet.com')

GO


INSERT INTO [Business].[Address]
           ([AddressTypeId],EntityId,[TownId],[Street],[Location],[ZipCode],[IsActive],[InsDate],[InsUser],[UpdDate],[UpdUser])
     VALUES
           (1, 1,'30193','Paseo De Cuitlahuac No. 175','Fraccionamiento Palma Real','91826',1,GETDATE(),'System',GETDATE(),'System')

GO


INSERT INTO [Business].[Address]
           ([AddressTypeId],EntityId,[TownId],[Street],[Location],[ZipCode],[IsActive],[InsDate],[InsUser],[UpdDate],[UpdUser])
     VALUES
           (1, 2,'30193','Paseo De Cuitlahuac No. 175','Fraccionamiento Palma Real','91826',1,GETDATE(),'System',GETDATE(),'System')

GO

INSERT INTO [Business].[PhoneNumber]
           ([PhoneTypeId],EntityId,[Phone],[InsDate],[InsUser],[UpdDate],[UpdUser])
     VALUES
           (1,1,'(229) 125 17 61',GETDATE(),'System',GETDATE(),'System')
GO

INSERT INTO [Business].[PhoneNumber]
           ([PhoneTypeId],EntityId,[Phone],[InsDate],[InsUser],[UpdDate],[UpdUser])
     VALUES
           (1,2,'(229) 125 17 62',GETDATE(),'System',GETDATE(),'System')
GO


INSERT INTO [Business].[EmailAddress]
           ([EntityId],[Email],[InsDate],[InsUser],[UpdDate],[UpdUser])
     VALUES
           (1,'arkhameng@gmail.com',getdate(),'System',getdate(),'System')
GO


INSERT INTO [Business].[EmailAddress]
           ([EntityId],[Email],[InsDate],[InsUser],[UpdDate],[UpdUser])
     VALUES
           (2,'veracruz@michangarro.com',getdate(),'System',getdate(),'System')
GO


INSERT INTO [Inventory].[Storage]
           ([BranchId],[Name],[InsDate],[InsUser],[UpdDate],[UpdUser])
     VALUES
           (2,'Bodega',GETDATE(),'System',GETDATE(),'System')
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


/**********************   MESURE UNITS  ****************************/


INSERT INTO [Inventory].[MeasureUnit]
           ([MeasureUnitId],[Name],[Description],[InsDate],[UpdDate],[InsUser],[UpdUser])
           
     VALUES
           ('Mts','Metro','Metro',GETDATE(),GETDATE(),'System','System')
GO

INSERT INTO [Inventory].[MeasureUnit]
           ([MeasureUnitId],[Name],[Description],[InsDate],[UpdDate],[InsUser],[UpdUser])
           
     VALUES
           ('Lto','Litro','Litro',GETDATE(),GETDATE(),'System','System')
GO

INSERT INTO [Inventory].[MeasureUnit]
           ([MeasureUnitId],[Name],[Description],[InsDate],[UpdDate],[InsUser],[UpdUser])
           
     VALUES
           ('Kg','Kilo','Kilo',GETDATE(),GETDATE(),'System','System')
GO

INSERT INTO [Inventory].[MeasureUnit]
           ([MeasureUnitId],[Name],[Description],[InsDate],[UpdDate],[InsUser],[UpdUser])
           
     VALUES
           ('Pza','Pieza','Pieza',GETDATE(),GETDATE(),'System','System')
GO