--CREATING DATABASE:
create database Ecommerce_Application

--CREATING TABLES:
create table Customers(
customer_id int identity(1,1) primary key,
name varchar(80),
email varchar(50),
password varchar(50) 
)

create table Products(
product_id int identity(501,1) primary key,
name varchar(80),
price int,
description varchar(max),
stockQuantity int,
)

create table Cart(
cart_id int identity(101,1) primary key,
customer_id int,
product_id int,
quantity int
constraint Cart_Customers foreign key(customer_id) references Customers(customer_id) on delete cascade,
constraint Cart_Products foreign key(product_id) references Products(product_id) on delete cascade
)

create table Orders(
order_id int identity(1001,1) primary key,
customer_id int,
order_date date,
total_price int,
shipping_address varchar(max)
constraint Orders_Customers foreign key(customer_id) references Customers(customer_id) on delete cascade
)

create table Order_Items( 
order_item_id int identity(1001,1) primary key,
order_id int,
product_id int,
quantity int
constraint Order_Items_Orders foreign key(order_id) references Orders(order_id) on delete cascade,
constraint Order_Items_Products foreign key(product_id) references Products(product_id) on delete cascade
)

alter table Products
add constraint CK_Price
check (price > 0)

alter table Products
add constraint CK_StockQuantity
check (stockQuantity >= 0)

alter table Cart
add constraint CK_quantity
check (quantity > 0)

alter table Orders
add constraint CK_total_price
check (total_price > 0)

alter table Order_Items
add constraint CK_order_Items_quantity
check (quantity > 0)

--INSERT VALUES INT0 TABLES:
insert into Customers (name,email,password)
values
('Vijay','vijay@gmail.com','dw2!nd'),
('Mahesh','mahesh@gmail.com','kdw89'),
('Arun','arun@gmail.com','koai8'),
('Muthu','muthu@gmail.com','adw1k1'),
('Yash','yash@gmail.com','daapd'),
('Dilli','dilli@gmail.com','dwaj45'),
('Sethupathi','sethupathi@gmail.com','d2en2d')

insert into Products(name,price,description,stockQuantity)
values
('Dove Shampoo',80,'New Neem Flavour',10),
('Baby Powder',140,'Increased Protection',12),
('Ink Pen',40,'Fluid Ball',40),
('Samsung S23 Ultra',24000,'High Definition Camera',5),
('Mac M5',1500000,'High end laptop',2),
('Fog Scent',160,'New Flavour',11),
('Tupperware',230,'New Larger Sized Bottle',17)

insert into Cart(customer_id,product_id,quantity)
values
(1,501,1),
(2,502,1), 
(3,503,4),
(4,504,1),
(5,505,1),
(6,506,2),
(7,507,2)

insert into Orders(customer_id,order_date,total_price,shipping_address)
values
(1,'2023-11-01',80,'13,Trichy,TamilNadu'),
(3,'2023-11-05',160,'17,Cochin,Kerala'),
(4,'2023-11-08',24000,'3,Trirupathi,Andhra Pradesh'),
(5,'2023-11-11',1500000,'12,Mumbai,Maharashtra'),
(7,'2023-11-13',460,'4,Pune,Maharashtra')

insert into Order_Items(order_id,product_id,quantity)
values
(1001,501,1),
(1002,503,4),
(1003,504,1),
(1004,505,1),
(1005,507,2)

--QUERY:

--1. createProduct():
insert into Products(name,price,description,stockQuantity)
values
('Hamam Oil',100,'New Sandal Flavour',19)

--2. createCustomer():
insert into Customers (name,email,password)
values
('Ajay','Ajay@gmail.com','dwl90a')

--3. deleteProduct():
delete from Products
where product_id = 508

--4. deleteCustomer():
delete from Customers
where customer_id = 8

--5. addToCart():
insert into Cart(customer_id,product_id,quantity)
values
(8,508,2)

--6. removeFromCart():
delete from Cart
where customer_id = 9 and product_id = 506

--7. getAllFromCart(Customer customer):

select Products.product_id,name,price,description,total from Products inner join (select customer_id,product_id,sum(quantity) as total from Cart group by customer_id,product_id) C
on Products.product_id = C.product_id
where customer_id =  1

--8. placeOrder(Customer customer, List<Map<Product,quantity>>, string shippingAddress):
insert into Orders(customer_id,order_date,total_price,shipping_address)
values
(4,'2023-11-30',200,'13,Mysore,Karnataka')

insert into Order_Items(order_id,product_id,quantity)
values
(1006,508,2)

--9. getOrdersByCustomer():
insert into Order_Items(order_id,product_id,quantity)
values
(1006,504,2)

select customer_id,O.order_id,Products.product_id,name,price,quantity from Products inner join (select customer_id,Orders.order_id,product_id,quantity from Orders inner join Order_Items
on Orders.order_id = Order_Items.order_id) O 
on Products.product_id = O.product_id
where customer_id = 8
