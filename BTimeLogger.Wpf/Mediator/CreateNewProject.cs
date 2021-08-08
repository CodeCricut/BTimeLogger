using BTimeLogger.Csv.Services;
using BTimeLogger.Wpf.Services.AppData;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace BTimeLogger.Wpf.Mediator
{
	public class CreateNewProject : IRequest
	{
		public CreateNewProject(string newProjectCsvLocation)
		{
			NewProjectCsvLocation = newProjectCsvLocation;
		}

		public string NewProjectCsvLocation { get; }
	}

	public class CreateNewProjectHandler : IRequestHandler<CreateNewProject>
	{
		private readonly ICsvLocationPrincipal _csvLocationPrincipal;
		private readonly IMediator _mediator;
		private readonly IReportLocationsPrincipal _reportLocationsPrincipal;

		public CreateNewProjectHandler(ICsvLocationPrincipal csvLocationPrincipal,
			IMediator mediator,
			IReportLocationsPrincipal reportLocationsPrincipal)
		{
			_csvLocationPrincipal = csvLocationPrincipal;
			_mediator = mediator;
			_reportLocationsPrincipal = reportLocationsPrincipal;
		}

		public async Task<Unit> Handle(CreateNewProject request, CancellationToken cancellationToken)
		{
			// Save any pending changes
			bool? accepted = await _mediator.Send(new PromptToSaveUnsavedChanges());

			if (accepted.HasValue && accepted.Value)
			{
				await _mediator.Send(new ClearAllData());
				await _mediator.Send(new SaveAs(request.NewProjectCsvLocation));
			}

			return Unit.Value;
		}
	}
}
