using System;
using Microsoft.OpenApi.Extensions;
using Restaurant.API.Enums;

namespace Restaurant.API.Exceptions
{
    public class NotFoundException : Exception
    {
        public string EntityName { get; }
        public string Identifier { get; }

        public NotFoundException(EntityType enumEntityType, string identifier) : base ($"Could not find {enumEntityType.GetDisplayName} with id {identifier}")
        {
            EntityName = enumEntityType.GetDisplayName();
            Identifier = identifier;
        }
    }
}