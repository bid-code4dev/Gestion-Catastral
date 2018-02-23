using System;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Text;
using System.Drawing;

namespace geoEngineOS.Locations
{
    using geoEngineOS.Map;
    using geoEngineOS.Entity;

    /// <summary>
    /// CLASE QUE GESTIONA LAS LOCALIZACIONES: 
    /// ENTIDADES DIGITALIZADAS POR EL USUARIO O CREADAS EN BASE A ENTIDADES DE UN TEMA USADAS PARA REALIZAR
    /// AN�LISIS ESPACIALES. LAS LOCALIZACIONES EST�N ASOCIADAS A UNA VENTANA DE MAPA...
    /// </summary>
    public class geOS_Location
    {
        private geOS_MapWindow _Mapa;
        private Int32 _nIdLoc;

        /// <summary>
        /// IDENTIFICADOR DE LA LOCALIZACI�N
        /// </summary>
        public Int32 Id
        {
            get { return _nIdLoc; }
        }

        /// <summary>
        /// VENTANA DE MAPA A LA QUE EST� ASOCIADA LA LOCALIZACION
        /// </summary>
        public geOS_MapWindow Map
        {
            get { return _Mapa; }
        }

        public geOS_Location()
        {
            this._nIdLoc = -1;
            this._Mapa = null;
        }
        public geOS_Location(geOS_MapWindow Mapa, Int32 IdLoc)
        {
            this._Mapa = Mapa;
            this._nIdLoc = IdLoc;
        }
        public geOS_Location(geOS_Location Loc)
        {
            this._nIdLoc    = Loc.Id;
            this._Mapa      = Loc.Map;
        }

        ~geOS_Location()
        {
            Free();
        }

        /// <summary>
        /// LIBERA UNA LOCALIZACION
        /// </summary>
        public void Free()
        {
            if (this._Mapa != null && this._nIdLoc >= 0)
                geOS_Gestor.LiberaLocalizacion(this._Mapa.Connection.ConnectionId, this._Mapa.MapId, this._nIdLoc);

            this._nIdLoc = -1;
            this._Mapa = null;
        }

        /// <summary>
        /// HACE UN FLASH DE LA LOCALIZACI�N
        /// </summary>
        /// <param name="nTimes">NUMERO DE VECES QUE SE DESEA HACER EL FLASH</param>
        public void Flash(short nTimes)
        {
            geOS_Gestor.FlashLocalizacion(this.Map.Connection.ConnectionId, this.Map.MapId, this._nIdLoc, nTimes);
        }

        /// <summary>
        /// REALIZA UN ZOOM A LA LOCALIZACI�N
        /// </summary>
        /// <returns></returns>
        public bool Zoom()
        {
            return geOS_Gestor.ZoomLocalizacion(this.Map.Connection.ConnectionId, this.Map.MapId, this._nIdLoc) == 0 ? false : true;
        }

        /// <summary>
        /// CREA UNA NUEVA LOCALIZACI�N COMO UN BUFFER ALREDEDOR DE LA LOALIZACI�N DADA
        /// </summary>
        /// <param name="Distance">TAMA�O DEL BUFFER EN METROS</param>
        /// <param name="LocColor">COLOR DE FONDO DEL BUFFER</param>
        /// <param name="LocStyle">ESTILO DEL BUFFER (SOLIDO, TRANSPARENTE, ETC)</param>
        /// <param name="Size">ANCHO DEL BORDE DEL BUFFER</param>
        /// <returns></returns>
        public geOS_Location CreateBuffer(double Distance,Color LocColor, byte LocStyle, double Size)
        {
            int IdLoc = geOS_Gestor.CreaBufferPorLocalizacion(this.Map.Connection.ConnectionId, this.Map.MapId, this._nIdLoc, Distance, LocColor.ToArgb(), LocStyle, Size);
            return IdLoc < 0 ? null : new geOS_Location(this.Map,IdLoc);
        }

