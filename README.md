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

Database diagram (in portuguese for while):
![image](https://user-images.githubusercontent.com/42729316/128185865-2a4ef67a-8e64-4649-8ff6-91936ed654c4.png)
