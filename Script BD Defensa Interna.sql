Create database BDProDefInt
go
use BDProDefInt
go
--------------------------------------------------Tablas-------------------------------------
Create table Cliente(
idCliente int not null identity primary key ,
CI int unique not null,
Nombre varchar(50) not null,
Edad int not null,
estadoCivil varchar(50) not null,
Profesion varchar(50) not null,
Email varchar(50) not null,
Telefono int not null
)
go
Create table Roles(
idRol int not null identity primary key,
Nombre varchar (50) not null,
Descripcion varchar (100) not null
)
go
Create table tipoPoliza(
idTipoPoliza int not null identity primary key,
NombreTP varchar(50) not null,
Descripcion varchar(100) not null
)
go
Create table Funcionario(
idFuncionario int not null identity primary key,
Nombre varchar(50) not null,
Login varchar(30) not null,
Password varchar(500) not null,
fechaPass datetime  not null,
idRol int not null Foreign key references Roles(idRol)
)
go
Create table polizaVehicular(
idPoliza int not null identity primary key,
fechaInicio datetime null,
fechaFin datetime null,
Tipo varchar (50) not null,
Modelo varchar (50) not null,
Aņo int not null,
Color varchar(50) not null,
Placa varchar(50) not null,
Chasis varchar(200) unique not null,
Costo float null,
Estado varchar(50) not null,
idFuncionario int not null Foreign key references Funcionario (idFuncionario),
idCliente int not null Foreign key references Cliente(idCliente),
idTipoPoliza int not null Foreign key references TipoPoliza(idTipoPoliza)
)
go
Create table Siniestro(
idSiniestro int not null identity primary key,
fechaInicio date null,
Lugar varchar (50) not null,
Detalle varchar (50) not null,
daņosMateriales varchar (50) not null,
daņosPersonales varchar (50) not null,
fechaFin date null,
Fotografia varbinary(Max) not null,
Costo float null,
Estado varchar(50) null,
DescripcionCierre varchar (300) null,
idPoliza int null  Foreign key references polizaVehicular(idPoliza),
idFuncionario int null Foreign key references Funcionario (idFuncionario)
)
go
Create table DetalleGasto(
idDetalleGasto int not null identity primary key,
Sepelio float null,
imagenSepelio varbinary(Max) null,
atencionMedica float null,
imagenAtMedic varbinary(Max) null,
daņosPropiedad float null,
imagenDProp varbinary(Max) null,
muerteAccidente float null,
imagenMuerteAcc varbinary(Max) null,
perdidaTotal float null,
imagenPerdTotal varbinary(Max) null,
reparacionVehicular float null,
imagenRepVehicular varbinary(Max) null,
idSiniestro int not null Foreign key references Siniestro(idSiniestro)
)
go
Create table Bitacora(
codbit int not null identity primary key, 
descripcion varchar(300) not null,
fecha datetime not null,
terminal varchar(100) not null,
usuario varchar(100) not null,
aplicacion varchar(100) not null
)
----------------------------------------------------------------------------------------
use master
go
drop database BDProDefInt
-------------------------------------------------Datos--------------------------------------------------

Insert into Roles values ('Admin','El administrador del sistema, acceso a todo')
Insert into Roles values ('Agente Poliza','Personal encargado de los clientes y polizas')
Insert into Roles values ('Agente Sinietros','Personal encargado de los siniestros y sus gastos')
Insert into Roles values ('Accionista','Realiza la supervision de los agentes y visualiza los reportes')

Insert into tipoPoliza values ('Responsabilidad Civil','Todos los daņos a terceros fuera y dentro del auto')
Insert into tipoPoliza values ('Perdida Total','Cubre la perdida total por daņo o robo del vehiculo')
Insert into tipoPoliza values ('Todo Riesgo','Cubre los daņos parciales al vehiculo, choques, o robos menores')

Insert into Funcionario values ('Jose Solano Romero','Admin','Admin',Getdate(),1)
Insert into Funcionario values ('Carlos Moreno Aranda','CMoreno','CMoreno',Getdate(),2)
Insert into Funcionario values ('Maria Suarez Aguiltar','MSuarez','MSuarez',Getdate(),3)
Insert into Funcionario values ('Pablo Medina Perez','PMedina','PMedina',Getdate(),4)

