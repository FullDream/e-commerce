using System.Reflection;

namespace Api;

public class TypeInspector<T>
{
	public string[] SimplePropertyNames { get; init; }
	public string[] NavigationPropertyNames { get; init; }

	public TypeInspector()
	{
		PropertyInfo[] properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

		List<string> simpleList = [];
		List<string> navigationsList = [];


		foreach (var property in properties)
		{
			if (IsSimpleType(property.PropertyType))
				simpleList.Add(property.Name);
			else
				navigationsList.Add(property.Name);
		}

		simpleList.ForEach(Console.WriteLine);

		Console.WriteLine("navigation");
		navigationsList.ForEach(Console.WriteLine);

		SimplePropertyNames = simpleList.ToArray();
		NavigationPropertyNames = navigationsList.ToArray();
	}

	private static bool IsSimpleType(Type type)
	{
		return type.IsPrimitive
		       || type == typeof(string)
		       || type == typeof(DateTime)
		       || type == typeof(Guid)
		       || type.IsEnum;
	}
}