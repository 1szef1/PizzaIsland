using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaIsland.Data
{
    public enum ProductEnum
    {
        [Description("Pizzy")]
        Pizza = 1,
        [Description("Dodatki do pizzy")]
        PizzaAddOn = 2,
        [Description("Dania główne")]
        MainDish = 3,
        [Description("Dodatki do dań głównych")]
        MainDishAddOn = 4,
        [Description("Zupy")]
        Soup = 5,
        [Description("Napoje")]
        Drink = 6,
    }
}
