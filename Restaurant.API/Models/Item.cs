using System;
using System.ComponentModel.DataAnnotations.Schema;
using Restaurant.API.Controllers;
using Restaurant.API.Exceptions;

namespace Restaurant.API.Models
{
    public class Item
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        public decimal Value { get; set; }
        public int StockQuantity { get; set; }

        internal void CheckStock(int quantity)
        {
            if (StockQuantity < quantity)
                throw new NoItemsInStockException(Name, StockQuantity);
        }

        internal void RemoveFromStock(int quantity)
        {
            CheckStock(quantity);

            StockQuantity -= quantity;
        }

        internal void SetPropertiesToUpdate(Item item)
        {
            if (!string.IsNullOrEmpty(item.Name) && item.Name != Name)
            {
                Name = item.Name;
            }
            if (!string.IsNullOrEmpty(item.Description) && item.Description != Description)
            {
                Description = item.Description;
            }
            if (!string.IsNullOrEmpty(item.Type) && item.Type != Type)
            {
                Type = item.Type;
            }
            if (item.Value != Value)
            {
                Value = item.Value;
            }
            if (item.StockQuantity != StockQuantity)
            {
                StockQuantity = item.StockQuantity;
            }
        }
    }
}