Insert into Cliente values (8177406,'Jose Solano Romero',22,'Soltero','Ingeniero en Sistema','jsolanor1994@gmail.com',70200974)
--------------------------------------------------------------------------------------------------------------
Create trigger tg_Fecha_Poliza on polizaVehicular for insert
as
begin
declare @ID as int

declare @FecIni as datetime
declare @FecFin as datetime
declare @CostoTipo as float
declare @CostoMarca as float
declare @CostoAņo as float
declare @CostoTotal as float
declare @Estado as varchar(50)

declare @FecIniAcc as datetime
declare @FecFinAcc as datetime
declare @CostoAcc as float

declare @Tipo as varchar(50)
declare @Marca as varchar(50)
declare @Aņo as int

set @ID = (select idPoliza from inserted)
set @FecIniAcc = (select fechaInicio from inserted)
set @FecFinAcc = (select fechaFin from inserted)
set @CostoAcc = (select Costo from inserted)

set @Tipo = (select Tipo from inserted)
set @Marca = (select Modelo from inserted)
set @Aņo = (select Aņo from inserted)

set @Estado = 'Habilitado'

if(@FecFinAcc is null and @FecFinAcc is null and @CostoAcc is null )
	begin
		set @FecIni = getdate()
		set @FecFin = DATEADD(YEAR,1,@FecIni)
	---------------------------------------------
		if(@Tipo = 'Auto' or @Tipo = 'Vagoneta')
			begin
				set @CostoTipo = 50
			end
		else
			begin
				set @CostoTipo = 100
			end
	 --------------------------------
		if(@Marca = 'Toyota' or @Marca = 'Suzuki' or @Marca = 'Nissan')
			begin
				set @CostoMarca = 100
			end
		else
			begin
			if(@Marca = 'Volkswagen' or @Marca = 'Renaut' or @Marca = 'Ford')
				begin
					set @CostoMarca = 50
				end
			else
				begin
					set @CostoMarca = 25
				end
			end
	------------------------------------------
		if(@Aņo between 1980 and 2000)
			begin
				set @CostoAņo = 50
			end
		else
			begin
				set @CostoAņo = 100
			end
	-----------------------------------------
		set @CostoTotal = @CostoAņo+@CostoMarca+@CostoTipo
		
		update polizaVehicular set fechaInicio = @FecIni, fechaFin = @FecFin, Costo = @CostoTotal, Estado = @Estado where idPoliza = @ID
	end
end

-----prueba de trigger de poliza------------
insert into polizaVehicular values (null,null,'Auto','Toyota',1994,'rojo','231-sds','caw54w825s',null,'',1,1,1)

delete from polizaVehicular where idPoliza = 1
DBCC CHECKIDENT (polizaVehicular, RESEED, 0)

select * from polizaVehicular

drop trigger tg_Fecha_Poliza

------------------------------------------------------------------------------------------------------------
Create trigger tg_Fecha_Siniestro on Siniestro for insert
as
begin
declare @ID as int

declare @FecIni as datetime
declare @FecFin as datetime
declare @TipoPoliza as varchar(50)
declare @CostoTotal as float
declare @IDPoliza as int

declare @FecIniAcc as datetime
declare @FecFinAcc as datetime
declare @CostoAcc as float

set @ID = (select idSiniestro from inserted)
set @IDPoliza = (select idPoliza from inserted)
set @FecIniAcc = (select fechaInicio from inserted)
set @FecFinAcc = (select fechaFin from inserted)
set @CostoAcc = (select Costo from inserted)



if(@FecFinAcc is null and @FecFinAcc is null and @CostoAcc is null )
	begin
		set @FecIni = getdate()
		set @TipoPoliza = (select tp.NombreTP from Siniestro s inner join polizaVehicular pv on pv.idPoliza = s.idPoliza 
															   inner join tipoPoliza tp on tp.idTipoPoliza = pv.idTipoPoliza 
															   where s.idPoliza = @ID)

		if(@TipoPoliza = 'Responsabilidad Civil')
			begin
				set @CostoTotal = 100
			end
		else
			begin
			if(@TipoPoliza = 'Perdida Total')
				begin
					set @CostoTotal = 200
				end
			else
				begin
					set @CostoTotal = 300
				end
			end
		
		update Siniestro set fechaInicio = @FecIni, Costo = @CostoTotal where idSiniestro = @ID
	end
end
-----prueba de trigger de poliza------------
insert into Siniestro values (null,'Av San Martin','Choque contra otro vehiculo de frente','Frente del vehiculo','Trauma en la cabeza',null,'',null,null,null,2,1)

