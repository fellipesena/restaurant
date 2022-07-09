# restaurant

## Restaurant.API
An API where is possible to control stock, items, tables, waiters, open and close bills, and orders financial control. 

Base CRUD:
- Waiters: Create, delete and get all;
- Tables: Create, delete, get all, get by number and get all availables;
- Item: Create, delete, update, get all and get by id;
- Bill: Create (open a new one), update (close), get by table number;
- Order: Create;

Special Behaviors:
- If try to create a new order with an item that does not have the quantity in stock, the order is not created;
- When a new bill is created to a table, the new table status is unavailable, so we cant have two bills to same table;
- When closes a bill, the total bill value considers item value at moment when create the order, so if change item value when have open bill the final value will not change.

## Restaurant.Database
Created using SQLServer and DbUp to version control (migrations).

Database diagram:
![image](![image](https://user-images.githubusercontent.com/42729316/178124341-147c5cca-045f-4290-8ce4-35da8d2a2edb.png))
