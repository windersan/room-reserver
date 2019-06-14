Create Procedure spReservation
@firstname nvarchar(50),
@lastname nvarchar(50),
@employeeid int,
@email nvarchar(50),
@start datetime,
@finish datetime,
@comment nvarchar(200),
@roomid int
as
Begin
	if not exists (select id from employee where id = @employeeid)
	Begin
		insert into employee (id, lastname, firstname, email)
		values (@employeeid, @lastname, @firstname, @email)
	End
	insert into reservation ([start], finish, roomid, employeeid, comment)
	values (@start, @finish, @roomid, @employeeid, @comment)
End






