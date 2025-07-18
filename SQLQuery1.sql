Insert Into Usuario Values('Erick','Arroyo','Velasco','1998-12-21','erick@hotmail.com','JAK','12345','','')
Insert Into Usuario Values('Isabel','Posadas','Camargo','2001-12-17','isa@hotmail.com','Isa','12345','','')
Insert Into Usuario Values('Paulina','Arroyo','Velasco','1995-06-01','pau@hotmail.com','Pau','12345','','')
Insert Into Contacto Values('Juan','Peres','','1998-02-01','','555555555','juan_p@yahoo.com','1')
Insert Into Contacto Values('Anita','La','Huerfanita','1998-01-02','','555555556','anita_h@yahoo.com','1')
Insert Into Contacto Values('Sara','Posadas','Camrgo','2005-11-02','','555555556','sara@yahoo.com','2')
Insert Into RedSocial Values('Facebook')
Insert Into RedSocial Values('Instagram')
Insert Into RedSocial Values('TikTok')
Insert Into RedSocial Values('WhatsApp')
Insert Into ContactoRedSocial Values ('1','1','www.facebook.com\Juan')
Insert Into ContactoRedSocial Values ('1','2','www.instagram.com\Juan')
Insert Into ContactoRedSocial Values ('2','1','www.facebook.com\Anita')
Insert Into ContactoRedSocial Values ('2','2','www.instagram.com\Anita')
select*from usuario
select*from contacto
select*from RedSocial
select*from ContactoRedSocial

--delete ContactoRedSocial  -----NUNCA USAER TRUNCATE TABLE EN TABLAS RELACIONADAS-----
--delete contacto
--delete RedSocial
--delete usuario

select r.Nombre as RedSocial , cr.UrlPerfil
from Usuario as u 
inner join Contacto as c on u.IdUsuario = c.IdUsuario
inner join ContactoRedSocial as cr on c.IdContacto = cr.IdContacto 
right join RedSocial as r on cr.IdRedSocial = r.IdRedSocial
where u.IdUsuario = 1 and c.IdContacto = 2

--Perfil--
ALTER PROCEDURE spPerfilSocial
@IdUsuario int,
@IdContacto int
as
BEGIN
select cr.IdContactoRedSocial as idPerfil, cr.IdContacto, cr.IdRedSocial, r.Nombre as RedSocial , cr.UrlPerfil
from RedSocial as r 
left join ContactoRedSocial as cr on r.IdRedSocial = cr.IdRedSocial and cr.IdContacto = @IdContacto
left join Contacto as c on c.IdContacto  = cr.IdContacto and c.IdUsuario = @IdUsuario
left join Usuario as u on u.IdUsuario = c.IdUsuario
order by r.Nombre asc
END

exec spPerfilSocial 1, 5