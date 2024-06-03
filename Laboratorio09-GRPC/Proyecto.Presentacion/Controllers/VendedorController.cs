using Microsoft.AspNetCore.Mvc;

//Librerías
using Proyecto.Presentacion.Models;
using Grpc.Net.Client;
using GrpcServiceBdVentas;
using GrpcServiceBdVentas.Protos;
using Vendedor = GrpcServiceBdVentas.Protos.Vendedor;

namespace Proyecto.Presentacion.Controllers
{
    public class VendedorController : Controller
    {
        //
        private Vendedor.VendedorClient _cliente;
        
        //
        public async Task<IActionResult> listadoVendedores()
        {
            var _canal = GrpcChannel.ForAddress("https://localhost:7019");
            _cliente = new Vendedor.VendedorClient(_canal);

            var request = new Empty();
            var mensaje = await _cliente.listadoVendedoresAsync(request);

            List<VendedorModel> aVendedores = new List<VendedorModel>();

            foreach ( var item in mensaje.Items )
            {
                aVendedores.Add(new VendedorModel()
                {
                    ide_ven = item.IdeVen,
                    nom_ven = item.NomVen,
                    sue_ven = item.SueVen,
                    fec_ing = item.FecIng,
                    nom_dis = item.NomDis,
                });
            }
            return View(aVendedores);
        }

        public IActionResult Index()
        {
            return View();
        }

    }
}
