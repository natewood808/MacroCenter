select * from __EFMigrationsHistory

select * from Products

delete from products where category = 'Graphics Cards'
delete from products where category = 'Processors'
delete from products where category = 'Motherboards'

delete from Products where Category = 'Watersports'
delete from Products where Category = 'Chess'
delete from Products where Category = 'Soccer'

insert into Products (Name, Description, Price, Category) values
('Kayak', 'A boat for one person', 275.00, 'Watersports'),
('Lifejacket', 'Protective and fashionable', 48.95, 'Watersports'),
('Soccer Ball', 'FIFA-approved size and weight', 19.50, 'Soccer'),
('Corner Flags', 'Give your playing field a professional touch', 34.95, 'Soccer'),
('Stadium', 'Flat-packed 35,000-seat stadium', 79500.00, 'Soccer'),
('Thinking Cap', 'Improve brain efficiency by 75%', 16.00, 'Chess'),
('Unsteady Chair', 'Secretly give your opponent a disadvantage', 29.95, 'Chess'),
('Human Chess Board', 'A fun game for the family', 75.00, 'Chess'),
('Bling-Bling King', 'Gold-plated, diamond-studded King', 1200.00, 'Chess')

insert into products (Name, Description, Price, Category, ImageString) Values 
('MVIDIA GeekForce RTX 9990 TI', 'The next-next-generation of graphics!', 4999.00, 'Graphics Cards', '~/Images/graphicsCard1.jpg'),
('Outel Core i10-11000K', 'Processing speeds have never been faster!', 48.95, 'Processors', '~/Images/p2.jpg'),
('EmSI GeekForce RTX 10000', 'Just buy the MVIDIA RTX 9990 TI.', 9999.00, 'Graphics Cards', '~/Images/graphicsCard2.jpg'),
('Megabyte GTX 1060', 'The subsidiary of Gigabyte serving up 2018 graphics!', 300.00, 'Graphics Cards', '~/Images/graphicsCard3.jpg'),
('BMD R99', 'The only thing better than legacy software is legacy hardware!', 99999.99, 'Graphics Cards', '~/Images/graphicsCard4.jpg'),
('BMD Ryzen 8 5600X', 'The lost addition to the Ryzen family.', 1299.49, 'Processors', '~/Images/p1.jpg'),
('Outel 4004', 'Because size does NOT matter!', 29.95, 'Processors', '~/Images/p3.jpg'),
('EmSI A550 Tomahawk', 'Warning not a motherboard, actually just an axe.', 75.00, 'Motherboards', '~/Images/mb1.jpg'),
('AZeus TUF GAMING X570-PlUS', 'Bringing some tough competition to the motherboard scene.', 1200.00, 'Motherboards', '~/Images/mb2.jpg')