        /// <summary>
        /// A�ADE UNA COORDENADA A UNA LOCALIZACI�N CREADA MEDIANTE EL M�TODO 
        /// CreategeOS_Location DEL LA CLASE geOS_MapWindow
        /// </summary>
        /// <param name="X">COORDENADA X</param>
        /// <param name="Y">COORDENADA Y</param>
        public void AddCoordinate(double X,double Y)
        {
            geOS_Gestor.AnadeCoordenadaLocalizacion(this.Map.Connection.ConnectionId, this.Map.MapId, this._nIdLoc, X, Y);
        }

        /// <summary>
        /// A�ADE UNA COORDENADA A UNA LOCALIZACI�N CREADA MEDIANTE EL M�TODO 
        /// CreategeOS_Location DEL LA CLASE geOS_MapWindow
        /// </summary>
        /// <param name="X">COORDENADA X</param>
        /// <param name="Y">COORDENADA Y</param>
        /// <param name="bAddPart">SI SE DESEA A�ADIR UNA PARTE A LA LOCALIZACION</param>
        public void AddCoordinateX(double X, double Y, bool bAddPart)
        {
            geOS_Gestor.AnadeCoordenadaLocalizacionX(this.Map.Connection.ConnectionId, this.Map.MapId, this._nIdLoc, X, Y, bAddPart);
        }

        /// <summary>
        /// A�ADE UNA COORDENADA A UNA LOCALIZACI�N CREADA MEDIANTE EL M�TODO 
        /// CreategeOS_Location DEL LA CLASE geOS_MapWindow
        /// </summary>
        /// <param name="X">COORDENADA X</param>
        /// <param name="Y">COORDENADA Y</param>
        /// <param name="bAddPart">SI SE DESEA A�ADIR UNA PARTE A LA LOCALIZACION</param>
        /// <param name="bRefreshTLayer">SI SE DESEA HACER UN REFRESH DEL TRACKING LAYER</param>
        public void AddCoordinate2(double X, double Y, bool bAddPart, bool bRefreshTLayer)
        {
            geOS_Gestor.AnadeCoordenadaLocalizacion2(this.Map.Connection.ConnectionId, this.Map.MapId, this._nIdLoc, X, Y, bAddPart, bRefreshTLayer);
        }

        /// <summary>
        /// DEVUELVE EL TIPO DE LOCALIZACI�N
        /// </summary>
        /// <returns></returns>
        public geOS_EntityType GetLocType()
        {
            return (geOS_EntityType)(geOS_Gestor.GetTipoLocalizacion(this.Map.Connection.ConnectionId, this.Map.MapId, this._nIdLoc));
        }

        /// <summary>
        /// DEVUELVE LOS PAR�METROS DE DISE�O DE LA LOCALIZACI�N
        /// </summary>
        /// <param name="LocColor">COLOR DE LA LOCALIZACI�N</param>
        /// <param name="Style">ESTILO DE LA LOCALIZACI�N</param>
        /// <param name="Size">TAMA�O EN METROS SI ES PUNTUAL O 
        /// TAMA�O DE LA LINEA O POLIGONO EN PIXELES SI ES LINEAL O SUPERFICIAL</param>
        /// <returns></returns>
        public bool GetParams(ref Color LocColor, ref byte Style, ref double Size)
        {
            int nColorAux = 0;

            if( geOS_Gestor.GetParamsLocalizacion(this.Map.Connection.ConnectionId, this.Map.MapId, this._nIdLoc, ref nColorAux, ref Style, ref Size) == 0 )
                return false;

            LocColor = Color.FromArgb(nColorAux);
            return true;
        }

        /// <summary>
        /// DEVUELVE EL N�MERO DE COORDENADAS DE UNA LOCALIZACI�N
        /// </summary>
        /// <param name="NumCoords">N�MERO DE COORDENADAS</param>
        /// <returns></returns>
        public bool GetNumCoords(ref short NumCoords)
        {
            return geOS_Gestor.GetNumCoordenadasLocalizacion(this.Map.Connection.ConnectionId, this.Map.MapId, this._nIdLoc, ref NumCoords) == 0 ? false : true;
        }

