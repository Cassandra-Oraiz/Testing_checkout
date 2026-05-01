using System.Runtime.CompilerServices;

namespace Backend.Backend.Helper
{
    public static class ExtractDocuSer
    {
        /// <summary>
        /// Extract The Id from Document Series
        /// </summary>
        /// <param name="DocumentSeries"></param>
        /// <param name="position"></param>
        /// <returns></returns>
        public static (int ExtractedId, int statusCode) ExtractDataFromDocumentSeries(this int position, string DocumentSeries)
        {
            if (position <= 0 || position > 3)
                return (0, 401); // Unathorized

            string[] Data = DocumentSeries.Split('-');

            if (string.IsNullOrEmpty(Data[position - 1]))
                return (0, 404); // Null or Empty ID

            if (int.TryParse(Data[position - 1], out int Id)) // Extract
                return (Id, 200);

            return (0, 422);
        }
    }
}
