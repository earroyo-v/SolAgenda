Insert Into Usuario Values('Erick','Arroyo','Velasco','1998-12-21','erick@hotmail.com','JAK','12345','','')
Insert Into Contacto Values('Juan','Peres','','1998-02-01','','555555555','juan_p@yahoo.com','1')
Insert Into Contacto Values('Anita','La','Huerfanita','1998-01-02','','555555556','anita_h@yahoo.com','1')
Insert Into RedSocial Values('Facebook')
Insert Into RedSocial Values('Instagram')
Insert Into ContactoRedSocial Values ('1','1','test')
Insert Into ContactoRedSocial Values ('1','2','test')
Insert Into ContactoRedSocial Values ('2','1','test2')
Insert Into ContactoRedSocial Values ('2','2','test2')
select*from usuario
select*from contacto
select*from RedSocial
select*from ContactoRedSocial

select u.Nombre as [User], c.Nombre as Contacto, cr.UrlPerfil, r.Nombre as RedSocial from Usuario as u 
inner join Contacto as c on u.IdUsuario = c.IdUsuario
inner join ContactoRedSocial as cr on c.IdContacto = cr.IdContacto 
inner join RedSocial as r on cr.IdRedSocial = r.IdRedSocial