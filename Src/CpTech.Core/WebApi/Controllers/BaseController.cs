﻿using System.Collections.Generic;
using AutoMapper;
using CpTech.Core.Dto;
using CpTech.Core.WebApi.Filters;
using CpTech.Core.WebApi.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace CpTech.Core.WebApi.Controllers
{
    [Route("[controller]")]
    [Consumes("application/json")]
    [Produces("application/json")]
    [ValidateModel]
    public class BaseController : ControllerBase
    {
        public IConfiguration Configuration { get; set; }

        public IMapper Mapper { get; set; }

        protected virtual IIdentity GenerateIdentity()
        {
            return new BaseIdentity(HttpContext);
        }

        protected TIdentity GenerateIdentity<TIdentity>()
            where TIdentity : IIdentity
        {
            return (TIdentity)GenerateIdentity();
        }

        protected SuccessResult SuccessResult()
        {
            return new SuccessResult();
        }

        protected DataResult<TResult> DataResult<TResult>(TResult data)
        {
            return new DataResult<TResult>(data);
        }

        protected PagingResult<TItem> PagingResult<TItem>(IList<TItem> data, long total, int page, int? size)
        {
            return new PagingResult<TItem>(data, total, page, size);
        }

        protected PagingResult<TItem> PagingResult<TItem>(IPaging<TItem> data)
        {
            return new PagingResult<TItem>(data);
        }
    }
}