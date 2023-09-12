using System;

namespace Restaurant.API.Exceptions
{
    public class NoItemsInStockException : Exception
    {
        public int StockQuantity { get; }
        public string ItemName { get; }

        public NoItemsInStockException(string itemName, int stockQuantity) : base ($"Has no {itemName} in stock. Available stock quantity is {stockQuantity}")
        {
            StockQuantity = stockQuantity;
            ItemName = itemName;
        }
    }
}