select * from Funcionario
drop trigger tg_Fecha_Siniestro
------------------------------------------------Views---------------------------------------------
-------------------------------------------------------View Reporte 1-------------------------------------------
Create View View_Report1
as
select pv.idPoliza as IDPoliza, Tipo, Modelo, Aņo, Color, Placa, Chasis, NombreTP from polizaVehicular  pv
		inner join tipoPoliza tp on pv.idTipoPoliza = tp.idTipoPoliza
--------------------------------------------------------------View Reporte 2--------------------------------------------
Create View View_Report2
as
select idSiniestro as IDSiniestro, fechaInicio as FechaInicio, Lugar, Detalle, daņosMateriales as DaņosMateriales, daņosPersonales as DaņosPersonales, Fotografia, Costo, Estado from Siniestro where Estado = 'Abierto'
--------------------------------------------------------------View Reporte 3----------------------------------------------
Create View View_Report3
as
select idSiniestro as IDSiniestro, fechaInicio as FechaInicio, fechaFin as FichaFin, Lugar, Detalle, daņosMateriales as DaņosMateriales, daņosPersonales as DaņosPersonales, Fotografia, Costo, Estado, DescripcionCierre from Siniestro 
--------------------------------------------------------------View Reporte 4---------------------------------------------
Create View View_Report4
as
Select * from detalleGasto where idSiniestro = 1
--------------------------------------------------------------Triggers para Bitacora-----------------------------------------------------------------------------------------
create trigger tg_alta_Cliente on Cliente for insert
as
declare @Nom as varchar(30)
declare @Codclie as int
set @Nom = (select Nombre from inserted)
set @Codclie = (select idCliente from inserted)
insert into Bitacora values('Se guardo Cliente: '+@Nom+' con id: '+convert(char(10),@Codclie),GETDATE(),HOST_NAME(),SYSTEM_USER,APP_NAME())
go
create trigger tg_baja_Cliente on Cliente for delete
as
declare @Nom as varchar(30)
declare @Codclie as int
set @Nom = (select Nombre from inserted)
set @Codclie = (select idCliente from inserted)
insert into Bitacora values('Se elimino Cliente: '+@Nom+' con id: '+convert(char(10),@Codclie),GETDATE(),HOST_NAME(),SYSTEM_USER,APP_NAME())
go
create trigger tg_Mod_Cliente on Cliente for update
as
declare @Nom as varchar(30)
declare @Codclie as int
set @Nom = (select Nombre from inserted)
set @Codclie = (select idCliente from inserted)
insert into Bitacora values('Se modifico Cliente: '+@Nom+' con id: '+convert(char(10),@Codclie),GETDATE(),HOST_NAME(),SYSTEM_USER,APP_NAME())

create trigger tg_alta_Poliza on polizaVehicular for insert
as
declare @Nom as varchar(30)
declare @Codclie as int
set @Nom = (select Chasis from inserted)
set @Codclie = (select idPoliza from inserted)
insert into Bitacora values('Se guardo Poliza Vehicular con Chasis: '+@Nom+' con id: '+convert(char(10),@Codclie),GETDATE(),HOST_NAME(),SYSTEM_USER,APP_NAME())
go
create trigger tg_baja_Poliza on polizaVehicular for delete
as
declare @Nom as varchar(30)
declare @Codclie as int
set @Nom = (select Chasis from inserted)
set @Codclie = (select idPoliza from inserted)
insert into Bitacora values('Se elimino Poliza Vehicular con Chasis: '+@Nom+' con id: '+convert(char(10),@Codclie),GETDATE(),HOST_NAME(),SYSTEM_USER,APP_NAME())
go
create trigger tg_Mod_Poliza on polizaVehicular for update
as
declare @Nom as varchar(30)
declare @Codclie as int
set @Nom = (select Chasis from inserted)
set @Codclie = (select idPoliza from inserted)
insert into Bitacora values('Se modifico Poliza Vehicular con Chasis: '+@Nom+' con id: '+convert(char(10),@Codclie),GETDATE(),HOST_NAME(),SYSTEM_USER,APP_NAME())

