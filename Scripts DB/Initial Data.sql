/************************ CATEGORIES ***********************************************/

INSERT INTO [Inventory].[Category]
           ([Name],[Description],[IsActive],[InsDate],[InsUser],[UpdDate],[UpdUser])
     VALUES
           ('Servicios','Servicios no cuantificables',1,GETDATE(),'System',GETDATE(),'System')


INSERT INTO [Inventory].[Category]
           ([Name],[Description],[IsActive],[InsDate],[InsUser],[UpdDate],[UpdUser])
     VALUES
           ('Camaras','Camaras',1,GETDATE(),'System',GETDATE(),'System')


GO
/************************SUB CATEGORIES ***********************************************/

INSERT INTO [Inventory].SubCategory
           (CategoryId, [Name],[Description])
     VALUES
           (1,'Alarma Residencial','Alarma Residencial')


INSERT INTO [Inventory].SubCategory
           (CategoryId, [Name],[Description])
     VALUES
           (1,'Rastreo Satelital','Rastreo Satelital')


INSERT INTO [Inventory].SubCategory
           (CategoryId, [Name],[Description])
     VALUES
           (1,'Cercado Electrico','Alarma Residencial')



INSERT INTO [Inventory].SubCategory
           (CategoryId, [Name],[Description])
     VALUES
           (1,'Circuito Cerrado','Circuito Cerrado')

INSERT INTO [Inventory].SubCategory
           (CategoryId, [Name],[Description])
     VALUES
           (2,'Camaras IP','Camaras IP')



INSERT INTO [Inventory].SubCategory
           (CategoryId, [Name],[Description])
     VALUES
           (2,'Camaras Avanzadas','Camaras Avanzadas')

GO

/************************CREDIT STATUS ***********************************************/
INSERT INTO [Config].[CreditStatus]
           ([CreditStatusId],[Name])
     VALUES
           (-2,'Cancelado')

INSERT INTO [Config].[CreditStatus]
           ([CreditStatusId],[Name])
     VALUES
           (-1,'Suspendido')

INSERT INTO [Config].[CreditStatus]
           ([CreditStatusId],[Name])
     VALUES
           (1,'Inactivo')

INSERT INTO [Config].[CreditStatus]
           ([CreditStatusId],[Name])
     VALUES
           (2,'Activo')

Go
/************************ CREDIT PAY FORMS ***********************************************/

INSERT INTO [Config].[PayForm]
           ([PayFormId],[Name])
     VALUES
           (1,'Contado')

INSERT INTO [Config].[PayForm]
           ([PayFormId],[Name])
     VALUES
           (2,'Crédito')
GO

/************************ PURCHASE STATUS ***********************************************/
INSERT INTO [Purchasing].[Status]
           ([PurchaseStatusId],[Name])
     VALUES
           (-1,'Cancelado')


INSERT INTO [Purchasing].[Status]
           ([PurchaseStatusId],[Name])
     VALUES
           (1,'En Revisión')

INSERT INTO [Purchasing].[Status]
           ([PurchaseStatusId],[Name])
     VALUES
           (2,'Autorizado')


INSERT INTO [Purchasing].[Status]
           ([PurchaseStatusId],[Name])
     VALUES
           (3,'Completado')
GO

/************************ SALE STATUS ***********************************************/
INSERT INTO [Sales].[Status]
           (SaleStatusId,[Name])
     VALUES
           (-2,'Cancelado')

INSERT INTO [Sales].[Status]
           (SaleStatusId,[Name])
     VALUES
           (-1,'En Cancelación')

INSERT INTO [Sales].[Status]
           (SaleStatusId,[Name])
     VALUES
           (1,'Reservado')


INSERT INTO [Sales].[Status]
           (SaleStatusId,[Name])
     VALUES
           (2,'Entregado')

INSERT INTO [Sales].[Status]
           (SaleStatusId,[Name])
     VALUES
           (3,'Pagado')

INSERT INTO [Sales].[Status]
           (SaleStatusId,[Name])
     VALUES
           (4,'Completado')
GO
/************************FLOW DIRECTIONS***********************************************/
INSERT INTO [Inventory].[FlowDirection]
           ([FlowDirectionId],[Name])
     VALUES
           (1,'Entrada')

