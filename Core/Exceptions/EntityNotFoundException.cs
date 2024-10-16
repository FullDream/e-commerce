namespace Core.Exceptions;

public class EntityNotFoundException(string entityName, object key)
	: Exception($"Entity \"{entityName}\" ({key}) was not found.");