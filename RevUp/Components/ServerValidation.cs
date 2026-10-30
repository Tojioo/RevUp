using Microsoft.AspNetCore.Components.Forms;
using RevUp.Models;

namespace RevUp.Components;

/// <summary>Shows the server's validation errors at their form fields and clears each one as soon as its field changes.</summary>
public sealed class ServerValidation
{
	private readonly ValidationMessageStore _messages;

	public ServerValidation(object model)
	{
		EditContext = new EditContext(model);
		_messages = new ValidationMessageStore(EditContext);
		EditContext.OnFieldChanged += (_, args) => _messages.Clear(args.FieldIdentifier);
	}

	public EditContext EditContext { get; }

	public void Show(IReadOnlyList<ValidationError> errors)
	{
		_messages.Clear();
		foreach (var error in errors)
		{
			_messages.Add(EditContext.Field(error.Field), error.Message);
		}

		EditContext.NotifyValidationStateChanged();
	}
}