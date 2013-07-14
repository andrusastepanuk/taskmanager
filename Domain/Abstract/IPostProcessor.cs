using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Domain.Entities;

namespace Domain.Abstract
{
    public interface IPostProcessor
    {
        void ProcessPost(ShippingDetails shippingDetails);
    }
}
