//Controller qui permet de récupérer l'administrateur via l'api

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


[Route("api/[controller]")]
[ApiController]

public class AdministrateurApiController : ControllerBase
{
    private readonly MvcJournalContext _context;

    public AdministrateurApiController(MvcJournalContext context)
    {
        _context = context;
    }

    // GET: api/AdApi
    public async Task<ActionResult<IEnumerable<Administrateur>>> GetAdministrateur()
    {
        return await _context.Administrateurs.OrderBy(s => s.Pseudo).ToListAsync();
    }


}