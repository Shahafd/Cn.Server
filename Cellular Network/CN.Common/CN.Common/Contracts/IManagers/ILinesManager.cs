using CN.Common.Enums;
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
        RequestStatusEnum SendLinePackageObj(LinePackObject linePackObj);
        RequestStatusEnum DeleteLine(string line);
        LineStatusEnum GetLineStatus(string line);
        RequestStatusEnum UpdateLineStatus(Line line);
        LineDetails GetLineDetails(string lineNumber);
        ClientBill GetBillForSpecificLines(string clientId, YearAndMonth Date, List<string> lines);
        double GetClientValue(Client client);
        bool LineExistedAtDate(string lineNumber, YearAndMonth date);
        double GetSumOfLast3Months(Client client);
    }
}
