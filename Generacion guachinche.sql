create database guachinche;
go

use guachinche;
go

create table plato(
	PlatoId integer primary Key identity,
	Nombre varchar(50) not null,
	Descripcion varchar(200) null,
	tipoId integer not null
);
go

create table restaurante(
	RestauranteId integer primary Key identity,
	Nombre varchar(50) not null,
	Rest_Url varchar(100) null,
	telefono char(9) null,
	valoracion integer,
	Id_tipo integer not null,
	zonaId integer not null
);
go

create table plato_restaurante(
	id integer primary key identity,
	plato_Id integer not null,
	restaurante_Id integer not null,
	valoracion integer null,
	activo bit not null
);
go

create table zona(
	Zona_id integer primary key identity,
	nombre varchar(50) not null,
	descripcion varchar(200) null
);
go

create table tipo(
	id integer primary key identity,
	nombre varchar(40) not null
);
go

create table tipoRestaurante(
	id integer primary key identity,
	nombre varchar(40) not null
);
go

--Creacion de las foreign keys
alter table restaurante
add constraint fk_tipoRest
foreign Key(Id_tipo) references tipoRestaurante(id) ON DELETE CASCADE ON UPDATE CASCADE;

create index ifk_restTipoRest on restaurante (Id_tipo);
go

alter table plato
add constraint fk_tipo
foreign key(tipoId) references tipo(id) ON DELETE CASCADE ON UPDATE CASCADE;

create index ifk_PlatoTipo on plato (tipoId);
go

alter table plato_restaurante
add constraint fk_restaurante
foreign key(restaurante_Id) references restaurante(RestauranteId) ON DELETE CASCADE ON UPDATE CASCADE;
go

alter table plato_restaurante
add constraint fk_plato
foreign key(plato_Id) references plato(PlatoId) ON DELETE CASCADE ON UPDATE CASCADE;
go



alter table restaurante
add constraint fk_zona
foreign key(zonaId) references zona(Zona_id) ON DELETE CASCADE ON UPDATE CASCADE;

create index ifk_zonaRest on restaurante (zonaId);
go


--Informacion de las zonas
insert into zona(nombre,descripcion) values('Zona norte','Esta es una de las mejores partes de la isla para comer platos tipicos canarios');
insert into zona(nombre,descripcion) values('Zona Sur','Esra zona de la isla se caracteriza por su variedad en pescados');
insert into zona(nombre,descripcion) values('Zona metropolitana','Zona perteneciente a la capital de la isla y sus medianias');
go

--Informacion de tipos de platos
insert into tipo(nombre) values('Carnes');
insert into tipo(nombre) values('Pescados y Mariscos');
insert into tipo(nombre) values('Entrantes');
insert into tipo(nombre) values('Postres');
insert into tipo(nombre) values('Sopas y potajes');
insert into tipo(nombre) values('Guarniciones');
go

--informacion de los tipos de restaurantes
insert into tipoRestaurante(nombre) values ('Especialidad en Carnes');
insert into tipoRestaurante(nombre) values ('Especialidad en Pescados');
insert into tipoRestaurante(nombre) values ('Platos variados');
go

