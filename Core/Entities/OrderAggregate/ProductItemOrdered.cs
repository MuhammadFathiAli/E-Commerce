using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities.OrderAggregate
{
    //this class acts as snapshot of the order product items at the time they are selected into the order 
    //because the product props (name, image, ....) may be changed after it's picked into order
    //so, i don't want the order items to be changed in the database after they are orderd
    //just save it in the database as it was ordered
    //so it will owned by order class, just a field in the order row in the db
    //no id
    public class ProductItemOrdered
    {
        public ProductItemOrdered() //ef migration
        {

        }
        public ProductItemOrdered(int productItemId, string productName, string picturelUrl)
        {
            ProductItemId = productItemId;
            Title = productName;
            Image = picturelUrl;
        }

        public int ProductItemId { get; set; }
        public string Title { get; set; }
        public string Image { get; set; }
    }
}
