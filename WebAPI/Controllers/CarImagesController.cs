using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Business.Abstract;
using Entities.Concrete;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarImagesController : ControllerBase
    {
        ICarImageService _carImageService;

        public CarImagesController(ICarImageService carImageService)
        {
            _carImageService = carImageService;
        }

        [HttpGet("getall")]
        public IActionResult GetAll()
        {
            var result = _carImageService.GetAll();
            if (result.Success) { return Ok(result); }
            return BadRequest(result);
        }

        [HttpGet("getbycarid")]
        public IActionResult GetByCarId(int carId)
        {
            var result = _carImageService.GetByCarId(carId);
            if (result.Success) { return Ok(result); }
            return BadRequest(result);
        }

        [HttpPost("add")]
        public IActionResult Add([FromForm(Name ="Image")] IFormFile file, [FromForm] CarImage carImage)
        {
            var result = _carImageService.Add(file, carImage);
            if (result.Success) { return Ok(result); }
            return BadRequest(result);
        }

        [HttpPost("update")]
        public IActionResult Update([FromForm(Name = "Image")] IFormFile file, [FromForm(Name ="Id")] int id)
        {
            var find = _carImageService.GetById(id);
            if (find.Success)
            {
                var result = _carImageService.Update(file, find.Data);
                if (result.Success) { return Ok(result); }
            }
            return BadRequest(find);
        }

        [HttpPost("delete")]
        public IActionResult Delete([FromForm(Name = "Id")] int id)
        {
            var find = _carImageService.GetById(id);
            if (find.Success)
            {
                var result = _carImageService.Delete(find.Data);
                if (result.Success)
                {
                    return Ok(result);
                }
            }
            return BadRequest(find);
        }
    }

}
