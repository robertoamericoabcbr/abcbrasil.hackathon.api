using Microsoft.AspNetCore.Http;
using System.IO;

namespace ABCBrasil.Hackthon.Api.Infra.Extensions
{
    /// <summary>
    /// Métodos de extensão para a interface <see cref="IFormFile"/>.
    /// </summary>
    public static class IFormFileExtensions
    {
        /// <summary>
        /// Obtém a extensão do arquivo do objeto <see cref="IFormFile"/>.
        /// Se o arquivo for uma planilha do Excel (.xlsx, .xls, .xlsm), a extensão retornada será ".xls".
        /// </summary>
        /// <param name="formFile">O arquivo para obter a extensão.</param>
        /// <returns>A extensão do arquivo.</returns>
        public static string GetExtension(this IFormFile formFile)
        {
            string extension = Path.GetExtension(formFile.FileName).Replace(".", "");

            bool isExcel = extension == "xlsx" || extension == "xls" || extension == "xlsm";

            if (isExcel)
            {
                return "xls";
            }

            return extension;
        }
    }
}