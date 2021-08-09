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

            using (ResXResourceWriter resxdef = new ResXResourceWriter("Resource.def.resx"))
            using (ResXResourceWriter resxTr = new ResXResourceWriter("Resource.tr.resx"))
            using (ResXResourceWriter resxEn = new ResXResourceWriter("Resource.en.resx"))
            using (ResXResourceWriter resxDe = new ResXResourceWriter("Resource.de.resx"))
            using (ResXResourceWriter resxRu = new ResXResourceWriter("Resource.ru.resx"))
            using (ResXResourceWriter resxRo = new ResXResourceWriter("Resource.ro.resx"))
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
            var filePath = "Resource." + $"{languages.ToLower()}.resx";
            return new ResxClass
            {
                ByteData = System.IO.File.ReadAllBytes(filePath),
                Type = "application/octet-stream",
                FileName = "Resource." + $"{languages.ToLower()}.resx"
            };
        }
    }
}
