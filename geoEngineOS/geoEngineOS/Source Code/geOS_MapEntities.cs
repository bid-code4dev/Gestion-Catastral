using System;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Text;
using System.Drawing;

namespace geoEngineOS.Entity
{
    using geoEngineOS.Map;
    using geoEngineOS.Connection;
    using geoEngineOS.Locations;

    /// <summary>
    /// CLASE QUE GESTIONA EL SOPORTE PARA LAS CONJUNTOS DE ENTIDADES
    /// UN CONJUNTO DE ENTIDADES PUEDE SER OBTENIDO MEDIANTE SELECCION POR PARTE DEL USUARIO O
    /// COMO UNA EXTRACCIÓN DESDE UN TEMA MEDIANTE UN FILTRO ALFANUMÉRICO
    /// UNA ENTIDAD ESTA ASOCIADA A UNA CONEXIÓN Y PUEDE SER USADA EN CUALQUIER VENTANA DE MAPA...
    /// </summary>
    public class geOS_MapEntities
    {
        private geOS_EtgConnection _Conexion;
        private Int32 _nIdEntidades;

        /// <summary>
        /// IDENTIFICADOR DEL CONJUNTO DE ENTIDADES
        /// </summary>
        public Int32 Id
        {
            get { return _nIdEntidades; }
        }

        /// <summary>
        /// CONEXIÓN A LA QUE ESTÁ ASOCIADO EL CONJUNTO
        /// </summary>
        public geOS_EtgConnection Connection
        {
            get { return _Conexion; }
        }

        public geOS_MapEntities()
        {
            this._nIdEntidades = -1;
        }
        public geOS_MapEntities(geOS_EtgConnection Conexion, Int32 nIdEntidades)
        {
            this._Conexion = Conexion;
            this._nIdEntidades = nIdEntidades;
        }
        public geOS_MapEntities(geOS_MapEntities Entidades)
        {
            this._Conexion = Entidades.Connection;
            this._nIdEntidades = Entidades.Id;
        }

        ~geOS_MapEntities()
        {
            Free();
        }

        /// <summary>
        /// LIBERA UN CONJUNTO DE ENTIDADES
        /// </summary>
        public void Free()
        {
            if (this._Conexion != null && this._nIdEntidades >= 0)
                geOS_Gestor.FreeEntidades(this._Conexion.ConnectionId, this._nIdEntidades);

            this._nIdEntidades = -1;
            this._Conexion = null;
        }

        /// <summary>
        /// DEVUELVE EL NÚMERO DE ENTIDADES DEL CONJUNTO
        /// </summary>
        /// <returns></returns>
        public int GetNumEntities()
        {
            return geOS_Gestor.GetNumEntidadesConjunto(this._Conexion.ConnectionId, this._nIdEntidades);
        }

        /// <summary>
        /// AÑADE MANUALMENTE UNA ENTIDAD AL CONJUNTO DE ENTIDADES
        /// </summary>
        /// <param name="Entity"></param>
        /// <returns></returns>
        public bool AddEntity(geOS_MapEntity Entity)
        {
            return geOS_Gestor.AddEntidadConjunto(this._Conexion.ConnectionId, this._nIdEntidades, Entity.Id);
        }

        /// <summary>
        /// RETORNA UNA ENTIDAD DEL CONJUNTO DE ENTIDADES
        /// </summary>
        /// <param name="Index">INDICE A LA ENTIDAD (BASE 0)</param>
        /// <returns>NULO SI HAY ERROR</returns>
        public geOS_MapEntity GetEntity(short Index)
        {
            int IdEntidad = geOS_Gestor.GetEntidadConjunto(this._Conexion.ConnectionId, this._nIdEntidades, Index);
            return IdEntidad < 0 ? null : new geOS_MapEntity(this._Conexion, IdEntidad);
        }

        /// <summary>
        /// DEVUELVE EL GEOCODIGO DE UNA ENTIDAD DEL CONJUNTO DE ENTIDADES
        /// </summary>
        /// <param name="Index">INDICE A LA ENTIDAD (BASE 0)</param>
        /// <param name="Geocode">GEOCODIGO DEVUELTO</param>
        /// <returns></returns>
        public bool GetEntityGeocode(short Index,ref string Geocode)
        {
            StringBuilder Aux = new StringBuilder(255);

            if( geOS_Gestor.GetGeocodigoEntidadConjunto(this._Conexion.ConnectionId, this._nIdEntidades, Index, Aux) == 0 )
                return false;

            Geocode = Aux.ToString();
            return true;
        }

        /// <summary>
        /// HACE UN ZOOM AL CONJUNTO DE ENTIDADES EN LA VENTANA DE MAPA
        /// </summary>
        /// <param name="Map">MAPA DONDE SE DESEA HACER EL ZOOM AL CONJUNTO DE ENTIDADES</param>
        public void Zoom(geOS_MapWindow Map)
        {
            geOS_Gestor.ZoomConjuntoEntidades(Map.Connection.ConnectionId, Map.MapId,this._nIdEntidades);
        }

        /// <summary>
        /// HACE UN FLASH DEL CONJUNTO DE ENTIDADES
        /// </summary>
        /// <param name="Map">MAPA DONDE SE DESEA HACER EL FLASH AL CONJUNTO DE ENTIDADES</param>
        /// <param name="Times">NUMERO DE VECES QUE SE HACE FLASH</param>
        public void Flash(geOS_MapWindow Map,short Times)
        {
            geOS_Gestor.FlashConjuntoEntidades(Map.Connection.ConnectionId, Map.MapId, this._nIdEntidades, Times);
        }

        /// <summary>
        /// RETORNA UNA LOCALIZACIÓN CONSTRUIDA A PARTIR DEL ESTE CONJUNTO DE ENTIDADES
        /// </summary>
        /// <param name="Map">MAPA DONDE SE DESEA CREAR LA LOCALIZACIÓN</param>
        /// <param name="LocColor">COLOR DE LA LOCALIZACIÓN</param>
        /// <param name="LocStyle">ESTILO DE LA LOCALIZACIÓN
        ///                         PUNTO: ENUMERACIÓN geOS_MarkerType EXCEPTO EL VALOR moTrueTypeMarker
        ///                         LINEA: ENUMERACIÓN geOS_LineType
        ///                         POLIGONO: ENUMERACIÓN geOS_FillType
        /// </param>
        /// <param name="LocSize">TAMAÑO DE LA LOCALIZACIÓN
        ///                         PUNTO: TAMAÑO DEL PUNTO EN METROS
        ///                         LINEA: ANCHO DE LA LINEA EN PIXELES
        ///                         POLIGONO: ANCHO DEL BORDE DEL POLIGONO EN PIXELES
        /// <returns></returns>
        public geOS_Location ReturnLocation(geOS_MapWindow Map,Color LocColor, byte LocStyle, double Size)
        {
            int IdgeOS_Location = geOS_Gestor.EntidadesALocalizacion(Map.Connection.ConnectionId, Map.MapId, this._nIdEntidades, LocColor.ToArgb(), LocStyle, Size);
            return IdgeOS_Location < 0 ? null : new geOS_Location(Map,IdgeOS_Location);
        }
    }
}