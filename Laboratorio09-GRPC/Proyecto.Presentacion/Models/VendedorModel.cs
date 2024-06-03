using System.ComponentModel;

namespace Proyecto.Presentacion.Models
{
    public class VendedorModel
    {
        [DisplayName("CÓDIGO")]
        public int ide_ven { get; set; }

        [DisplayName("VENDEDOR")]
        public string? nom_ven { get; set; }

        [DisplayName("SUELDO S/. ")]
        public Double sue_ven { get; set; }

        [DisplayName("FECHA DE INGRESO")]
        public string fec_ing { get; set; }

        [DisplayName("DISTRITO")]
        public string? nom_dis { get; set; }
    }
}
