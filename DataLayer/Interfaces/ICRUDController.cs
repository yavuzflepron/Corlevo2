using Microsoft.AspNetCore.Mvc;

namespace DataLayer.Interfaces
{
    internal interface ICRUDController
    {
        ActionResult<ICollection<IEntity>> Get();
    }
}