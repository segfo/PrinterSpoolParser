﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text;
using System.IO;
using EMFSpool;

namespace PrinterSpoolParseW
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var argsDict = args.Select(arg => arg.Split('=')).Where(s => s.Length == 2).ToDictionary(v => v[0], v => v[1]);
            var spool = argsDict.TryGetValue("spl",out var spl)? spl : "FP00000.SPL";
            var output = argsDict.TryGetValue("OutFile", out var opval) ? opval : "out";

            FileStream spoolFileStream = new FileStream(spool, FileMode.Open, FileAccess.Read);
            BinaryReader spoolReader = new BinaryReader(spoolFileStream, Encoding.Unicode);
            EMFSpoolFile spoolFile = new EMFSpoolFile(spoolReader);
            int pageNumber = 1;
            foreach (EMFPage page in spoolFile.Pages) {
                page.PageImage.Save(output + "_page"+pageNumber+".png");
                page.Thumbnail.Save(output + "_page" + pageNumber + "_Thumbnail.png");
            }
        }
    }
}
