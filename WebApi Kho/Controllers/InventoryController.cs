using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi_Kho.Databases;
using WebApi_Kho.Models;

namespace WebApi_Kho.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InventoryController : ControllerBase
    {
        private readonly DB context;

        public InventoryController(DB context)
        {
            this.context = context;
        }

    }


}
