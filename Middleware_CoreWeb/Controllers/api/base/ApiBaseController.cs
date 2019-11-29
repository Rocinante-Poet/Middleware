using Microsoft.AspNetCore.Mvc;
using Middleware_CoreWeb;
using Middleware_Tool;
using System.Collections.Generic;

namespace Middleware_CoreWeb
{
    public class ApiBaseController : ControllerBase
    {
        public Basemessage succeed()
        {
            return new Basemessage() { message = "成功", state = 200 };
        }

        public Basemessage succeed(string message)
        {
            return new Basemessage() { message = message, state = 200 };
        }

        public Basemessage succeed<T>(string message, IEnumerable<T> list)
        {
            return new Responsemessage<T>() { message = message, state = 200, Data = list };
        }

        public Basemessage succeed<T>(IEnumerable<T> list)
        {
            return new Responsemessage<T>() { message = "成功", state = 200, Data = list };
        }

        public Basemessage succeed<T>(T data)
        {
            return new Jsonmessage<T>() { message = "成功", state = 200, Data = data };
        }

        public Basemessage Error()
        {
            return new Basemessage() { message = "操作失败", state = 500 };
        }

        public Basemessage Error(string message)
        {
            return new Basemessage() { message = message, state = 500 };
        }
    }
}