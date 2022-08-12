using Newtonsoft.Json.Converters;

namespace AdapterImec.Application.Infrastructure.Converter
{
    public class IsoDateConverter : IsoDateTimeConverter
    {
        public IsoDateConverter() =>
            this.DateTimeFormat = Culture.DateTimeFormat.ShortDatePattern;
    }
}
