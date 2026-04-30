using System.Runtime.CompilerServices;

namespace Backend.Backend.Helper
{
    public static class ExtractDocuSer
    {
        /// <summary>
        /// Extract The Id from Document Series
        /// </summary>
        /// <param name="DocumentSeries"></param>
        /// <returns></returns>
        public static (int ExtractedId, int statusCode) GetIdByExtractingDocumentSeries(this string DocumentSeries)
        {
            string[] Data = DocumentSeries.Split('-');

            if (string.IsNullOrEmpty(Data[2]))
                return (0, 404);

            if (int.TryParse(Data[2], out int Id))
                return (Id, 200);

            return (0, 422);
        }
    }
}
