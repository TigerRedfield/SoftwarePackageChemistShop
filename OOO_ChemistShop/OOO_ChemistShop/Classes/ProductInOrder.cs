using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOO_ChemistShop.Classes
{
    public class ProductInOrder
    {
        public Classes.ProductExtended ProductExtendedInOrder { get; set; }

        public int countProductInOrder { get; set; }

        /// <summary>
        /// Расширенный класс 
        /// </summary>
        /// <param name="productExtended"></param>
        public ProductInOrder(ProductExtended productExtended) 
        {
            ProductExtendedInOrder = productExtended;
            this.countProductInOrder = 1;
        }
    }
}
