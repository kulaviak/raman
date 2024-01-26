using System.Text;

namespace TurboLabs.EMOS.Common
{
    /// <summary>
    /// Util class for formatting exception to string.
    /// </summary>
    public class ExceptionFormatter
    {
        /// <summary>
        /// Convert exception and it's inner exceptions into string.
        /// </summary>
        public string ToString(Exception ex)
        {
            try
            {
                var ret = "";
                var e = ex;
                do
                {
                    ret += ExceptionToString(e);
                    e = e.InnerException;
                } while (e != null);
                return ret;
            }
            // make sure that formatting exception doesn't throw another exception
            catch (Exception ex2)
            {
                return $"An error {ex2.Message} occurred during formatting other exception: {ex.Message}. Original exception is not available.";
            }
        }

        private static string ExceptionToString(Exception ex)
        {
            if (ex == null) return null;
            var sb = new StringBuilder();
            sb.AppendLine(ex.Message);
            sb.AppendLine(ex.StackTrace);
            return sb.ToString();
        }
    }
}