--Informacion de los platos
insert into plato(Nombre,Descripcion,tipoId,ImagenUrl) values ('Papas arrugadas con mojo.','Papas cocidas con piel y sal, acompañadas de una salsa picante llamada mojo',6,'img/platos/papa_arrugadas_con_mojo.jpg');
insert into plato(Nombre,Descripcion,tipoId,ImagenUrl) values ('Conejo en salmorejo','Guiso de conejo en una salsa de ajo, vinagre y especias',1,'img/platos/conejo_salmorejo.jpg');
insert into plato(Nombre,Descripcion,tipoId,ImagenUrl) values ('Escaldón de gofio','Plato hecho con gofio, un tipo de harina de cereales, y caldo caliente',3,'img/platos/escaldon.jpg');
insert into plato(Nombre,Descripcion,tipoId,ImagenUrl) values ('Potaje de berros','Guiso hecho con berros, papas, calabaza, garbanzos y carne de cerdo',5,'img/platos/Potaje_de_berros.jpg');
insert into plato(Nombre,Descripcion,tipoId,ImagenUrl) values ('Bienmesabe','Postre hecho con almendras molidas, miel y huevo.',4,'img/platos/Bienmesabe.png');
insert into plato(Nombre,Descripcion,tipoId,ImagenUrl) values ('Sancocho canario.','Guiso de pescado salado, papas, batata y cebolla',2,'img/platos/Sancocho.jpg');
insert into plato(Nombre,Descripcion,tipoId,ImagenUrl) values ('Ropa vieja','Carne desmenuzada, papas, garbanzos y pimiento',1,'img/platos/ropa_vieja.jpg');
insert into plato(Nombre,Descripcion,tipoId,ImagenUrl) values ('Puchero canario','Guiso hecho con carne, garbanzos, papas, batata, zanahoria y col',5,'img/platos/puchero.jpg');
insert into plato(Nombre,Descripcion,tipoId,ImagenUrl) values ('Cherne en adobo','Pescado adobado con ajo, vinagre y pimiento',2,'img/platos/cherne.jpg');
insert into plato(Nombre,Descripcion,tipoId,ImagenUrl) values ('Carajacas','Plato hecho con tripas de cerdo y salsa picante',1,'img/platos/carajacas.jpg');
insert into plato(Nombre,Descripcion,tipoId,ImagenUrl) values ('Conejo en saltao','Guiso de conejo con papas, cebolla y pimiento',1,'');
insert into plato(Nombre,Descripcion,tipoId,ImagenUrl) values ('Escabeche de pescado','Pescado marinado con vinagre, cebolla, ajo y especias.',2,'img/platos/escabeche.jpg');


--Insertar restaurantes

insert into restaurante(zonaId,Nombre,Rest_Url,telefono,valoracion,Id_tipo) values(1,'Los Rodeos','','922111333',3,1);
insert into restaurante(zonaId,Nombre,Rest_Url,telefono,valoracion,Id_tipo) values(1,'Los Toneles','img/restaurantes/Los_Toneles.jpg','92200232',2,1);
insert into restaurante(zonaId,Nombre,Rest_Url,telefono,valoracion,Id_tipo) values(1,'Casa Tomás','img/restaurantes/Casa_tomas.jpg','922111333',1,1);
insert into restaurante(zonaId,Nombre,Rest_Url,telefono,valoracion,Id_tipo) values(1,'El Nervioso','img/restaurantes/EL_Nervioso.jpg','922111333',5,1);
insert into restaurante(zonaId,Nombre,Rest_Url,telefono,valoracion,Id_tipo) values(2,'Rancho de Nino','img/restaurantes/Rancho_de_Nino.jpg','922111333',4,3);
insert into restaurante(zonaId,Nombre,Rest_Url,telefono,valoracion,Id_tipo) values(1,'El Fonil','img/restaurantes/El_Fonil.jpg','922111333',1,1);
insert into restaurante(zonaId,Nombre,Rest_Url,telefono,valoracion,Id_tipo) values(1,'El Moral','img/restaurantes/El_Moral.jpg','922111333',4,1);
insert into restaurante(zonaId,Nombre,Rest_Url,telefono,valoracion,Id_tipo) values(3,'La cuadra del Palmero','img/restaurantes/Cuadra_del_Palmero.jpg','922111333',5,1);
insert into restaurante(zonaId,Nombre,Rest_Url,telefono,valoracion,Id_tipo) values(2,'El empedrado','img/restaurantes/el_empedrado.jpg','922111333',2,1);
insert into restaurante(zonaId,Nombre,Rest_Url,telefono,valoracion,Id_tipo) values(2,'Los pinchitos','img/restaurantes/Los_Pinchitos.jpg','922111333',3,2);
insert into restaurante(zonaId,Nombre,Rest_Url,telefono,valoracion,Id_tipo) values(1,'Casa Lito','img/restaurantes/Casa_lito.jpg','922111333',2,3);
insert into restaurante(zonaId,Nombre,Rest_Url,telefono,valoracion,Id_tipo) values(3,'El regulo','img/restaurantes/El_regulo.jpg','922111333',5,1);
insert into restaurante(zonaId,Nombre,Rest_Url,telefono,valoracion,Id_tipo) values(3,'Victor Fernandez gastrobar','','922111333',2,1);
insert into restaurante(zonaId,Nombre,Rest_Url,telefono,valoracion,Id_tipo) values(2,'La Basilica','img/restaurantes/La_Basilica.jpg','922111333',3,1);
insert into restaurante(zonaId,Nombre,Rest_Url,telefono,valoracion,Id_tipo) values(2,'La cofradia del Mar','img/restaurantes/Cofradia.jpg','922111333',4,2);
insert into restaurante(zonaId,Nombre,Rest_Url,telefono,valoracion,Id_tipo) values(2,'El rincon de Juan Carlos','img/restaurantes/Rincon_de_Juan.jpg','922111333',1,2);


