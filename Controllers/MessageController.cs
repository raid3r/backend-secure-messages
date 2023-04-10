using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SecureMessages.Models;
using SecureMessages.Models.Db;
using SecureMessages.Models.Requests;
using SecureMessages.Models.Responses;
using RequestMessageDataModel = SecureMessages.Models.Requests.MessageDataModel;
using ResponseMessageDataModel = SecureMessages.Models.Responses.MessageDataModel;

namespace SecureMessages.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MessageController : ControllerBase
    {
        private readonly ILogger<MessageController> _logger;
        private readonly MessagesDbContext _context;

        public MessageController(
            ILogger<MessageController> logger,
            MessagesDbContext dbContext
            )
        {
            _logger = logger;
            _context = dbContext;
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Save([FromBody] RequestMessageDataModel addMessage)
        {
            string uniqueCode;
            do
            {
                uniqueCode = UniqueCodeHelper.GenerateRandomString(15);
            } while ((await _context.Messages.Where(x => x.Code == uniqueCode).CountAsync()) > 0);

            var message = new Message { Code = uniqueCode, EncryptedContent = addMessage.EncryptedText };
            _context.Messages.Add(message);

            await _context.SaveChangesAsync();
            return Ok(new MessageSaved { Code = uniqueCode, Url = $"/Message/Read/{uniqueCode}" });
        }

        [HttpGet]
        [Route("[action]/{code}")]
        public async Task<IActionResult> Read(string code)
        {
            var message = await _context.Messages.FirstOrDefaultAsync(x=> x.Code == code);

            //if (message != null)
            //{
            //    _context.Messages.Remove(message);
            //}    

            return Ok(new ResponseMessageDataModel
            {
                Found = message != null,
                EncryptedText = message != null ? message.EncryptedContent: ""
            });
        }
    }
}