using System;
using System.IO;
using System.Reflection;

namespace ProjectWebApi.Core.Services.Excel
{
    public abstract class BaseExcelBuilder
    {
        protected Stream CopyResourceFile(string resourceFilePath)
        {
            var assembly = Assembly.GetExecutingAssembly();
            var memoryStream = new MemoryStream();

            using (var resourceStream = assembly.GetManifestResourceStream(resourceFilePath))
            {
                resourceStream.CopyTo(memoryStream);
            }

            memoryStream.Position = 0;
            return memoryStream;
        }
    }
}
