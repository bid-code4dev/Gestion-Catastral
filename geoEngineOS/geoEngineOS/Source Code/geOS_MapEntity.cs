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
    /// CLASE QUE GESTIONA EL SOPORTE PARA LAS ENTIDADES
    /// UNA ENTIDAD ESTA ASOCIADA A UNA CONEXIÓN Y PUEDE SER USADA EN CUALQUIER VENTANA DE MAPA...
    /// </summary>
    public class geOS_MapEntity
    {
        private geOS_EtgConnection _Conexion;
        private Int32 _nIdEntidad;

        private string _sTheme;
        private string _sGeocode;

        /// <summary>
        /// IDENTIFICADOR DE LA ENTIDAD
        /// </summary>
        public Int32 Id
        {
            get { return _nIdEntidad; }
        }

        /// <summary>
        /// CONEXION A LA QUE ESTÁ ASOCIADA LA ENTIDAD
        /// </summary>
        public geOS_EtgConnection Connection
        {
            get { return _Conexion; }
        }

        /// <summary>
        /// TEMA DE LA ENTIDAD
        /// </summary>
        public string Theme
        {
            get { return this._sTheme; }
        }

        /// <summary>
        /// GECODIGO DE LA ENTIDAD
        /// </summary>
        public string GeoCode
        {
            get { return this._sGeocode; }
        }

        public geOS_MapEntity()
        {
            this._nIdEntidad = -1;
        }
        public geOS_MapEntity(geOS_EtgConnection Conexion, Int32 nIdEntidad)
        {
            this._Conexion = Conexion;
            this._nIdEntidad = nIdEntidad;

            string sThemeAux = new string(' ',100);
            string sGeocodeAux = new string(' ',100);

            if (this.GetParams(ref sThemeAux, ref sGeocodeAux))
            {
                this._sTheme = sThemeAux;
                this._sGeocode = sGeocodeAux;
            }
        }

        public geOS_MapEntity(geOS_MapEntity Entidad)
        {
            this._Conexion      = Entidad.Connection;
            this._nIdEntidad    = Entidad.Id;
            this._sTheme        = Entidad._sTheme;
            this._sGeocode      = Entidad._sGeocode;
        }

        ~geOS_MapEntity()
        {
            Free();
        }

        /// <summary>
        /// LIBERA UNA ENTIDAD
        /// </summary>
        public void Free()
        {
            if (this._Conexion != null && this._nIdEntidad >= 0)
                geOS_Gestor.FreeEntidad(this._Conexion.ConnectionId, this._nIdEntidad);

            this._nIdEntidad = -1;
            this._Conexion = null;
        }

        /// <summary>
        /// HACE UN FLASH DE LA ENTIDAD EN LA VENTANA DE MAPA QUE SE PASA COMO PARAMETRO
        /// </summary>
        /// <param name="Mapa">VENTANA DE MAPA EN LA QUE SE DESEA HACER EL FLASH</param>
        /// <param name="nTimes">NUMERO DE VECES QUE SE DESEA HACER EL FLASH</param>
        public void Flash(geOS_MapWindow Mapa,short nTimes)
        {
            geOS_Gestor.FlashEntidad(_Conexion.ConnectionId,Mapa.MapId,_nIdEntidad,nTimes);
        }

        /// <summary>
        /// MUESTRA EL SENTIDO DE LA ENTIDAD (SI ES LINEAL) EN LA VENTANA DE MAPA
        /// </summary>
        /// <param name="Mapa"></param>
        /// <returns></returns>
        public bool ShowEntitySense(geOS_MapWindow Mapa)
        {
            return geOS_Gestor.VerSentidoEntidad(_Conexion.ConnectionId,Mapa.MapId,_nIdEntidad);
        }

        /// <summary>
        /// HACE UN ZOOM A LA ENTIDAD EN LA VENTANA DE MAPA QUE SE PASA COMO PARAMETRO
        /// </summary>
        /// <param name="Mapa"></param>
        public void Zoom(geOS_MapWindow Mapa)
        {
            geOS_Gestor.ZoomEntidad(_Conexion.ConnectionId,Mapa.MapId,_nIdEntidad);
        }

        /// <summary>
        /// DEVUELVE LOS PARÁMETROS DE LA ENTIDAD
        /// </summary>
        /// <param name="sTheme">CÓDIGO DEL TEMA DE LA ENTIDAD</param>
        /// <param name="sGeocode">GEOCODIGO DE LA ENTIDAD</param>
        /// <returns>FALSE SI HAY ERROR</returns>
        public bool GetParams(ref string sTheme, ref string sGeocode)
        {
            StringBuilder sThemeAux = new StringBuilder(255);
            StringBuilder sGeocodeAux = new StringBuilder(255);

            if (geOS_Gestor.GetParamsEntidad(_Conexion.ConnectionId,_nIdEntidad,sThemeAux,sGeocodeAux) == 0)
                return false;

            this._sTheme = sTheme = sThemeAux.ToString();
            this._sGeocode = sGeocode = sGeocodeAux.ToString();
            return true;
        }

        /// <summary>
        /// RETORNA EL FEATUREID (CAMPO INTERNO DE IDENTIFICACIÓN) DE LA ENTIDAD
        /// </summary>
        /// <returns>-1 SI HAY ERROR</returns>
        public int GetFeatureID()
        {
            return geOS_Gestor.GetFeatureID(_Conexion.ConnectionId,_nIdEntidad);
        }

        /// <summary>
        /// ACTUALIZA EL GEOCODIGO ASOCIADO A LA ENTIDAD
        /// </summary>
        /// <param name="sGeocode"></param>
        /// <returns></returns>
        public bool SetGeocode(string sGeocode)
        {
            if( geOS_Gestor.SetGeocodigoEntidad(_Conexion.ConnectionId, _nIdEntidad, sGeocode) == 0 )
                return false;
            
            this._sGeocode = sGeocode;
            return true;
        }

        /// <summary>
        /// DEVUELVE EL AREA DE LA ENTIDAD (0 SI ES PUNTUAL O LINEAL)
        /// </summary>
        /// <returns></returns>
        public double GetArea()
        {
            return geOS_Gestor.GetAreaEntidad(_Conexion.ConnectionId, _nIdEntidad);
        }

        /// <summary>
        /// DEVUELVE LA LONGITUD DE LA ENTIDAD
        /// 0 SI LA ENTIDAD ES PUNTUAL
        /// PERIMETRO SI LA ENTIDAD ES SUPERFICIAL
        /// </summary>
        /// <returns></returns>
        public double GetLength()
        {
            return geOS_Gestor.GetLongitudEntidad(_Conexion.ConnectionId, _nIdEntidad);
        }

        /// <summary>
        /// DEVUELVE LA EXTENSIÓN DE LA ENTIDAD
        /// </summary>
        /// <param name="MinX">COORDENADA X MINIMA</param>
        /// <param name="MinY">COORDENADA Y MINIMA</param>
        /// <param name="MaxX">COORDENADA X MAXIMA</param>
        /// <param name="MaxY">COORDENADA Y MAXIMA</param>
        /// <returns>FALSE SI HAY ERROR</returns>
        public bool GetExtent(ref double MinX, ref double MinY, ref double MaxX, ref double MaxY)
        {
            return geOS_Gestor.GetExtentEntidad(_Conexion.ConnectionId, _nIdEntidad, ref MinX, ref MinY, ref MaxX, ref MaxY) == 0 ? false : true;
        }

        /// <summary>
        /// DEVUELVE EL NÚMERO DE COORDENADAS DE UNA ENTDAD
        /// </summary>
        /// <param name="nNumParts">OPCIONAL (PUEDE SER NULL) PARA RETORNAR EL NUMERO DE PARTES</param>
        /// <returns>NUMERO DE COORDENADAS (-1 SI HAY ERROR)</returns>
        public int GetNumCoords(ref short nNumParts)
        {
            return geOS_Gestor.GetNumCoordsEntidad(_Conexion.ConnectionId, _nIdEntidad, ref nNumParts);
        }

        /// <summary>
        /// DEVUELVE EL PAR (X,Y) DE LA COORDENADA QUE SE PASA COMO PARÁMETRO
        /// </summary>
        /// <param name="nIndex">INDICE A LA COORDENADA</param>
        /// <param name="X">VALOR X DE LA COORDENADA</param>
        /// <param name="Y">VALOR Y DE LA COORDENADA</param>
        /// <param name="nPart">OPCIONAL (PUEDE SER NULL) INDICE A LA PARTE</param>
        /// <returns>FALSE SI HAY ERROR</returns>
        public bool GetCoordinate(int nIndex, ref double X, ref double Y, ref short nPart)
        {
            return geOS_Gestor.GetCoordenadaEntidad(_Conexion.ConnectionId, _nIdEntidad,nIndex, ref X, ref Y, ref nPart);
        }

        /// <summary>
        /// DEVUELVE EL TIPO DE ENTIDAD
        /// </summary>
        /// <returns></returns>
        public geOS_EntityType GetEntityType()
        {
            return (geOS_EntityType)geOS_Gestor.GetEntidadShapeType(_Conexion.ConnectionId, _nIdEntidad);
        }

        /// <summary>
        /// DEVUELVE EL VALOR DE UN ATRIBUTO ALFANUMÉRICO ASOCIADO A LA ENTIDAD
        /// </summary>
        /// <param name="sAtrib">NOMBRE DEL ATRIBUTO</param>
        /// <param name="sValue">VALOR DEL ATRIBUTO</param>
        public void GetAtrib(string sAtrib, ref string sValue)
        {
            StringBuilder sAux = new StringBuilder(255);
            geOS_Gestor.GetAtribEntidad(_Conexion.ConnectionId, _nIdEntidad, sAtrib, sAux);

            sValue = sAux.ToString();
        }

        /// <summary>
        /// ACTUALIZA EL VALOR DE UN ATRIBUTO ALFANUMERICO ASOCIADO A LA ENTIDAD
        /// </summary>
        /// <param name="sAtrib">NOMBRE DEL ATRIBUTO</param>
        /// <param name="sValue">NUEVO VALOR DEL ATRIBUTO</param>
        public void SetAtrib(string sAtrib, string sValue)
        {
            geOS_Gestor.SetAtribEntidad(_Conexion.ConnectionId, _nIdEntidad, sAtrib, sValue);
        }

        /// <summary>
        /// DEVUELVE LA DISTANCIA EXISTENTE A LA ENTIDAD QUE SE PASA COMO PARÁMETRO
        /// </summary>
        /// <param name="Entity">ENTIDAD A LA QUE SE DESEA SABER LA DISTANCIA</param>
        /// <returns></returns>
        public double DistanceToEntity(geOS_MapEntity Entity)
        {
            return geOS_Gestor.DistanciaEntreEntidades(_Conexion.ConnectionId, this._nIdEntidad, Entity.Id);
        }

        /// <summary>
        /// CREA UNA LOCALIZACIÓN A PARTIR DE LA GEOMETRIA DE ESTA ENTIDAD
        /// </summary>
        /// <param name="Map">VENTANA DE MAPA DONDE SE DESEA CREAR LA LOCALIZACIÓN</param>
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
        public geOS_Location CreateLocation(geOS_MapWindow Map,Color LocColor, byte LocStyle, double Size)
        {
            int IdLoc = geOS_Gestor.EntidadALocalizacion(this._Conexion.ConnectionId, Map.MapId, this.Id, LocColor.ToArgb(), LocStyle, Size);
            return IdLoc < 0 ? null : new geOS_Location(Map, IdLoc);
        }

        /// <summary>
        /// DEVUELVE LA N-ENTIDADES MAS CERCANAS DE UN TEMA A LA ENTIDAD
        /// </summary>
        /// <param name="Map">MAPA DONDE SE DESEAN BUSCAR LAS ENTIDADES</param>
        /// <param name="Theme">CÓDIGO DEL TEMA DONDE SE BUSCAN LAS ENTIDADES</param>
        /// <param name="Filter">FILTRO ALFANUMÉRICO SOBRE EL TEMA (PUEDE SER VACIO)</param>
        /// <param name="MaxEntities">NÚMERO DE ENTIDADES MÁS CERCANAS A BUSCAR</param>
        /// <param name="Entities">ARRAY CON LAS ENTIDADES MÁS CERCANAS ENCONTRADAS</param>
        /// <param name="Distances">ARRAY CON LAS DISTANCIAS A CADA ENTIDAD</param>
        /// <returns></returns>
        public bool GetClosestEntities(geOS_MapWindow Map,string Theme, string Filter, short MaxEntities, ref geOS_MapEntity[] Entities, ref double[] Distances)
        {
            byte[] bIdEntidades = new byte[MaxEntities * sizeof(Int32)];
            byte[] bDistances = new byte[MaxEntities * sizeof(double)];

            if (geOS_Gestor.GetEntidadesMasCercanasE(Map.Connection.ConnectionId, Map.MapId, this._nIdEntidad,
                                MaxEntities, Theme, Filter, bIdEntidades, bDistances) == 0)
                return false;

            int[] IdEntidades = new int[MaxEntities];

            for (int i = 0; i < MaxEntities; i++)
            {
                IdEntidades[i] = BitConverter.ToInt32(bIdEntidades, i * 4);
                Distances[i] = BitConverter.ToDouble(bDistances, i * 8);
            }

            for (int i = 0; i < MaxEntities; i++)
                Entities[i] = new geOS_MapEntity(Map.Connection, IdEntidades[i]);

            return true;
        }

        /// <summary>
        /// EDITA UNA ENTIDAD (DEBE PERTENECER AL TEMA QUE SE ESTÁ EDITANDO ACTUALMENTE
        /// </summary>
        /// <param name="Mapa">VENTANA DE MAPA DONDE SE ESTÁ EDITANDO</param>
        /// <returns></returns>
        public bool Edit(geOS_MapWindow Mapa)
        {
            if (this._Conexion.ConnectionId < 0 || this._nIdEntidad < 0 || Mapa.MapId < 0)
                return false;

            return geOS_Gestor.EditarEntidad(this._Conexion.ConnectionId, Mapa.MapId, this._nIdEntidad);
        }

        /// <summary>
        /// BORRA UNA ENTIDAD (DEBE PERTENECER AL TEMA QUE SE ESTÁ EDITANDO ACTUALMENTE
        /// </summary>
        /// <param name="Mapa">VENTANA DE MAPA DONDE SE ESTÁ EDITANDO</param>
        /// <param name="bIncTransaction">SI SE DESEA INCREMENTAR LA TRANSACCIÓN DE EDICIÓN</param>
        /// <returns></returns>
        public bool Delete(geOS_MapWindow Mapa,bool bIncTransaction)
        {
            if (this._Conexion.ConnectionId < 0 || this._nIdEntidad < 0 || Mapa.MapId < 0 )
                return false;

            bool bRetorno = geOS_Gestor.BorrarEntidad(this._Conexion.ConnectionId, Mapa.MapId, this._nIdEntidad, bIncTransaction);

            // Una vez borrada la entidad ya no es válida
            this._nIdEntidad = -1;
            this._Conexion = null;
            return bRetorno;
        }

        /// <summary>
        /// EDITA UNA ENTIDAD (DEBE PERTENECER AL TEMA QUE SE ESTÁ EDITANDO ACTUALMENTE
        /// </summary>
        /// <param name="Mapa">VENTANA DE MAPA DONDE SE ESTÁ EDITANDO</param>
        /// <param name="fAngle">DEVUELVE EL ANGULO DE ROTACIÓN</param>
        /// <returns></returns>
        public bool Rotate(geOS_MapWindow Mapa,ref double fAngle)
        {
            if (this._Conexion.ConnectionId < 0 || this._nIdEntidad < 0 || Mapa.MapId < 0)
                return false;

            return geOS_Gestor.RotarEntidad(this._Conexion.ConnectionId, Mapa.MapId, this._nIdEntidad, ref fAngle);
        }
    }
}