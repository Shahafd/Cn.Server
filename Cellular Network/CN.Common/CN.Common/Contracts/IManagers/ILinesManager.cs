using CN.Common.Models;
using CN.Common.Models.TempModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CN.Common.Contracts.IManagers
{
    public interface ILinesManager
    {
        string GetNewLine(Client client);
        List<Package> GetDefaultPackages();
        PackageDetails GetPackageDetailsByPackageId(int id);
        SelectedNumbers GetSelectedNumbersById(int selectedNumbersId);
        List<Line> GetClientLinesByClientId(string id);
        Package GetPackageByLineId(string lineId);
        object SendLinePackageObj(LinePackObject linePackObj);
    }
}
