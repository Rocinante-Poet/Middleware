﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Middleware_DatabaseAccess;
using Middleware_Tool;
using System.Collections.Generic;

namespace Middleware_CoreWeb.Controllers.api
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class GroupController : ApiBaseController
    {
        private DB_Group Get_GroupDb = new DB_Group();

        [HttpGet]
        public JsonData<group_model> Get([FromQuery]int limit, int offset, string Name) => Get_GroupDb.GetList(limit, offset, Name);

        [HttpGet("select")]
        public IEnumerable<group_model> Getparentmenus() => Get_GroupDb.Get();

        [HttpPut("add")]
        public Basemessage Put([FromBody]group_model item) => Get_GroupDb.Add(item) ? succeed() : Error();

        [HttpDelete]
        public Basemessage delete([FromBody]List<group_model> itemArray) => Get_GroupDb.Delete(itemArray) ? succeed() : Error();

        [HttpPut("edit")]
        public Basemessage Putedit([FromBody]group_model item) => Get_GroupDb.Update(item) ? succeed() : Error();
    }
}