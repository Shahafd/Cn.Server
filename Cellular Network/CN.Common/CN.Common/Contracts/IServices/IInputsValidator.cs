using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CN.Common.Contracts.IServices
{
    public interface IInputsValidator
    {
        string ValidateStrInput(string fieldName, string input, int minLength, int maxLength);
        string ValidateIntInput(string fieldName, string input, int minValue, int maxValue);
        string ValidateIntInput(string fieldName, int input, int minValue, int maxValue);
        string ValidateDoubleInput(string fieldName, string input, int minValue, int maxValue);
        string ValidateDoubleInput(string fieldName, double input, int minValue, int maxValue);
        string ValidateDateInput(string fieldName, DateTime input);
        string ValidateIDInput(string fieldName, string input);
        string ValidatePhoneInput(string input);
        string ValidateYearOfBirthInput(string fieldName, string input);
    }
}
