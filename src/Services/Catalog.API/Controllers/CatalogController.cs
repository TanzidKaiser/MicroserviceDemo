﻿using Catalog.API.Interfaces.Manager;
using Catalog.API.Models;
using CoreApiResponse;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using System.Net;

namespace Catalog.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CatalogController : BaseController
    {
        IProductManager _productManager;
        public CatalogController(IProductManager productManager)
        {
            _productManager= productManager; 
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Product>), (int)HttpStatusCode.OK)]
        [ResponseCache(Duration = 10)]
        public IActionResult GetProducts()
        {
            try
            {
                var products = _productManager.GetAll();
                return CustomResult("Data load successfully", products, HttpStatusCode.OK);

            } catch (Exception ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.BadRequest);
            }
        }

        [HttpGet]
        [ProducesResponseType(typeof(Product), (int)HttpStatusCode.OK)]
        public IActionResult GetById(string id)
        {
            try
            {
                var product = _productManager.GetById(id);
                return CustomResult("Data load successfully", product, HttpStatusCode.OK);

            }
            catch (Exception ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.BadRequest);
            }
        }

        [HttpGet]
        [ProducesResponseType(typeof(Product), (int)HttpStatusCode.OK)]
        public IActionResult GetByCatagory(string catagory)
        {
            try
            {
                var product = _productManager.GetByCatagory(catagory);
                return CustomResult("Data load successfully", product, HttpStatusCode.OK);

            }
            catch (Exception ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.BadRequest);
            }
        }

        [HttpPost]
        [ProducesResponseType(typeof(Product), (int) HttpStatusCode.Created)]
        public IActionResult CreateProduct([FromBody] Product product)
        {
            try
            {
                product.Id = ObjectId.GenerateNewId().ToString();
                bool isSaved = _productManager.Add(product);

                if(isSaved)
                {
                    return CustomResult("Product has been save successfully", product, HttpStatusCode.Created);
                }

                return CustomResult("Product save failed", product, HttpStatusCode.BadRequest);

            } catch (Exception ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.BadRequest);
            }
        }

        [HttpPut]
        [ProducesResponseType(typeof(Product), (int)HttpStatusCode.OK)]
        public IActionResult UpdateProduct([FromBody] Product product)
        {
            try
            {
                if(string.IsNullOrEmpty(product.Id))
                {
                    return CustomResult("Data not found", HttpStatusCode.NotFound);
                }

                bool isUpdated = _productManager.Update(product.Id, product);
                if(isUpdated)
                {
                    return CustomResult("Product has been modified succesfully", product, HttpStatusCode.OK);
                }

                return CustomResult("Product modified failed", product, HttpStatusCode.BadRequest);

            } catch (Exception ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.BadRequest);
            }
        }

        [HttpDelete]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public IActionResult DeleteProduct(string id)
        {
            try
            {
                if(string.IsNullOrEmpty(id))
                {
                    return CustomResult("Data not found", HttpStatusCode.NotFound);
                }

                bool isDeleted = _productManager.Delete(id);
                if(isDeleted)
                {
                    return CustomResult("Product has been delete successfully", HttpStatusCode.OK);
                }

                return CustomResult("product deleted failed", HttpStatusCode.BadRequest);

            } catch(Exception ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.BadRequest);
            }
        }
    }
}
