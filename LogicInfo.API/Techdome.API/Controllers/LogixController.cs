using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Techdome.API.Model;

namespace Techdome.API.Controllers
{
    public class LogixController : Controller
    {
        // GET: LogixController
        private readonly InlineDatabaseContext Context;
        public LogixController(InlineDatabaseContext context)
        {
            Context = context;
        }

        // POST: LogixController/Create
        [HttpGet("getAll/unitMaster")]
        /*[ValidateAntiForgeryToken]*/
        public async Task<IEnumerable<UnitMaster>> GetAllInUnitMaster()
        {
            List<UnitMaster> unitMasterList = await Context.Untmst.ToListAsync();
            return unitMasterList;
        }
        [HttpPost("add/unitMaster")]
        public ActionResult AddInUnitMaster([FromBody] UnitMasterInput unitMasterInput)
        {   
            if(unitMasterInput.Name == null || unitMasterInput.Group == null || unitMasterInput.Desc == null)
            {
                return NotFound();
            }

            List<UnitMaster> allUnitMaster = Context.Untmst.ToList();
            int alreadyPresent = allUnitMaster.FindIndex(x => x.Name == unitMasterInput.Name && x.Group == unitMasterInput.Group && x.Desc == unitMasterInput.Desc);
            if(alreadyPresent != -1)
            {
                return NotFound();
            }
            int index = 1;
            while(true)
            {
                UnitMaster findUnitMaster = allUnitMaster.Find(x => x.Id == index);
                if (findUnitMaster == null) break;
                index++;
            }
            UnitMaster unitMaster = new UnitMaster();
            unitMaster.Id = index;
            unitMaster.Name = unitMasterInput.Name;
            unitMaster.Group = unitMasterInput.Group;
            unitMaster.Desc = unitMasterInput.Desc;
            try
            {
                Context.Untmst.Add(unitMaster);
                Context.SaveChangesAsync();
            }
            catch(Exception ex)
            {
                Console.WriteLine("Error occured in Adding Unit Master Row: {0}", ex);
                throw;
            }
            UnitMaster thisRow = Context.Untmst.FirstOrDefault(row => row.Name == unitMaster.Name && row.Group == unitMaster.Group && row.Desc == unitMaster.Desc);
            if(thisRow != null)
            {
                return Ok();
            }
            else
            {
                return NotFound();
            }
        }

        // GET: LogixController/Delete/5
        [HttpDelete("remove")]
        public ActionResult DeleteUnitMasterRow([FromQuery] UnitMasterInput unitMasterInput)
        {
            if (unitMasterInput.Name == null || unitMasterInput.Group == null || unitMasterInput.Desc == null)
            {
                return NotFound();
            }

            UnitMaster unitMaster = Context.Untmst.FirstOrDefault(x => x.Name == unitMasterInput.Name && x.Group == unitMasterInput.Group && x.Desc == unitMasterInput.Desc);
            if(unitMaster == null)
            {
                return NotFound();
            }
            Context.Untmst.Remove(unitMaster);
            Context.SaveChanges();
            return Ok();

        }
    }
}
