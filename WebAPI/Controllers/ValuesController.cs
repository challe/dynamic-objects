using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ObjectLibrary;
using ObjectLibrary.Services;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly IDynamicObjectService _dynamicObjectService;

        public ValuesController(IDynamicObjectService dynamicObjectService)
        {
            _dynamicObjectService = dynamicObjectService;
        }

        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            Type customType = CustomTypeBuilder.GetType("Company");
            int id = 2;

            var method = _dynamicObjectService.GetType().GetMethod("FindById");
            var genericMethod = method.MakeGenericMethod(customType);
            var result = genericMethod.Invoke(_dynamicObjectService, new object[] { id });

            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
