namespace BusinessActions.Commands;

public abstract class CommandBase<T>
{
	public CommandType CommandType { get; set; } = CommandType.Execute;

	public T Data { get; private set; }

	public CommandBase(T data)
	{
		Data = data;
	}
}

public enum CommandType
{
	Execute,
	Undo
}
