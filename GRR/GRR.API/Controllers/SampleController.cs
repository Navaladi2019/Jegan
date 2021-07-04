
using GRR.Data.Models;
using GRR.Data.UnitOfWork;
using GRR.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GRR.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SampleController : ControllerBase
    {
        private readonly ISampleService _sampleService;
        public SampleController(ISampleService sampleService,IUow iuow)
        {
            _sampleService = sampleService;
        }

        [HttpPost]
        public DbUser save(DbUser dbUser)
        {
            var res = _sampleService.saveuser(dbUser);
            return res;
        }

        [HttpPost]
        [Route("Update")]
        public DbUser update(DbUser dbUser)
        {
            var res = _sampleService.Updateuser(dbUser);
            return res;
        }

        [HttpDelete]
        [Route("delete")]
        public void delete(int id)
        {
             _sampleService.deleteUser(id);
        }

        [HttpGet]
        [Route("get")]
        public object get(int id)
        {
           return _sampleService.GetUser(id);
        }
    }
}