INSERT INTO [Inventory].[FlowDirection]
           ([FlowDirectionId],[Name])
     VALUES
           (2,'Salida')
GO


/************************ADDRESS TYPES***********************************************/

INSERT INTO [Business].[AddressType]
           ([AddressTypeId],[Name],[Description])
     VALUES
           (1,'Facturación','Dirección de facturación')

INSERT INTO [Business].[AddressType]
           ([AddressTypeId],[Name],[Description])
     VALUES
           (2,'Hogar','Lugar de residencia')

INSERT INTO [Business].[AddressType]
           ([AddressTypeId],[Name],[Description])
     VALUES
           (3,'Oficina','Dirección de trabajo')

INSERT INTO [Business].[AddressType]
           ([AddressTypeId],[Name],[Description])
     VALUES
           (4,'Envío','Dirección de envío')
GO

/*************************PHONE TYPES***************************************/


INSERT INTO [Business].[PhoneType]
           ([PhoneTypeId],[Name],[Description])
     VALUES
           (1,'Móvil','Teléfono móvil')

INSERT INTO [Business].[PhoneType]
           ([PhoneTypeId],[Name],[Description])
     VALUES
           (2,'Casa','Teléfono de casa')

INSERT INTO [Business].[PhoneType]
           ([PhoneTypeId],[Name],[Description])
     VALUES
           (3,'Oficina','Teléfono de oficina')

INSERT INTO [Business].[PhoneType]
           ([PhoneTypeId],[Name],[Description])
     VALUES
           (4,'Fax','Número de Fax')
GO
/*************************PHONE TYPES***************************************/

INSERT INTO [Business].[EmailType]
           ([EmailTypeId],[Name],[Description])
     VALUES
           (1,'Principal','Correo principal')

INSERT INTO [Business].[EmailType]
           ([EmailTypeId],[Name],[Description])
     VALUES
           (2,'Trabajo','Correo de trabajo')

		   
INSERT INTO [Business].[EmailType]
           ([EmailTypeId],[Name],[Description])
     VALUES
           (3,'Personal','Correo personal')
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


INSERT INTO [Purchasing].Supplier
(EntityId, BusinessName,WebSite,[Status],[CreditLimit],[CreditDays],[CreditBalance])
VALUES
(1,'Ariel Camacho Barrientos','http://www.arkhamnet.com',1,0,0,0)

GO


INSERT INTO [Business].[Address]
           ([AddressTypeId],EntityId,[TownId],[Street],[Location],[ZipCode],[InsDate],[InsUser])
     VALUES
           (1, 1,'30193','Paseo De Cuitlahuac No. 175','Fraccionamiento Palma Real','91826',GETDATE(),'System')

GO


INSERT INTO [Business].[Address]
           ([AddressTypeId],EntityId,[TownId],[Street],[Location],[ZipCode],[InsDate],[InsUser])
     VALUES
           (1, 2,'30193','Paseo De Cuitlahuac No. 175','Fraccionamiento Palma Real','91826',GETDATE(),'System')

GO

INSERT INTO [Business].[PhoneNumber]
           ([PhoneTypeId],EntityId,[Phone],[InsDate],[InsUser])
     VALUES
           (1,1,'(229) 125 17 61',GETDATE(),'System')
GO

INSERT INTO [Business].[PhoneNumber]
           ([PhoneTypeId],EntityId,[Phone],[InsDate],[InsUser])
     VALUES
           (1,2,'(229) 125 17 62',GETDATE(),'System')
GO


INSERT INTO [Business].[EmailAddress]
           ([EntityId],EmailTypeId,[Email],[InsDate],[InsUser])
     VALUES
           (1,1,'arkhameng@gmail.com',getdate(),'System')
GO


INSERT INTO [Business].[EmailAddress]
           ([EntityId],EmailTypeId,[Email],[InsDate],[InsUser])
     VALUES
           (2,1,'veracruz@michangarro.com',getdate(),'System')
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
           ([MeasureUnitId],[Name],[Description],[InsDate],[UpdDate],[InsUser],[UpdUser], IsActive)
           
     VALUES
           ('Mts','Metro','Metro',GETDATE(),GETDATE(),'System','System',1)
GO