        /// <summary>
        /// DEVUELVE LOS VALORES DE UNA COORDENADA DE LA LOCALIZACI�N
        /// </summary>
        /// <param name="Index">INDICE DE LA COORDENADA (BASE 0)</param>
        /// <param name="X">COORDENADA X</param>
        /// <param name="Y">COORDENADA Y</param>
        /// <param name="Part">NUMERO DE PARTE</param>
        /// <returns></returns>
        public bool GetCoordinate(int Index, ref double X, ref double Y, ref short Part)
        {
            return geOS_Gestor.GetCoordenadaLocalizacion(this.Map.Connection.ConnectionId, this.Map.MapId, this._nIdLoc, Index, ref X, ref Y, ref Part) == 0 ? false : true;
        }

        /// <summary>
        /// DEVUELVE LA EXTENSI�N DE UNA LOCALIZACI�N
        /// </summary>
        /// <param name="MinX">COORDENADA X MINIMA DE LA LOCALIZACI�N</param>
        /// <param name="MinY">COORDENADA Y MINIMA DE LA LOCALIZACI�N</param>
        /// <param name="MaxX">COORDENADA X MAXIMA DE LA LOCALIZACI�N</param>
        /// <param name="MaxY">COORDENADA Y MAXIMA DE LA LOCALIZACI�N</param>
        /// <returns></returns>
        public bool GetExtent(ref double MinX, ref double MinY, ref double MaxX, ref double MaxY)
        {
            return geOS_Gestor.GetExtentLocalizacion(this.Map.Connection.ConnectionId, this.Map.MapId, this._nIdLoc, ref MinX, ref MinY, ref MaxX, ref MaxY);
        }

        /// <summary>
        /// DEVUELVE LA DISTANCIA DE LA LOCALIZACI�N A UN TEMA DE LA VISTA
        /// </summary>
        /// <param name="Theme">C�DIGO DEL TEMA EN LA VISTA</param>
        /// <returns></returns>
        public double DistanceToTheme(string Theme)
        {
            return geOS_Gestor.DistanciaLocalizacionTema(this.Map.Connection.ConnectionId, this.Map.MapId, this._nIdLoc, Theme);
        }

        /// <summary>
        /// DEVUELVE LA N-ENTIDADES MAS CERCANAS DE UN TEMA A LA LOCALIZACI�N
        /// </summary>
        /// <param name="Theme">C�DIGO DEL TEMA DONDE SE BUSCAN LAS ENTIDADES</param>
        /// <param name="Filter">FILTRO ALFANUM�RICO SOBRE EL TEMA (PUEDE SER VACIO)</param>
        /// <param name="MaxEntities">N�MERO DE ENTIDADES M�S CERCANAS A BUSCAR</param>
        /// <param name="Entities">ARRAY CON LAS ENTIDADES M�S CERCANAS ENCONTRADAS</param>
        /// <param name="Distances">ARRAY CON LAS DISTANCIAS A CADA ENTIDAD</param>
        /// <returns></returns>
        public bool GetClosestEntities(string Theme,string Filter,short MaxEntities,ref geOS_MapEntity[] Entities,ref double[] Distances)
        {
            byte[] bIdEntidades = new byte[MaxEntities * sizeof(Int32)];
            byte[] bDistances = new byte[MaxEntities * sizeof(double)];

            if (geOS_Gestor.GetEntidadesMasCercanas(this.Map.Connection.ConnectionId, this.Map.MapId, this._nIdLoc,
                                MaxEntities, Theme, Filter, bIdEntidades, bDistances) == 0)
                return false;

            int[] IdEntidades = new int[MaxEntities];

            for (int i = 0; i < MaxEntities; i++)
            {
                IdEntidades[i] = BitConverter.ToInt32(bIdEntidades, i * 4);
                Distances[i] = BitConverter.ToDouble(bDistances, i * 8);
            }

            for (int i = 0; i < MaxEntities; i++)
                Entities[i] = new geOS_MapEntity(this.Map.Connection, IdEntidades[i]);

            return true;
        }

        /// <summary>
        /// BUSCA ENTIDADES SOBRE UN TEMA SEG�N UN FILTRO ESPACIAL Y ALFANUM�RICO
        /// </summary>
        /// <param name="SpatialOp">OPERADOR ESPACIAL</param>
        /// <param name="Theme">TEMA DE BUSQUEDA</param>
        /// <param name="Filter">FILTRO ALFANUM�RICO SOBRE EL TEMA</param>
        /// <returns></returns>
        public geOS_MapEntities SearchEntities(geOS_SpatialOperator SpatialOp,string Theme,string Filter)
        {
            int IdEntidades = geOS_Gestor.BuscaEntidadesLocalizacion(this.Map.Connection.ConnectionId, this.Map.MapId, this._nIdLoc, 
                                                                  (short)SpatialOp,Theme, Filter);
            return IdEntidades < 0 ? null : new geOS_MapEntities(this.Map.Connection,IdEntidades);
        }

