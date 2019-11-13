using Microsoft.AspNetCore.Mvc;
using Middleware_Tool;
using System.Collections.Generic;

namespace Middleware_CoreWeb
{
    public class ApiBaseController : ControllerBase
    {
        public Responsemessage succeed()
        {
            return new Responsemessage() { message = "成功", state = 200 };
        }

        public Responsemessage succeed(string message)
        {
            return new Responsemessage() { message = message, state = 200 };
        }

        public Responsemessage succeed(string message, List<object> list)
        {
            return new Responsemessage() { message = message, state = 200, Data = list };
        }

        public Responsemessage succeed<T>(List<object> list)
        {
            return new Responsemessage() { message = "成功", state = 200, Data = list };
        }

        public Responsemessage Error()
        {
            return new Responsemessage() { message = "操作失败", state = 500 };
        }

        public Responsemessage Error(string message)
        {
            return new Responsemessage() { message = message, state = 500 };
        }
    }
}