INSERT INTO [Inventory].[MeasureUnit]
           ([MeasureUnitId],[Name],[Description],[InsDate],[UpdDate],[InsUser],[UpdUser], IsActive)
           
     VALUES
           ('Lto','Litro','Litro',GETDATE(),GETDATE(),'System','System',1)
GO

INSERT INTO [Inventory].[MeasureUnit]
           ([MeasureUnitId],[Name],[Description],[InsDate],[UpdDate],[InsUser],[UpdUser], IsActive)
           
     VALUES
           ('Kg','Kilo','Kilo',GETDATE(),GETDATE(),'System','System',1)
GO

INSERT INTO [Inventory].[MeasureUnit]
           ([MeasureUnitId],[Name],[Description],[InsDate],[UpdDate],[InsUser],[UpdUser], IsActive)
           
     VALUES
           ('Pza','Pieza','Pieza',GETDATE(),GETDATE(),'System','System',1)
GO

/*********************ACCOUNT STATUS************************************/

INSERT INTO [Production].[AccountStatus]
          (AccountStatusId, Code, Name, [Description], IsActive, InsDate, InsUser, UpdDate, UpdUser)
     VALUES
          (-2,'Cancelled','Cancelado','Cuenta Cancelada',1,GETDATE(),'System',GETDATE(),'System') 
GO

INSERT INTO [Production].[AccountStatus]
          (AccountStatusId, Code, Name, [Description], IsActive, InsDate, InsUser, UpdDate, UpdUser)
     VALUES
          (-1,'Suspended','Suspendido','Cuenta Suspendida',1,GETDATE(),'System',GETDATE(),'System') 
GO

INSERT INTO [Production].[AccountStatus]
          (AccountStatusId, Code, Name, [Description], IsActive, InsDate, InsUser, UpdDate, UpdUser)
     VALUES
          (1,'Active','Activo','Cuenta Activa',1,GETDATE(),'System',GETDATE(),'System') 
GO

INSERT INTO [Production].[AccountStatus]
          (AccountStatusId, Code, Name, [Description], IsActive, InsDate, InsUser, UpdDate, UpdUser)
     VALUES
          (2,'Finished','Finalizado','Cuenta Finalizada',1,GETDATE(),'System',GETDATE(),'System') 
GO


/*********************ACCOUNT TYPES ******************************/

INSERT INTO [Production].[AccountType]
           ([Code],[Name],[Description],[IsActive],[InsDate],[InsUser],[UpdDate],[UpdUser])
     VALUES
           ('ALR','Alarma Residencial','Alarma Residencial',1,GETDATE(),'System',GETDATE(),'System')
GO

INSERT INTO [Production].[AccountType]
           ([Code],[Name],[Description],[IsActive],[InsDate],[InsUser],[UpdDate],[UpdUser])
     VALUES
           ('CRE','Cerco Eléctrico','Cerco Eléctrico',1,GETDATE(),'System',GETDATE(),'System')
GO

INSERT INTO [Production].[AccountType]
           ([Code],[Name],[Description],[IsActive],[InsDate],[InsUser],[UpdDate],[UpdUser])
     VALUES
           ('CCTV','Circuito Cerrado','Circuito Cerrado',1,GETDATE(),'System',GETDATE(),'System')
GO

INSERT INTO [Production].[AccountType]
           ([Code],[Name],[Description],[IsActive],[InsDate],[InsUser],[UpdDate],[UpdUser])
     VALUES
           ('GPS','Rastreo Satelital','Rastreo Satelital',1,GETDATE(),'System',GETDATE(),'System')
GO

/*********************SERVICE TYPE***************************/

INSERT INTO [Production].[ServiceType]
            ([ServiceTypeId],Code, [Name],[Descriptión],[IsActive],[InsDate],[InsUser],[UpdDate],[UpdUser])
     VALUES
         (1,'Preventive','Preventivo','Mantenimiento Preventivo',1,GETDATE(),'System',GETDATE(),'System')
GO

INSERT INTO [Production].[ServiceType]
            ([ServiceTypeId],Code, [Name],[Descriptión],[IsActive],[InsDate],[InsUser],[UpdDate],[UpdUser])
     VALUES
         (2,'Corrective','Correctivo','Mantenimiento Correctivo',1,GETDATE(),'System',GETDATE(),'System')
