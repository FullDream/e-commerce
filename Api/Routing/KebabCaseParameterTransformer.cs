using System.Text.RegularExpressions;

namespace Api.Routing;

public partial class KebabCaseParameterTransformer : IOutboundParameterTransformer
{
	public string? TransformOutbound(object? value)
	{
		return value == null ? null : Regex().Replace(value.ToString()!, "$1-$2").ToLower();
	}

	[GeneratedRegex("([a-z])([A-Z])")]
	private static partial Regex Regex();
}