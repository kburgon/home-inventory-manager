namespace BusinessActions.Commands;

/// <summary>
/// The base class containing structural elements for any
/// command handler type.
/// </summary>
public abstract class CommandHandlerBase<T>
{
	public async Task ProcessAsync(CommandBase<T> command)
	{
		if (command.CommandAction == CommandAction.Execute)
		{
			await ExecuteAsync(command);
		}

		await UndoAsync(command);
	}

	public abstract Task ExecuteAsync(CommandBase<T> command);

	public abstract Task UndoAsync(CommandBase<T> command);
}
