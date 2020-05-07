using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using AutoMapper;
using crud.api.core.enums;
using crud.api.core.interfaces;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using postal.code.api.Mapper;
using postal.code.api.Models;
using postal.code.core.entities;
using postal.code.core.services;

namespace postal.code.api.Controllers
{
    [EnableCors(policyName: Program.AllowSpecificOrigins)]
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class PostalCodeController : Controller
    {
        private readonly AddressService _service;
        private readonly IMapper _mapper;

        public PostalCodeController(AddressService service, MapperProfile mapperProfile)
        {
            this._service = service;
            this._mapper = mapperProfile.CreateMapper(new AddressMapper());
        }

        [HttpPost]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Post))]
        public ActionResult<IEnumerable<IHandleMessage>> Post(AddressModel address)
        {
            try
            {
                var entity = this._mapper.Map<Address>(address);
                var result = this._service.AppenData(entity);

                if (result.Any(r => r.Code.Equals(HandlesCode.Accepted) || r.Code.Equals(HandlesCode.Ok)))
                {
                    return StatusCode((int)HttpStatusCode.OK, result);
                }
                else
                {
                    return StatusCode((int)HttpStatusCode.BadRequest, result);
                }
            }
            catch (Exception e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, HandleMessage.Factory(e.GetType().Name, e.Message, HandlesCode.InternalException, e.StackTrace));
            }            
        }
    }
}