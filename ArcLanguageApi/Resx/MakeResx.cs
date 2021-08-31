using ARCLanguageApi.EntityFrameworkCore;
using ARCLanguageApi.Resx;
using ICSharpCode.Decompiler.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ARCLanguageApi.Resx
{
    public static class MakeResx
    {

        public static ResxClass BuildResx(ARCLanguageContext _arcContext, string languages)
        {
            var Data = _arcContext.ARCLanguages.ToList();

            string filepathFormat = System.IO.Path.Combine(System.IO.Directory.GetCurrentDirectory(), "ResourceFiles", "{0}");

            using (ResXResourceWriter resxdef = new ResXResourceWriter(string.Format(filepathFormat, "Resource.def.resx")))
            using (ResXResourceWriter resxTr = new ResXResourceWriter( string.Format(filepathFormat, "Resource.tr.resx")))
            using (ResXResourceWriter resxEn = new ResXResourceWriter( string.Format(filepathFormat, "Resource.en.resx")))
            using (ResXResourceWriter resxDe = new ResXResourceWriter( string.Format(filepathFormat, "Resource.de.resx")))
            using (ResXResourceWriter resxRu = new ResXResourceWriter( string.Format(filepathFormat, "Resource.ru.resx")))
            using (ResXResourceWriter resxRo = new ResXResourceWriter( string.Format(filepathFormat, "Resource.ro.resx")))
            {
                foreach (var allData in Data)
                {
                    if (!string.IsNullOrEmpty(allData.KEY_VALUE))
                        resxdef.AddResource(allData.KEY_NAME, allData.KEY_VALUE);
                    if (!string.IsNullOrEmpty(allData.KEY_VALUE_TR))
                        resxTr.AddResource(allData.KEY_NAME, allData.KEY_VALUE_TR);
                    if (!string.IsNullOrEmpty(allData.KEY_VALUE_EN))
                        resxEn.AddResource(allData.KEY_NAME, allData.KEY_VALUE_EN);
                    if (!string.IsNullOrEmpty(allData.KEY_VALUE_DE))
                        resxDe.AddResource(allData.KEY_NAME, allData.KEY_VALUE_DE);
                    if (!string.IsNullOrEmpty(allData.KEY_VALUE_RU))
                        resxRu.AddResource(allData.KEY_NAME, allData.KEY_VALUE_RU);
                    if (!string.IsNullOrEmpty(allData.KEY_VALUE_RO))
                        resxRo.AddResource(allData.KEY_NAME, allData.KEY_VALUE_RO);
                }
            }
            var fileName = $"Resource.{languages.ToLower()}.resx";
            var filePath = string.Format(filepathFormat, fileName);
            return new ResxClass
            {
                ByteData = System.IO.File.ReadAllBytes(filePath),
                Type = "application/octet-stream",
                FileName = fileName
            };
        }
    }
}
