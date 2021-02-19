using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iPassport.Domain.Utils
{
    public class FormattedDate
    {
        public DateTimeOffset date { get; }

        public FormattedDate(DateTimeOffset date)
        {
            this.date = date;
        }

        public override string ToString()
        {
            return date.ToString("yyyy-MM-ddTHH:mm:ss.fffzzz", CultureInfo.InvariantCulture);
        }
    }
}
