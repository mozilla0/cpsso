using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace PrivateLabelLite.Entities.Api
{
    [Serializable]
    public class ApiErrorException : Exception
    {
        public ApiErrorException(SerializationInfo info, StreamingContext context)
        {
            if (info != null)
            {
                try
                {
                    this.ErrorMessage = info.GetValue("ErrorMessage", typeof(object));
                    this.Result = info.GetString("Result");
                    this.ErrorCode = info.GetString("ErrorCode");
                    this.Key = info.GetInt32("Key");
                }
                catch (Exception)
                {
                    
                }
               

            }
        }
        public override void GetObjectData(SerializationInfo info,StreamingContext context)
        {
            base.GetObjectData(info, context);

            if (info != null)
                info.AddValue("ErrorMessage", this.ErrorMessage);
        }

        public string Result { get; set; }
        public string ErrorCode { get; set; }
        public object ErrorMessage { get; set; }
        public int Key { get; set; }
        public bool IsSuccess
        {
            get
            {
                return (Result ?? "").ToLower() == "success" ? true : false;
            }

        }
        public bool IsValid { get { return IsSuccess; } }
        public string Message
        {
            get
            {
                return ErrorMessage.ToString();
            }

        }
    }
}
