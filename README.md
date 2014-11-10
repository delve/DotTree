DotTree
=======

A family tree exercise in C# with .Net MVC &amp; Entity Framework



ToDo
====
User logins (obvious)
Family access by user login
CRUD forms for user access to data
Remove the foreign key to family table in People (ties into a lot of different things)
Implement EF loading method selection in IPersonRepository
Add family membership delete functionality in EditFamily
Add family membership add functionality to EditPerson
Add family membership delete functionality to EditPerson
Homepage

Possible features
=================
Consider allowing people to be added as family members from EditFamily 
   This doesn't make a great deal of sense except in a multi-user-multi-family context. A site admin wouldn't be makign this sort of change
     The only benefit is cross adding family members between multiple familes in a single users data context

.