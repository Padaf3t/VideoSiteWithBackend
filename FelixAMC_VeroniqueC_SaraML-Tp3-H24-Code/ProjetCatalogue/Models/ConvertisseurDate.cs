using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System.Linq.Expressions;

namespace ProjetCatalogue.Models
{
    public class ConvertisseurDate : ValueConverter<DateOnly, DateTime>
    {
        public ConvertisseurDate(Expression<Func<DateOnly, DateTime>> convertToProviderExpression, Expression<Func<DateTime, DateOnly>> convertFromProviderExpression, ConverterMappingHints? mappingHints = null) : base(convertToProviderExpression, convertFromProviderExpression, mappingHints)
        {
        }

        public override DateTime ConvertToProvider(DateOnly dateOnly)
        {
            return dateOnly.ToDateTime(TimeOnly.MinValue);
        }

        public override DateOnly ConvertFromProvider(DateTime dateTime)
        {
            return dateTime.Date;
        }
    }
}
