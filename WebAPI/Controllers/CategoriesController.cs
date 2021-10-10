using AutoMapper;
using Business.Abstract;
using Entities.DataTransferObjects;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using WebAPI.LoggerService;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/category")]
    public class CategoriesController : ControllerBase
    {
        private IRepositoryWrapper _wrapper;
        private IMapper _mapper;
        private ILoggerManager _logger;
        public CategoriesController(IRepositoryWrapper wrapper, IMapper mapper, ILoggerManager logger)
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
                var categories = _wrapper.Category.GetAll();
                var categoryResult = _mapper.Map<List<CategoryDto>>(categories);
                return Ok(categoryResult);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            try
            {
                var category = _wrapper.Category.GetById(id);
                if (category == null)
                {
                    _logger.Error($"Category {id} not found");
                    return NotFound($"Category {id} not found");
                }

                var categoryResult = _mapper.Map<CategoryDto>(category);
                return Ok(categoryResult);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpPost]
        public IActionResult Create([FromBody] CategoryUpdateDto category)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("Model object is not valid");
                }

                var categoryEntity = _mapper.Map<Category>(category);

                _wrapper.Category.Create(categoryEntity);
                _wrapper.Save();
                return Ok(categoryEntity);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut]
        public IActionResult Update([FromQuery]int id, [FromBody] CategoryUpdateDto category)
        {
            try
            {
                if(category == null)
                {
                    return BadRequest("Category object is null");
                }

                if(!ModelState.IsValid)
                {
                    return BadRequest("Model object is not valid");
                }

                var categoryEntity = _wrapper.Category.GetById(id);
                _mapper.Map(category, categoryEntity);

                if(categoryEntity == null)
                {
                    _logger.Error($"Category {id} not found for updating");
                    return NotFound("Category is not exist");
                }

                _wrapper.Category.Update(categoryEntity);
                _wrapper.Save();

                return Ok(categoryEntity);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete]
        public IActionResult Delete([FromQuery] int id)
        {
            try
            {
                var category = _wrapper.Category.GetById(id);

                if (category == null)
                {
                    _logger.Error($"Category {id} not found for deleting");
                    return NotFound($"Category {id} not found for deleting");
                }

                _wrapper.Category.Delete(category);
                _wrapper.Save();
                return Ok(category);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
                return StatusCode(500, "Internal Server Error");
            }
        }
    }
}
