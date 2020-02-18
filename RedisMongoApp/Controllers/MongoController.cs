using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HangFireConsole;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repository;
using Model;
using Newtonsoft.Json;

namespace RedisMongoApp.Controllers
{
    [Route("api/")]
    [ApiController]
    public class MongoController : ControllerBase
    {
        private readonly IRepositoryManagement _repositoryManagement;
        
        private RedisConnectorHelper redisConnector;
        public MongoController(IRepositoryManagement repositoryManagement)
        {

            _repositoryManagement = repositoryManagement;
            redisConnector = new RedisConnectorHelper();
        }

        StackExchange.Redis.IDatabase cache = RedisConnectorHelper.Connection.GetDatabase();

        [Route("get/{id}")]
        public IActionResult Details(string id)
        {
            Product product;

            var obj = cache.StringGet(id);
            if (obj.HasValue == true)
            {
                product = JsonConvert.DeserializeObject<Product>(obj);
            }
            else
            {
                product = _repositoryManagement.Get<Product>(id);
                product.counter = product.counter + 1;
                _repositoryManagement.UpdateInfo<Product>(id, product);
            }

            return Ok(product);
        }
        [Route("create")]
        [HttpPost]
        public RedirectToActionResult Create(Product record)
        {
            record.Source = "MongoDB";
            record.counter = -1;
            Product newProduct = _repositoryManagement.Create<Product>(record);
            return RedirectToAction("details", new { id = newProduct.PrimaryId });
        }
    }
}