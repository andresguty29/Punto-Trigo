using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Abstracciones.Modelos.Usuario.Usuario;

namespace Abstracciones.Interfaces.API.UsuarioAPI
{
    public interface IUsuarioController
    {
        Task<IActionResult> Obtener();
        Task<IActionResult> Obtener(Guid Id);
        Task<IActionResult> Agregar(UsuarioRequest usuario);
        Task<IActionResult> Editar(Guid Id, UsuarioRequest usuario);
        Task<IActionResult> Eliminar(Guid Id);
    }
}
