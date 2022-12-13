namespace BusinessActions.Commands;

public abstract class CommandHandlerBase<T>
{
	public async Task ProcessAsync(CommandBase<T> command)
	{
		if (command.CommandType == CommandType.Execute)
		{
			await ExecuteAsync(command);
		}

		await UndoAsync(command);
	}

	public abstract Task ExecuteAsync(CommandBase<T> command);

	public abstract Task UndoAsync(CommandBase<T> command);
}
