using ARCLanguageApi.Entites;
using ARCLanguageApi.EntityFrameworkCore;
using ARCLanguageApi.Resx;
using ICSharpCode.Decompiler.Util;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using System.Web;
using System.Net.Http;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.StaticFiles;

namespace ARCLanguageApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ARCLanguageApiController : ControllerBase
    {
        private readonly ARCLanguageContext _arcContext;

        public ARCLanguageApiController(ARCLanguageContext context)
        {
            _arcContext = context;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ARCLANGUAGE>>> GetArcs()
        {
            //return await _arcContext.ARCLanguages.OrderBy(x=> x.RESOURCE_ORIGIN).ThenBy(x=> x.KEY_NAME).ToListAsync();
            return await _arcContext.ARCLanguages.Where(x => x.IsActive).OrderBy(x => x.KEY_NAME).ToListAsync();
        }

        [HttpGet("getFile/{languages}")]
        public async Task<ActionResult> DownloadFile(string languages)
        {
            var Data = _arcContext.ARCLanguages.ToList();
            var resxData = MakeResx.BuildResx(_arcContext, languages);
            return File(resxData.ByteData, resxData.Type, resxData.FileName);
        }


        [HttpGet("search/{key}")]
        public List<ARCLANGUAGE> SearchArc(string key)
        {
            var data = _arcContext.ARCLanguages.Where
                (x => x.IsActive && (x.KEY_NAME.Contains(key) ||
                x.KEY_VALUE.Contains(key) ||
                x.KEY_VALUE_EN.Contains(key) ||
                x.KEY_VALUE_DE.Contains(key) ||
                x.KEY_VALUE_RU.Contains(key) ||
                x.KEY_VALUE_RO.Contains(key) ||
                x.KEY_COMMENT.Contains(key) ||
                x.KEY_VALUE_TR.Contains(key))).ToList();

            return data;
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<ARCLANGUAGE>> GetArc(int id)
        {
            var data = await _arcContext.ARCLanguages.FindAsync(id);

            if (data == null)
            {
                return NotFound();
            }

            return data;
        }
        [HttpPost]
        public async Task<ActionResult<ARCLANGUAGE>> PostArc([FromBody] ARCLANGUAGE arcLanguage) //ASYNC
        {
            bool anyKeyName = _arcContext.ARCLanguages.Where(x => x.KEY_NAME == arcLanguage.KEY_NAME).Any();

            if (anyKeyName)
            {
                throw new DuplicateDataInsertException( arcLanguage.KEY_NAME+ " already exists in database, you should try different key name" );
            }
            arcLanguage.IsActive = true;
            arcLanguage.CreateDate = DateTimeOffset.Now;
            arcLanguage.CreaterApp = "app";
            arcLanguage.CreatorAppVersion = "v1";

            _arcContext.ARCLanguages.Add(arcLanguage);
            await _arcContext.SaveChangesAsync();

            return CreatedAtAction("GetArcs", arcLanguage);

        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutArc(int id, ARCLANGUAGE arcLanguageUpdate) //ASYNC
        {
            if (id != arcLanguageUpdate.ID)
            {
                return BadRequest();
            }

            _arcContext.Entry(arcLanguageUpdate).State = EntityState.Modified;

            try
            {
                await _arcContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {

            }

            return NoContent();
        }

        [HttpPut("isActive/{id}")]
        public async Task<IActionResult> EditArcActive(int id) //ASYNC
        {
            ARCLANGUAGE arcLanguageUpdate = new ARCLANGUAGE();
            arcLanguageUpdate.ID = id;
            arcLanguageUpdate.IsActive = false;

            if (id != arcLanguageUpdate.ID)
            {
                return BadRequest();
            }
            var entry = _arcContext.Entry(arcLanguageUpdate);
            entry.State = EntityState.Unchanged;
            entry.Property("IsActive").IsModified = true;

            try
            {
                await _arcContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {

            }

            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<ARCLANGUAGE>> DeleteArc(int id)
        {
            var data = await _arcContext.ARCLanguages.FindAsync(id);
            if (data == null)
            {
                return NotFound();
            }

            _arcContext.ARCLanguages.Remove(data);
            await _arcContext.SaveChangesAsync();

            return data;
        }
    }

}
