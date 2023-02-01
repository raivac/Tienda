using System.Data;
using System.Data.SqlClient;

namespace Tienda.Models
{
    
    public class MantenimientoArticulos
    {
        private SqlConnection con;
        private void Conectar()
        {
            var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appSettings.json", optional: true,reloadOnChange: true);
            IConfiguration _configuration = builder.Build();
            string constr =_configuration.GetConnectionString("DefaultConnection");
        
        
            con = new SqlConnection(constr);

        
        
        }
        public int Alta(Articulo art){
    
            Conectar();
            SqlCommand cmd = new SqlCommand("insert into articulos (codigo,descripcion,precio) values(@codigo,@descripcion,@precio)",con);
            cmd.Parameters.Add("@codigo", System.Data.SqlDbType.Int);
            cmd.Parameters.Add("@descripcion", System.Data.SqlDbType.VarChar);
            cmd.Parameters.Add("@precio", System.Data.SqlDbType.Float);

            cmd.Parameters["@codigo"].Value = art.Codigo.ToString();
            cmd.Parameters["@descripcion"].Value = art.Descripcion.ToString();
            cmd.Parameters["@precio"].Value = art.Precio.ToString();


            con.Open();
            int i =  cmd.ExecuteNonQuery();
            con.Close();
            return i;   
        }

        public List<Articulo> GetAll()
        {
            Conectar();
            List < Articulo > articulos= new List < Articulo >();       
            string sql = "select codigo,descripcion,precio from articulos";
            SqlCommand comando = new SqlCommand(sql,con);
            con.Open();
            SqlDataReader registros =  comando.ExecuteReader();
            while(registros.Read()) {

                Articulo art = new Articulo();
                art.Codigo = int.Parse(registros["codigo"].ToString());
                art.Descripcion =registros["descripcion"].ToString();
                art.Precio = float.Parse(registros["precio"].ToString());

                articulos.Add(art); 
            }
            return articulos;
        }

        public int Borrar(int codigo) {

            Conectar();

            string sql = "delete from articulos where codigo=@codigo";

            SqlCommand sentencia = new SqlCommand(sql,con);

            sentencia.Parameters.Add("@codigo", SqlDbType.Int);
            sentencia.Parameters["@codigo"].Value = codigo;

            con.Open() ;
            int i = sentencia.ExecuteNonQuery();
            con.Close();
            return i;
        }
    
    }
}
