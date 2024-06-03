//Librerías
using GrpcServiceBdVentas.Protos;
using Microsoft.Data.SqlClient;
using System.Data;
using Grpc.Core;
using System.Xml.Linq;

namespace GrpcServiceBdVentas.Services
{                               //Service declarado en vendedor.proto
    public class VendedorService: Vendedor.VendedorBase
    {
        string cadena = "server=SUITEE102-ST22;database=bd_ventas;" +
                   "uid=sa;pwd=sql;" +
                    "TrustServerCertificate=False;Encrypt=False";

        private readonly ILogger<VendedorService> _logger; //Define la cadena de conexión
        private List<vendedorResponse> vendedorResponse; //

        public VendedorService(ILogger<VendedorService> logger)
        {
            _logger = logger;
            vendedorResponse = listadoVendedores();
        }


        //Lista de vendedores
        private List<vendedorResponse> listadoVendedores()
        {
            List<vendedorResponse> aVendedores = new List<vendedorResponse>();
            using (SqlConnection cn = new SqlConnection(cadena))
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand("SP_LISTADO_VENDEDORES_VISTA_CLIENTE", cn);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read()) 
                {
                    aVendedores.Add(new vendedorResponse() 
                    { 
                        IdeVen = Int32.Parse(dr[0].ToString()),
                        NomVen = dr[1].ToString(),
                        SueVen = Double.Parse(dr[2].ToString()),
                        FecIng = dr[3].ToString(), 
                        NomDis = dr[4].ToString(),
                    });
                }
                cn.Close();
            }
            return aVendedores;
        }

        //Devuelve vista
        public override Task<vendedorListResponse> listadoVendedores(Empty request, ServerCallContext context)
        {
            vendedorListResponse response = new vendedorListResponse();
            response.Items.AddRange(vendedorResponse);
            return Task.FromResult(response);
        }

    }
}
