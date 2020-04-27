
alter table Item 
add constraint FK_Customer_Items foreign key (SellerID)
references Customer (SellerID)
on delete no action 
on update no action;







