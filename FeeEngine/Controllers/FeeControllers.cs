using FeeEngine.Models;
using FeeEngine.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FeeEngine.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FeeController : ControllerBase
    {
        private readonly IEnumerable<IRule> _rules;
        private readonly IFeeCalculatorService _feeCalculatorService;
        private readonly ITransactionHistoryService _transactionHistoryService;
        private readonly IBatchFeeCalculatorService _batchFeeCalculatorService;

        public FeeController(IEnumerable<IRule> rules, IFeeCalculatorService feeCalculatorService,ITransactionHistoryService transactionHistoryService,
            IBatchFeeCalculatorService batchFeeCalculatorService)
        {
            _rules = rules;
            _feeCalculatorService = feeCalculatorService;
            _transactionHistoryService = transactionHistoryService; 
            _batchFeeCalculatorService = batchFeeCalculatorService;
        }
        
        [HttpPost("calculate")]
        public IActionResult CalculateFee([FromBody] Transaction transaction)
        {
          var result = _feeCalculatorService.CalculateFee(transaction);

            return Ok(result);
        }

        [HttpGet("history")]
        public IActionResult GetTransactionHistory()
        {
            var history = _transactionHistoryService.GetAll();
            return Ok(history);
        }

        [HttpPost("calculate/batch")]
        public IActionResult CalculateBatch([FromBody] List<Transaction> transactions)
        {
            var results = _batchFeeCalculatorService.CalculateBatch(transactions);
            return Ok(results);
        }
    }

}
