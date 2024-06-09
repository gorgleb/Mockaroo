using Microsoft.AspNetCore.Mvc;
using Mockaroo.Services;
using MockarooLibrary.Exceptions;
using MockarooLibrary.Model;
using MockarooLibrary.Model.Enums;
using MockarooLibrary.Processors.Interfaces;
using MockarooLibrary.Repository.Interfaces;
using MockarooWebApi.Helpers;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MockarooWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MockController : ControllerBase
    {
        private readonly IMockProcessor _mockProcessor;

        public MockController(IMockProcessor mockProcessor)
        {
            _mockProcessor = mockProcessor;
        }

        [HttpGet("generate")]
        public async Task<IActionResult> GenerateData(string tableName, int rowCount, List<(string, string)> parameters, OutputDataTypes outputType)
        {
            var table = new Table();
            var generateParameters = new Dictionary<string, string>();

            try
            {
                table = TableConverter.ConvertToTable(tableName, rowCount, parameters);
            }
            catch (TypeConversionException ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"An error occurred: {ex.Message}");
            }

            var downloadData = await _mockProcessor.ProcessDataGeneration(table, generateParameters, outputType);

            return Ok(downloadData.Content);
        }
    }
}