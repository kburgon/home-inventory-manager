namespace BusinessActions.Commands;

/// <summary>
/// Base command object that provides a core command structure.
/// </summary>
public abstract class CommandBase<T>
{
	/// <summary>
	/// Determines the action type that should be performed with the command.
	/// </summary>
	/// <remarks>
	/// See <see ref="CommandAction"/> for types of actions that can be taken.
	/// </remarks>
	public CommandAction CommandAction { get; set; } = CommandAction.Execute;

	/// <summary>
	/// The data that is required to execute the command.
	/// </summary>
	public T Data { get; private set; }

	/// <summary>
	/// The constructor of the object, which sets the value of the command data.
	/// </summary>
	public CommandBase(T data)
	{
		Data = data;
	}
}

/// <summary>
/// The type of action to perform for the command.
/// </summary>
/// <remarks>
/// "Actions" in this sense are actions noted in a typical command pattern,
/// such as an execution action and an undo action.
public enum CommandAction
{
	Execute,
	Undo
}
