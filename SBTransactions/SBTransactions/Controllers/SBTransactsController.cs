using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using SBTransactions.Models;

namespace SBTransactions.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SBTransactsController : ControllerBase
    {
        private readonly SBContext _context;

        public SBTransactsController(SBContext context)
        {
            _context = context;
        }

        // GET: api/SBTransacts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SBTransact>>> GetSbtransaction()
        {
            return await _context.Sbtransaction.ToListAsync();
        }

        // GET: api/SBTransacts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SBTransact>> GetSBTransact(int id)
        {
            var sBTransact = await _context.Sbtransaction.FindAsync(id);

            if (sBTransact == null)
            {
                return NotFound();
            }

            return sBTransact;
        }

        // PUT: api/SBTransacts/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSBTransact(int id, SBTransact sBTransact)
        {
            if (id != sBTransact.TransactionId)
            {
                return BadRequest();
            }

            _context.Entry(sBTransact).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SBTransactExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/SBTransacts
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<SBTransact>> PostSBTransact(SBTransact sBTransact)
        {
            _context.Sbtransaction.Add(sBTransact);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSBTransact", new { id = sBTransact.TransactionId }, sBTransact);
        }

        // DELETE: api/SBTransacts/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSBTransact(int id)
        {
            var sBTransact = await _context.Sbtransaction.FindAsync(id);
            if (sBTransact == null)
            {
                return NotFound();
            }

            _context.Sbtransaction.Remove(sBTransact);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SBTransactExists(int id)
        {
            return _context.Sbtransaction.Any(e => e.TransactionId == id);
        }


        [HttpGet]
        [Route("GetAccountDetails")]
        public async Task<List<SBTransact>> GetProduct()
        {
            string BaseUrl = "http://localhost:18857/";
            var AccInfo = new List<SBTransact>();
            //HttpClient cl = new HttpClient();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BaseUrl);
                client.DefaultRequestHeaders.Clear();
                //Define request data format  
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                //Sending request to find web api REST service resource GetAllEmployees using HttpClient  
                HttpResponseMessage Res = await client.GetAsync("api/Accounts");
                //Checking the response is successful or not which is sent using HttpClient  
                if (Res.IsSuccessStatusCode)
                {
                    //Storing the response details recieved from web api   
                    var AccResponse = Res.Content.ReadAsStringAsync().Result;

                    //Deserializing the response recieved from web api and storing into the Employee list  
                    AccInfo = JsonConvert.DeserializeObject<List<SBTransact>>(AccResponse);

                }
                //returning the employee list to view  
                return AccInfo;
            }
        }
    }
}