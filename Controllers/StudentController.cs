using MahaNoticePortalAPI.Business;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Data;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public StudentController(IConfiguration configuration)
        {
            _configuration = configuration;
            StudentBusiness.Initialize(_configuration);
        }


        [HttpPost]
        [AllowAnonymous]
        [Route("saveStudent")]
        public async Task<IActionResult> saveStudent(Student student )
        {
            DataTable dt = new DataTable();
            try
            {
                dt = await StudentBusiness.SaveStudent(student);

                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        string JSONString = string.Empty;
                        JSONString = JsonConvert.SerializeObject(dt);
                        return Ok(JSONString);
                    }
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
            return StatusCode(StatusCodes.Status500InternalServerError, "An unexpected error occurred.");
        }
        [HttpGet]
        [AllowAnonymous]
        [Route("getStudent")]
        public async Task<IActionResult> getStudents( )
        {
            DataTable dt = new DataTable();
            try
            {
                dt = await StudentBusiness.GetStudent();

                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        string JSONString = string.Empty;
                        JSONString = JsonConvert.SerializeObject(dt);
                        return Ok(JSONString);
                    }
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
            return StatusCode(StatusCodes.Status500InternalServerError, "An unexpected error occurred.");
        }

    


}



