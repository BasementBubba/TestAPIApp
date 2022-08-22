using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DialogsController : ControllerBase
    {
        List<RGDialogsClients> dialogsClients = new RGDialogsClients().Init();

        [HttpPost("dialogs")]
        public IEnumerable<Guid> GetDialogs([FromBody] IEnumerable<Guid> clientIDs)
        {
            List<Guid> result = new List<Guid>();
            var dialogs = dialogsClients.GroupBy(d => d.IDRGDialog).ToList();
            foreach(var dialog in dialogs)
            {
                var ids = dialog.Select(d => d.IDClient).ToList();
                if(ids.Intersect(clientIDs).Count() == clientIDs.Count())
                {
                    result.Add(dialog.Key);
                }
            }
            if(result.Count == 0)
            {
                result.Add(Guid.Empty);
            }
            return result;
        }
    }
}
