using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOO_ChemistShop.Classes
{
    public static class Helper
    {
        //доступное свойство связи с БД
        public static Model.OOODBChemistShop DB { get; set; }

        //Доступное свойство пользователя системы
        public static Model.Users User { get; set; }
    }
}
