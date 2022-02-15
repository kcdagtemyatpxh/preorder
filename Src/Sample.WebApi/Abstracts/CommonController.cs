﻿using Microsoft.AspNetCore.Mvc;
using Sample.Common;
using TripleSix.Core.Dto;
using TripleSix.Core.WebApi.Controllers;

namespace Sample.WebApi.Abstracts
{
    [Route("[controller]")]
    [ApiExplorerSettings(GroupName = "common")]
    public abstract class CommonController : BaseController
    {
        protected override IIdentity GenerateIdentity()
        {
            return new Identity(HttpContext);
        }
    }
}
