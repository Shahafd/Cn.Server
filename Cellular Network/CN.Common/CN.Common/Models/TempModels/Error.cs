using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CN.Common.Models.TempModels
{
    public class Error
    {
        public int ErrorID { get; set; }
        public int UserID { get; set; }
        public string Message { get; set; }
        public string ErrorLocation { get; set; }
        public string TargetObject { get; set; }
        public string StackTrace { get; set; }
        public DateTime Date { get; set; }
        public Error()
        {

        }
        public Error(Exception exception, int UserID)
        {
            Message = exception.Message;
            StackTrace = exception.StackTrace;
            TargetObject = exception.TargetSite.Name;
            this.UserID = UserID;
            ErrorLocation = exception.TargetSite.DeclaringType.FullName;
            Date = DateTime.Now;
        }
        public Error(string Message, string StackTrace, string TargetObject, int AccountID)
        {
            this.Message = Message;
            Date = DateTime.Now;
            this.StackTrace = StackTrace;
            this.TargetObject = TargetObject;
            this.UserID = AccountID;
        }


    }
}
