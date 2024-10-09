using Store.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Repository.Specification.Productspecs
{
    public class ProductWithCountSpecification : BaseSpecification<Products>
    {
        public ProductWithCountSpecification(ProductSpecification specs)
           : base(product => (!specs.BrandId.HasValue || specs.BrandId == specs.BrandId.Value) &&
                             (!specs.TypeId.HasValue || specs.TypeId == specs.TypeId.Value) &&
                              (string.IsNullOrEmpty(specs.Search) || product.Name.Trim().ToLower().Contains(specs.Search))

           )
        {

        }
    }
}
