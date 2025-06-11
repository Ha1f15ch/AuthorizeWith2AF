using BusinessEngine.Commands.RefreshtokenCommand;
using DatabaseEngine;
using DatabaseEngine.DbModels;
using MediatR;

namespace BusinessEngine.Handlers.RefreshtokenCommandHandler
{
    public class AddRefreshtokenCommandHandler : IRequestHandler<AddRefreshtokenCommand, RefreshToken?>
    {
        private readonly AppDbContext _context;

        public AddRefreshtokenCommandHandler(AppDbContext appDbContext)
        {
            _context = appDbContext;
        }

        public async Task<RefreshToken?> Handle(AddRefreshtokenCommand command, CancellationToken cancellationToken)
        {
            return null;
        }
    }
}
