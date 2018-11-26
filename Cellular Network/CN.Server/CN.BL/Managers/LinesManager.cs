using CN.Common.Contracts.IManagers;
using CN.Common.Contracts.IRepositories;
using CN.Common.Models;
using CN.Common.Models.TempModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CN.BL.Managers
{
    public class LinesManager:ILinesManager
    {
        INetworkRepository networkRepository { get; set; }
        public LinesManager(INetworkRepository networkRepository)
        {
            this.networkRepository = networkRepository;
        }
        public string GetNewLine(Client client)
        {
            //gets a new line,if first line: returns the clients contacts number,else generates a new 1
            List<Line> clientLines = networkRepository.GetClientLines(client.ID);
            if (clientLines.Count == 0)
            {
                return client.ContactNumber;
            }
            else return networkRepository.GetNewNumber();
        }

        public List<Package> GetDefaultPackages()
        {
            //returns the default packages
            return networkRepository.GetDefaultPackages();
        }

        public PackageDetails GetPackageDetailsByPackageId(int id)
        {
            //returns the package details for this package id
            return networkRepository.GetPackageDetailsByPackageId(id);
        }

        public SelectedNumbers GetSelectedNumbersById(int selectedNumbersId)
        {
            //returns the selected lines that matches this id
            return networkRepository.GetSelectedNumbersById(selectedNumbersId);
        }
        public List<Line> GetClientLinesByClientId(string id)
        {
            //returns a list of string represnting the client lines
            return networkRepository.GetClientLines(id);
        }

        public Package GetPackageByLineId(string lineId)
        {
            //returns the package that matches this line id
            return networkRepository.GetPackageByLineId(lineId);
        }

        public object SendLinePackageObj(LinePackObject linePackObj)
        {
            //check if the line needs to be created or updated and acts accordingly
        }
    }
}
