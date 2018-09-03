using SQLite.Net.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StartFinance.Models
{
    public class ShoppingList
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }

        [Unique]
        public string ShopName { get; set; }

        [NotNull]
        public string NameOfItem { get; set; }

        [NotNull]
        public DateTime ShoppingDate { get; set; }

        [NotNull]
        public Decimal PriceQuoted { get; set; }

    }
}
