using CN.Common.Configs;
using CN.Common.Contracts.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CN.Common.Services
{
    public class InputsValidator : IInputsValidator
    {
        public string ValidateStrInput(string fieldName, string input, int minLength, int maxLength)
        {
            //validates an input, returns an empty string if the input has been approved
            string returnStr = "";
            if (string.IsNullOrEmpty(input))
            {
                returnStr = $"Please insert a {fieldName}";
            }
            else if (input.Length < minLength || input.Length > maxLength)
            {
                returnStr = $"{fieldName} length must be between {minLength} and {maxLength} chars";
            }
            return returnStr;
        }
        public string ValidateIntInput(string fieldName, string input, int minValue, int maxValue)
        {
            //validates an input, returns an empty string if the input has been approved
            string returnStr = "";
            int outNum;
            if (!int.TryParse(input, out outNum))
            {
                returnStr = $"{fieldName} must be between {minValue} and {maxValue} digits only";
            }

            else if (outNum < minValue || outNum > maxValue)
            {
                returnStr = $"{fieldName} must be between {minValue} and {maxValue} digits only";
            }
            return returnStr;
        }
        public string ValidateIntInput(string fieldName, int input, int minValue, int maxValue)
        {
            //validates an input, returns an empty string if the input has been approved
            string returnStr = "";

            if (input < minValue || input > maxValue)
            {
                returnStr = $"{fieldName} must be between {minValue} and {maxValue} digits only";
            }
            return returnStr;
        }
        public string ValidateIDInput(string fieldName, string input)
        {
            //validates an input, returns an empty string if the input has been approved
            string returnStr = "";
            int outNum;
            if (!int.TryParse(input, out outNum))
            {
                returnStr = $"{fieldName} length must be between {InputsConfigs.MinIdLength} and {InputsConfigs.MaxIdLength} digits only";
            }

            else if (input.Length < InputsConfigs.MinIdLength || input.Length > InputsConfigs.MaxIdLength)
            {
                returnStr = $"{fieldName} length must be between {InputsConfigs.MinIdLength} and {InputsConfigs.MaxIdLength} digits only";
            }
            return returnStr;
        }
        public string ValidateDoubleInput(string fieldName, string input, int minValue, int maxValue)
        {
            //validates an input, returns an empty string if the input has been approved
            string returnStr = "";
            double outNum;
            if (!double.TryParse(input, out outNum))
            {
                returnStr = $"{fieldName} must be between {minValue} and {maxValue} digits only";
            }
            else if (outNum == 0)
            {
                returnStr = $"Please insert a {fieldName}";
            }
            else if (outNum < minValue || outNum > maxValue)
            {
                returnStr = $"{fieldName} must be between {minValue} and {maxValue} digits only";
            }
            return returnStr;
        }
        public string ValidateDoubleInput(string fieldName, double input, int minValue, int maxValue)
        {
            //validates an input, returns an empty string if the input has been approved
            string returnStr = "";
          
             if (input < minValue || input > maxValue)
            {
                returnStr = $"{fieldName} must be between {minValue} and {maxValue} digits only";
            }
            return returnStr;
        }
        public string ValidateDateInput(string fieldName, DateTime input)
        {
            //validates an input, returns an empty string if the input has been approved
            string returnStr = "";
            if (input >= DateTime.Now)
            {
                returnStr = $"Please insert a valid {fieldName}";
            }

            return returnStr;
        }
        public string ValidatePhoneInput(string input)
        {
            //validates phone input, returns an empty string if the input has been approved
            //DOESNOT CHECK IF THE PHONE EXISTS
            string returnStr = "";
            int numCheck;
            if (string.IsNullOrEmpty(ValidateStrInput("Phone number", input, InputsConfigs.PhoneLength, InputsConfigs.PhoneLength)) && int.TryParse(input, out numCheck))
            {
            }
            else
            {
                returnStr = $"Phone number must contain digits only and must be {InputsConfigs.PhoneLength} digits long";
            }
            return returnStr;
        }

    }
}