GO

INSERT INTO [Production].[ServiceType]
            ([ServiceTypeId],Code, [Name],[Descriptión],[IsActive],[InsDate],[InsUser],[UpdDate],[UpdUser])
     VALUES
         (3,'Install','Instalación','Servicio de Instalación',1,GETDATE(),'System',GETDATE(),'System')
GO

/*************************Payment Periodes***********************************/

INSERT INTO [Production].[PaymentPeriod]
           (PaymentPeriodId, [Name],[Description],[IsActive],[InsDate],[InsUser],[UpdDate],[UpdUser])
     VALUES
           (1,'Mensual','Una vez cada mes',1,GETDATE(),'System',GETDATE(),'System')
GO

INSERT INTO [Production].[PaymentPeriod]
           (PaymentPeriodId, [Name],[Description],[IsActive],[InsDate],[InsUser],[UpdDate],[UpdUser])
     VALUES
           (2,'Bimestral','Cada dos meses',1,GETDATE(),'System',GETDATE(),'System')
GO

INSERT INTO [Production].[PaymentPeriod]
           (PaymentPeriodId, [Name],[Description],[IsActive],[InsDate],[InsUser],[UpdDate],[UpdUser])
     VALUES
           (3,'Trimestral','Cada tres meses',1,GETDATE(),'System',GETDATE(),'System')
GO

INSERT INTO [Production].[PaymentPeriod]
           (PaymentPeriodId, [Name],[Description],[IsActive],[InsDate],[InsUser],[UpdDate],[UpdUser])
     VALUES
           (4,'Cuatrimestral','Cada cuatro meses',1,GETDATE(),'System',GETDATE(),'System')
GO

INSERT INTO [Production].[PaymentPeriod]
           (PaymentPeriodId, [Name],[Description],[IsActive],[InsDate],[InsUser],[UpdDate],[UpdUser])
     VALUES
           (6,'Semestral','Cada seis meses',1,GETDATE(),'System',GETDATE(),'System')
GO


INSERT INTO [Production].[PaymentPeriod]
           (PaymentPeriodId, [Name],[Description],[IsActive],[InsDate],[InsUser],[UpdDate],[UpdUser])
     VALUES
           (12,'Anual','Una vez al año',1,GETDATE(),'System',GETDATE(),'System')
GO

/*************************Maintenance Periodes***********************************/

INSERT INTO [Production].[MaintPeriod]
           (MaintPeriodId, [Name],[Description],[IsActive],[InsDate],[InsUser],[UpdDate],[UpdUser])
     VALUES
           (1,'Mensual','Una vez cada mes',1,GETDATE(),'System',GETDATE(),'System')
GO

INSERT INTO [Production].[MaintPeriod]
           (MaintPeriodId, [Name],[Description],[IsActive],[InsDate],[InsUser],[UpdDate],[UpdUser])
     VALUES
           (2,'Bimestral','Cada dos meses',1,GETDATE(),'System',GETDATE(),'System')
GO

INSERT INTO [Production].[MaintPeriod]
           (MaintPeriodId, [Name],[Description],[IsActive],[InsDate],[InsUser],[UpdDate],[UpdUser])
     VALUES
           (3,'Trimestral','Cada tres meses',1,GETDATE(),'System',GETDATE(),'System')
GO

INSERT INTO [Production].[MaintPeriod]
           (MaintPeriodId, [Name],[Description],[IsActive],[InsDate],[InsUser],[UpdDate],[UpdUser])
     VALUES
           (4,'Cuatrimestral','Cada cuatro meses',1,GETDATE(),'System',GETDATE(),'System')
GO

INSERT INTO [Production].[MaintPeriod]
           (MaintPeriodId, [Name],[Description],[IsActive],[InsDate],[InsUser],[UpdDate],[UpdUser])
     VALUES
           (6,'Semestral','Cada seis meses',1,GETDATE(),'System',GETDATE(),'System')
GO


INSERT INTO [Production].[MaintPeriod]
           (MaintPeriodId, [Name],[Description],[IsActive],[InsDate],[InsUser],[UpdDate],[UpdUser])
     VALUES
           (12,'Anual','Una vez al año',1,GETDATE(),'System',GETDATE(),'System')
GO
