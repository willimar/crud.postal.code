using crud.api.core.enums;
using crud.api.core.interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace postal.code.api
{
    public class HandleMessage : IHandleMessage
    {
        public string MessageType { get; private set; }

        public string Message { get; private set; }

        public HandlesCode Code { get; private set; }

        public List<string> StackTrace { get; private set; }

        public static IHandleMessage Factory(string messageType, string message, HandlesCode code, string stackTrace)
        {
            return new HandleMessage()
            {
                Code = code,
                Message = message,
                MessageType = messageType,
                StackTrace = stackTrace?.Split("\r\n").ToList()
            };
        }
    }
}