--informacion de platosRestaurantes

insert into plato_restaurantes(plato_Id,restaurante_Id,valoracion,activo,PlatoId,RestauranteId) values (1,6,3,1,1,6);
insert into plato_restaurantes(plato_Id,restaurante_Id,valoracion,activo,PlatoId,RestauranteId) values (2,6,2,1,2,6);
insert into plato_restaurantes(plato_Id,restaurante_Id,valoracion,activo,PlatoId,RestauranteId) values (3,6,1,1,3,6);
insert into plato_restaurantes(plato_Id,restaurante_Id,valoracion,activo,PlatoId,RestauranteId) values (4,6,5,1,4,6);
insert into plato_restaurantes(plato_Id,restaurante_Id,valoracion,activo,PlatoId,RestauranteId) values (5,6,4,1,5,6);
insert into plato_restaurantes(plato_Id,restaurante_Id,valoracion,activo,PlatoId,RestauranteId) values (1,2,3,1,1,2);
insert into plato_restaurantes(plato_Id,restaurante_Id,valoracion,activo,PlatoId,RestauranteId) values (1,3,2,1,1,2);
insert into plato_restaurantes(plato_Id,restaurante_Id,valoracion,activo,PlatoId,RestauranteId) values (1,4,3,1,1,4);
insert into plato_restaurantes(plato_Id,restaurante_Id,valoracion,activo,PlatoId,RestauranteId) values (8,6,5,1,8,6);
insert into plato_restaurantes(plato_Id,restaurante_Id,valoracion,activo,PlatoId,RestauranteId) values (9,6,2,1,9,6);
insert into plato_restaurantes(plato_Id,restaurante_Id,valoracion,activo,PlatoId,RestauranteId) values (10,6,3,1,10,6);
insert into plato_restaurantes(plato_Id,restaurante_Id,valoracion,activo,PlatoId,RestauranteId) values (1,14,5,1,1,14);
insert into plato_restaurantes(plato_Id,restaurante_Id,valoracion,activo,PlatoId,RestauranteId) values (12,6,1,1,12,6);
go

--parte nueva
create table user_restaurante(
	id integer primary key identity,
	usuario_Id nvarchar(450) not null,
	restaurante_Id integer not null
);

alter table user_restaurante
add constraint fk_user
foreign key(usuario_Id) references AspNetUsers(Id) on delete cascade on update cascade;


alter table user_restaurante
add constraint fk_userRest
foreign Key(restaurante_Id) references restaurante(RestauranteId) ON DELETE CASCADE ON UPDATE CASCADE;


create table user_plato_restaurante(
	id integer primary key identity,
	usuario_Id nvarchar(450) not null,
	plato_restaurante_Id integer not null
);

alter table user_plato_restaurante
add constraint fk_user_plato
foreign key(usuario_Id) references AspNetUsers(Id) on delete cascade on update cascade;

alter table user_plato_restaurante
add constraint fk_plato_restaurante
foreign Key(plato_restaurante_Id) references plato_restaurante(id) ON DELETE CASCADE ON UPDATE CASCADE;
