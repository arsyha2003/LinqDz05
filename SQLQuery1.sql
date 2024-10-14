﻿﻿
use master 
go
create database CountriesV2
go
use CountriesV2
go


create table PartsOfTheWorld(
	ID int identity(1,1) not null primary key,
	Name nvarchar(30) constraint UQ_PartOC unique
)
create table Country(
	ID int identity(1,1) not null primary key,
	PartOfTheWorldId int constraint FK_POFWLI foreign key
								(PartOfTheWorldId) references PartsOfTheWorld(ID),
	Name nvarchar(30) not null,
	TotalCountOfPersons int not null,
	SquareOfCountry int not null
)
create table BigSities(
	ID int identity(1,1) not null primary key,
	CountOfPersons int not null,
	Name nvarchar(30),
	CountryId int constraint FK_CID foreign key
								(CountryId) references Country(ID),
)
create table CapitalsOfCountries(
	ID int identity(1,1) not null primary key,
	SityId int constraint FK_CI22D foreign key
								(SityId) references BigSities(ID),
	CountryId int constraint FK_CI2D foreign key
								(CountryId) references Country(ID),
)
insert into PartsOfTheWorld(Name)
values
('Africa'),
('Asia'),
('JapanIslands')
insert into Country(PartOfTheWorldId,Name,TotalCountOfPersons,SquareOfCountry)
values
(1,'UAR',100000,12331),
(2,'China',1300000,777721),
(3,'Japan',5300000,7721)
insert into BigSities(CountOfPersons, Name,CountryId)
values
(1123000, 'Shanhai',2),
(1312300, 'Tyanczin',2),
(5212300, 'Piechin',2),
(3133000, 'Guanjou',3),
(4321430, 'Tokyo',3),
(2133000, 'Kyoto',3),
(3233000, 'Osaka',3),
(6733000, 'Kair',1),
(871300, 'Luanda',1),
(243000, 'UAR',1)
insert into CapitalsOfCountries(SityId,CountryId)
values
(3,1),
(5,2),
(10,3)

select * from CapitalsOfCountries
select * from BigSities
select * from PartsOfTheWorld
select * from Country