using AutoMapper;
using Business.Abstract;
using Microsoft.AspNetCore.Mvc;
using Entities.DataTransferObjects;
using System;
using System.Collections.Generic;
using Entities.Models;
using DataAccess.Abstract;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/product")]
    public class ProductsController: ControllerBase
    {
        private IRepositoryWrapper _wrapper;
        private IMapper _mapper;
        private ILoggerManagerRepository _logger;
        public ProductsController(IRepositoryWrapper wrapper, IMapper mapper, ILoggerManagerRepository logger)
        {
            _wrapper = wrapper;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                var products = _wrapper.Product.GetAll();
                var productsResult = _mapper.Map<List<ProductDto>>(products);
                //_logger.Save(200, "Fetched all products");
                return Ok(productsResult);
            }
            catch (Exception ex)
            {
                //_logger.Save(500, ex.Message);
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            try
            {
                var product = _wrapper.Product.GetById(id);
                if (product == null)
                {
                    //_logger.Save(400, $"Product not found by id = {id}");
                    return NotFound($"Product not found by id = {id}");
                }

                //_logger.Save(200, $"Product found by id = {id}");
                var productResult = _mapper.Map<ProductDto>(product);
                return Ok(productResult);
            }
            catch (Exception ex)
            {
                //_logger.Save(500, $"Product fetch error: {ex.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpPost]
        public IActionResult Create([FromBody] ProductUpdateDto product)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest("Model object is not valid");
                }

                var productEntity = _mapper.Map<Product>(product);

                _wrapper.Product.Create(productEntity);
                _wrapper.Save();
                return Ok(productEntity);
            }
            catch (Exception ex)
            {
                //_logger.Save(500, ex.Message);
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpPut]
        public IActionResult Update([FromQuery] int id,[FromBody] ProductUpdateDto product)
        {
            try
            {
                if(product == null)
                {
                    return BadRequest("Product object is null");
                }

                if(!ModelState.IsValid)
                {
                    return BadRequest("Model object is not valid");
                }

                var productEntity = _wrapper.Product.GetById(id);
                _mapper.Map(product, productEntity);

                if(productEntity == null)
                {
                    //_logger.Save(404, $"Product {id} not found for updating");
                    return NotFound("Product is not exist");
                }

                _wrapper.Product.Update(productEntity);
                _wrapper.Save();

                return Ok(productEntity);
            }
            catch (Exception ex)
            {
                //_logger.Save(500, ex.Message);
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpDelete]
        public IActionResult Delete([FromQuery] int id)
        {
            try
            {
                var product = _wrapper.Product.GetById(id);

                if(product == null)
                {
                    //_logger.Save(404, $"Product {id} not found for deleting");
                    return NotFound("Product is not exist");
                }

                _wrapper.Product.Delete(product);
                _wrapper.Save();
                return Ok(product);
            }
            catch (Exception ex)
            {
                //_logger.Save(500, ex.Message);
                return StatusCode(500, "Internal Server Error");
            }
        }
    }
}
