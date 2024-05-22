using System.Globalization;
using CommunityToolkit.Maui.Converters;
using KlidecekIS.BL.Models;

namespace KlidecekIS.Converters;

public class ModelIsNewToIsVisibleInvertedConverter : BaseConverterOneWay<ModelBase, bool>
{
    public override bool ConvertFrom(ModelBase value, CultureInfo? culture)
        => value.Id != Guid.Empty;
    
    public override bool DefaultConvertReturnValue { get; set; } = true;
}