create trigger tg_alta_Siniestro on Siniestro for insert
as
declare @Nom as varchar(50)
declare @Nom2 as varchar(50)
declare @Codclie as int
declare @CodVe as int
set @Nom = (select Lugar from inserted)
set @Codclie = (select idSiniestro from inserted)
set @CodVe = (select idPoliza from inserted)
set @Nom2 = (select Detalle from inserted)
insert into Bitacora values('Se guardo Siniestro en el Lugar: '+@Nom+' con detalle: '+@Nom2+', con el ID de Poliza:  '+convert(char(10),@CodVe)+', con el Id: '+convert(char(10),@Codclie),GETDATE(),HOST_NAME(),SYSTEM_USER,APP_NAME())
go
create trigger tg_baja_Siniestro on Siniestro for delete
as
declare @Nom as varchar(50)
declare @Nom2 as varchar(50)
declare @Codclie as int
declare @CodVe as int
set @Nom = (select Lugar from inserted)
set @Codclie = (select idSiniestro from inserted)
set @CodVe = (select idPoliza from inserted)
set @Nom2 = (select Detalle from inserted)
insert into Bitacora values('Se Elemino Siniestro en el Lugar: '+@Nom+' con detalle: '+@Nom2+', con el ID de Poliza:  '+convert(char(10),@CodVe)+', con el Id: '+convert(char(10),@Codclie),GETDATE(),HOST_NAME(),SYSTEM_USER,APP_NAME())
go
create trigger tg_Mod_Siniestro on Siniestro for update
as
declare @Nom as varchar(50)
declare @Nom2 as varchar(50)
declare @Codclie as int
declare @CodVe as int
set @Nom = (select Lugar from inserted)
set @Codclie = (select idSiniestro from inserted)
set @CodVe = (select idPoliza from inserted)
set @Nom2 = (select Detalle from inserted)
insert into Bitacora values('Se Modifico Siniestro en el Lugar: '+@Nom+' con detalle: '+@Nom2+', con el ID de Poliza:  '+convert(char(10),@CodVe)+', con el Id: '+convert(char(10),@Codclie),GETDATE(),HOST_NAME(),SYSTEM_USER,APP_NAME())


create trigger tg_alta_Gastos on DetalleGasto for insert
as
declare @CodSin as int
declare @Codclie as int
set @CodSin = (select idSiniestro from inserted)
set @Codclie = (select idDetalleGasto from inserted)
insert into Bitacora values('Se guardo Detalle de Gastos del siniestro con id : '+convert(char(10),@CodSin)+', con id del Gasto: '+convert(char(10),@Codclie),GETDATE(),HOST_NAME(),SYSTEM_USER,APP_NAME())
go
create trigger tg_baja_Gastos on DetalleGasto for delete
as
declare @CodSin as int
declare @Codclie as int
set @CodSin = (select idSiniestro from inserted)
set @Codclie = (select idDetalleGasto from inserted)
insert into Bitacora values('Se elimino Detalle de Gastos del siniestro con id : '+convert(char(10),@CodSin)+', con id del Gasto: '+convert(char(10),@Codclie),GETDATE(),HOST_NAME(),SYSTEM_USER,APP_NAME())
go
create trigger tg_Mod_Gastos on DetalleGasto for update
as
declare @CodSin as int
declare @Codclie as int
set @CodSin = (select idSiniestro from inserted)
set @Codclie = (select idDetalleGasto from inserted)
insert into Bitacora values('Se modifico Detalle de Gastos del siniestro con id : '+convert(char(10),@CodSin)+', con id del Gasto: '+convert(char(10),@Codclie),GETDATE(),HOST_NAME(),SYSTEM_USER,APP_NAME())
-------------------------------------------------------------------------------------------------Tareas Programadas----------------------------------------------------------------

create procedure SPBackupTotal
as
	begin	
		declare @Fec date
		declare @nom varchar(50)	
		set @Fec = SYSDATETIME()
		set @nom = 'C:\FINAL\CopyBDProDefIntTotal - '+(CONVERT(varchar(30),@Fec))+'.bak' 
		backup database BDProDefInt
		to Disk = @nom
		with CHECKSUM
end
create procedure SPBackupDiferencial
as
	begin	
		declare @Fec date
		declare @nom varchar(50)	
		set @Fec = SYSDATETIME()
		set @nom = 'C:\FINAL\CopyBDProDefIntTotal - '+(CONVERT(varchar(30),@Fec))+'.bak'
		backup database BDProDefInt
		to Disk = @nom
		with DIFFERENTIAL
end
------------------------------------------------------------------------------------------------------------------------------------------------
