using CN.Common.Models.TempModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CN.Common.Contracts.IServices
{
    public interface IDocGenerator
    {
        object Generate(ClientBill clientBill);

    }
}