        /// <summary>
        /// DEVUELVE LAS ENTIDADES DE UN TEMA QUE EST�N A UNA DISTANCIA DADA DE LA LOCALIZACI�N
        /// </summary>
        /// <param name="Distance">DISTANCIA</param>
        /// <param name="Theme">TEMA DE B�SQUEDA</param>
        /// <param name="Filter">FILTRO ALFANUM�RICO OPCIONAL</param>
        /// <returns></returns>
        public geOS_MapEntities SearchByDistance(double Distance,string Theme,string Filter)
        {
            int IdEntidades = geOS_Gestor.BuscaEntidadesDistancia(this.Map.Connection.ConnectionId, this.Map.MapId, this._nIdLoc, 
                                                               Distance,Theme, Filter);
            return IdEntidades < 0 ? null : new geOS_MapEntities(this.Map.Connection,IdEntidades);
        }

        /// <summary>
        /// DEVUELVE LA DISTANCIA DE ESTA LOCALIZACI�N A UNA ENTIDAD
        /// </summary>
        /// <param name="Entity">ENTIDAD</param>
        /// <returns></returns>
        public double DistanceToEntity(geOS_MapEntity Entity)
        {
            return geOS_Gestor.DistanciaLocalizacionEntidad(this.Map.Connection.ConnectionId, this.Map.MapId, this._nIdLoc, Entity.Id);
        }

        /// <summary>
        /// DEVUELVE LA DISTANCIA DE ESTA LOCALIZACI�N A OTRA LOCALIZACI�N
        /// </summary>
        /// <param name="Loc">LOCALIZACI�N</param>
        /// <returns></returns>
        public double DistanceToLocation(geOS_Location Loc)
        {
            return geOS_Gestor.DistanciaLocalizacionLocalizacion(this.Map.Connection.ConnectionId, this.Map.MapId, this._nIdLoc, Loc.Id);
        }

        /// <summary>
        /// CAMBIA LA SIMBOLOG�A DE UNA LOCALIZACI�N
        /// </summary>
        /// <param name="LocColor">NUEVO COLOR</param>
        /// <param name="LocStyle">NUEVO ESTILO</param>
        /// <param name="fSize">NUEVO TAMA�O</param>
        /// <param name="sFont">NUEVA FUENTE DE LETRA (TIPO: moPoint Y ESTILO: moTrueTypeMarker</param>
        /// <param name="Character">CAR�CTER (TIPO: moPoint Y ESTILO: moTrueTypeMarker</param>
        /// <param name="bOutline">SI SE PINTA EL BORDE</param>
        /// <param name="nColorOutline">COLOR DEL BORDE</param>
        /// <param name="fAngRotation">ANGULO DE ROTACI�N (TIPO: moPoint Y ESTILO: moTrueTypeMarker</param>
        /// <returns></returns>
        public bool ChangeSimbology(Color LocColor, byte LocStyle, double fSize, string sFont, short Character, bool bOutline, int nColorOutline, double fAngRotation)
        {
            return geOS_Gestor.CambiarSimbologiaLocalizacion(this.Map.Connection.ConnectionId, this.Map.MapId, this._nIdLoc, 
                LocColor.ToArgb(), LocStyle, fSize, sFont, Character, bOutline, nColorOutline, fAngRotation) == 1 ? true : false;
        }

        public geOS_MapEntity CreateEntity(string sGeocode, bool bIncTransaction)
        {
            int IdEntity = geOS_Gestor.CrearEntidadPorLocalizacion(this.Map.Connection.ConnectionId, this.Map.MapId, this._nIdLoc, sGeocode, bIncTransaction);
            return IdEntity < 0 ? null : new geOS_MapEntity(this.Map.Connection, IdEntity);
        }

    }
}