using System;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Text;
using System.Drawing;

namespace geoEngineOS.Map
{
    using geoEngineOS.Connection;
    using geoEngineOS.Entity;
    using geoEngineOS.Locations;

    /// <summary>
    /// CLASE QUE GESTIONA LA VENTANA DE MAPA 
    /// SE USA COMO CLASE DE DISE�O VISUAL UN PictureBox
    /// </summary>
    public class geOS_MapWindow : System.Windows.Forms.PictureBox
    {
        protected geOS_EtgConnection _Conexion;
        protected Int32 _nIdMapa;

        /// <summary>
        /// DEVUELVE LA CONEXI�N ASOCIADA AL MAPA
        /// </summary>
        public geOS_EtgConnection Connection
        {
            get { return this._Conexion; }
        }

        /// <summary>
        /// DEVUELVE EL IDENTIFICADOR DE MAPA
        /// </summary>
        public Int32 MapId
        {
            get { return _nIdMapa; }
        }

        public geOS_MapWindow()
        {
            this._nIdMapa = -1;
            this._Conexion = null;
        }

        ~geOS_MapWindow()
        {
            Detach();
        }

        protected override void OnHandleDestroyed(EventArgs e)
        {
            this.Detach();
            base.OnHandleDestroyed(e);
        }

        // HANDLERS DE MENSAJES ENVIADOS POR EL MAPA...
        public delegate void onBeforeMapDrawHandler();
        public event onBeforeMapDrawHandler OnBeforeMapDraw;

        public delegate void onAfterMapDrawHandler();
        public event onAfterMapDrawHandler OnAfterMapDraw;

        public delegate void onCancelledMapDrawHandler();
        public event onCancelledMapDrawHandler OnCancelledMapDraw;

        public delegate void onChangeViewHandler(string sNewView);
        public event onChangeViewHandler OnChangeView;

        public delegate void onEntityInfoHandler(geOS_MapEntity Entity);
        public event onEntityInfoHandler OnEntityInfo;

        public delegate void onMapClick(int MouseX, int MouseY);
        public event onMapClick OnMapClick;

        public delegate void onEditionInit();
        public event onEditionInit OnEditionInit;

        public delegate void onEditionFinished();
        public event onEditionFinished OnEditionFinished;

        public delegate void onEditionSaved();
        public event onEditionSaved OnEditionSaved;

        public delegate void onEditionUNDO(int TransactionID);
        public event onEditionUNDO OnEditionUNDO;

        public delegate bool onGetGeocode(geOS_MapEntity Entity,ref string NewGeocode);
        public event onGetGeocode OnGetGeocode;

        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);

            // LOS MENSAJES ENVIADOS POR EL MOTOR EMPIEZAN EN 0x4200
            if (m.Msg >= 0x4200)
            {
                geOS_Message msg = (geOS_Message)m.Msg;

                if (msg == geOS_Message.msgInfoEntity)
                {
                    geOS_MapEntity Entidad = new geOS_MapEntity(this._Conexion, (int)m.LParam);
                    OnEntityInfo(Entidad);
                }
                else if (msg == geOS_Message.msgBeforeMapDraw && this.OnBeforeMapDraw != null)
                    OnBeforeMapDraw();
                else if (msg == geOS_Message.msgAfterMapDraw && this.OnAfterMapDraw != null)
                    OnAfterMapDraw();
                else if (msg == geOS_Message.msgCanceledMapDraw && this.OnCancelledMapDraw != null)
                    OnCancelledMapDraw();
                else if (msg == geOS_Message.msgMapClick && this.OnMapClick != null)
                    OnMapClick((int)m.WParam,(int)m.LParam);
                else if (msg == geOS_Message.msgChangeView && this.OnChangeView != null)
                    OnChangeView(Marshal.PtrToStringAnsi(m.LParam));
                else if (msg == geOS_Message.msgEditionInit && this.OnEditionInit != null)
                    OnEditionInit();
                else if (msg == geOS_Message.msgEditionFinished && this.OnEditionFinished != null)
                    OnEditionFinished();
                else if (msg == geOS_Message.msgEditionSaved && this.OnEditionSaved != null)
                    OnEditionSaved();
                else if (msg == geOS_Message.msgUndo && this.OnEditionUNDO != null)
                    OnEditionUNDO((int)m.WParam);
                else if (msg == geOS_Message.msgGetGeocode && this.OnGetGeocode != null)
                {
                    geOS_MapEntity entity = new geOS_MapEntity(this.Connection, (int)m.WParam);
                    string sNewGeocode = Marshal.PtrToStringAnsi(m.LParam);

                    bool bRetorno = OnGetGeocode(entity,ref sNewGeocode);

                    // Resultado del mensaje...
                    m.Result = new IntPtr(bRetorno ? 1 : 0);

                    if (bRetorno && sNewGeocode != Marshal.PtrToStringAnsi(m.LParam))
                    {
                        System.IO.MemoryStream memStream = new System.IO.MemoryStream(sNewGeocode.Length + 1);

                        memStream.Write(Encoding.ASCII.GetBytes(sNewGeocode), 0, sNewGeocode.Length);
                        memStream.WriteByte((byte)0);

                        Marshal.Copy(memStream.ToArray(), 0, m.LParam, sNewGeocode.Length + 1);
                    }
                }
            }
        }

        /// <summary>
        /// CARGA UN MAPA EN LA VENTANA
        /// </summary>
        /// <param name="Connection">HANDLE DE LA CONEXION AL ETG</param>
        /// <param name="bUserEnv">SI SE CARGAN O NO LOS PAR�METROS DE ENTORNO DEL USUARIO</param>
        /// <returns></returns>
        public bool Attach(geOS_EtgConnection Connection, bool bUserEnv)
        {
            Int16 nEntorno = bUserEnv ? (Int16)1 : (Int16)0;

            this._Conexion = Connection;

            this._nIdMapa = geOS_Gestor.AttachWindow(this._Conexion.ConnectionId, this.Handle.ToInt32(), nEntorno);
            return this._nIdMapa >= 0;
        }

        /// <summary>
        /// CARGA UN MAPA EN LA VENTANA. 
        /// SE CARGAN LAS CAPAS VECTORIALES Y RASTER DE LA VIEW QUE SE PASA COMO PARAMETRO NO LA VIEW DE DEFECTO DEL USUARIO
        /// </summary>
        /// <param name="Connection">HANDLE DE LA CONEXION AL ETG</param>
        /// <param name="bUserEnv">SI SE CARGAN O NO LOS PAR�METROS DE ENTORNO DEL USUARIO</param>
        /// <param name="sView">CODIGO DE LA VIEW (CAPAS VECTORIALES Y RASTER) A MOSTRAR</param>
        /// <returns></returns>
        public bool AttachView(geOS_EtgConnection Connection, bool bUserEnv, string sView)
        {
            Int16 nEntorno = bUserEnv ? (Int16)1 : (Int16)0;

            this._Conexion = Connection;

            this._nIdMapa = geOS_Gestor.AttachWindowVista(this._Conexion.ConnectionId, this.Handle.ToInt32(), nEntorno, sView);
            return this._nIdMapa >= 0;
        }

        /// <summary>
        /// DESCARGA EL MAPA DE LA VENTANA
        /// </summary>
        public void Detach()
        {
            if (this._Conexion != null && this._nIdMapa >= 0)
                geOS_Gestor.DetachWindow(this._Conexion.ConnectionId, this._nIdMapa);

            this._nIdMapa = -1;
        }

        /// <summary>
        /// MUESTRA U OCULTA UNA TOOLBAR DEL MAPA
        /// </summary>
        /// <param name="toolbar">IDENTIFICADOR DE LA TOOLBAR A MOSTRAR U OCULTA</param>
        /// <param name="bShow">TRUE: MOSTRAR
        ///                     FALSE: OCULTAR
        /// </param>
        public void ShowToolbar(geOS_MapToolbar toolbar, bool bShow)
        {
            geOS_Gestor.MuestraToolbar(_Conexion.ConnectionId, _nIdMapa, (Int16)toolbar, bShow ? (Int16)1 : (Int16)0);
        }

        /// <summary>
        /// RESALTA EN EL COLOR DE SELECCI�N LAS ENTIDADES DEL TEMA QUE SE PASA COMO PARAMETRO QUE NO TENGAN GEOC�DIGO ASOCIADO
        /// </summary>
        /// <param name="sTheme">C�DIGO DEL TEMA</param>
        /// <param name="bShow">
        /// TRUE: MOSTRAR ENTIDADES SIN INFO
        /// FALSE: OCULTAR ENTIDADES SIN INFO
        /// </param>
        /// <returns></returns>
        public bool ShowEntitiesWithoutGeocode(string sTheme, bool bShow)
        {
            return geOS_Gestor.MuestraEntidadesSinInfo(_Conexion.ConnectionId, _nIdMapa, sTheme, bShow);
        }

        /// <summary>
        /// MUESTRA/OCULTA LA VENTANA DE INFO GENERAL DE ENTIDAD
        /// </summary>
        /// <param name="bShow"></param>
        public void ShowEntityInfoWindow(bool bShow)
        {
            geOS_Gestor.SetVentanaInfoEntidad(_Conexion.ConnectionId, _nIdMapa, bShow ? (Int16)1 : (Int16)0);
        }

        /// <summary>
        /// A�ADE DIN�MICAMENTE UN TEMA A LA VENTANA DE MAPA
        /// </summary>
        /// <param name="Theme">C�DIGO DEL TEMA A A�ADIR</param>
        /// <param name="MinScale">ESCALA MINIMA DE VISUALIZACI�N DEL TEMA</param>
        /// <param name="MaxScale">ESCALA M�XIMA DE VISUALIZACI�N DEL TEMA</param>
        /// <returns></returns>
        public bool AddTheme(string Theme, int MinScale, int MaxScale)
        {
            return geOS_Gestor.AnadeTemaMapa(_Conexion.ConnectionId, _nIdMapa, Theme, MinScale, MaxScale);
        }

        /// <summary>
        /// PERMITE QUITAR UN TEMA DE LA VISTA DE MAPA
        /// </summary>
        /// <param name="Theme">C�DIGO DEL TEMA QUE SE DESEA ELIMINAR</param>
        /// <returns></returns>
        public bool RemoveTheme(string Theme)
        {
            return geOS_Gestor.EliminaTemaMapa(_Conexion.ConnectionId, _nIdMapa, Theme);
        }

        /// <summary>
        /// PERMITE QUE EL USUARIO A�ADA DIN�MICAMENTE NUEVOS TEMAS A LA VISTA DE MAPA
        /// </summary>
        /// <param name="AllowSimples">SI SE PERMITE A�ADIR TEMAS SIMPLES</param>
        /// <param name="AllowThemathics">SI SE PERMITE A�ADIR TEM�TICOS</param>
        /// <param name="AllowQueries">SI SE PERMITE A�ADIR QUERYS</param>
        /// <returns></returns>
        public bool AddThemes(bool AllowSimples, bool AllowThemathics, bool AllowQueries)
        {
            return geOS_Gestor.AnadeTemasVista(_Conexion.ConnectionId, _nIdMapa,AllowSimples,AllowThemathics,AllowQueries);
        }

        /// <summary>
        /// PERMITE FILTRAR DIN�MICAMENTE LAS ENTIDADES DE UN TEMA
        /// </summary>
        /// <param name="Theme">CODIGO DEL TEMA A FILTRAR</param>
        /// <param name="Filter">FILTRO ALFANUM�RICO A APLICAR</param>
        /// <returns></returns>
        public bool FilterTheme(string Theme, string Filter)
        {
            return geOS_Gestor.SetFiltroTema(_Conexion.ConnectionId, _nIdMapa, Theme, Filter);
        }

        /// <summary>
        /// ACTIVA LA HERRAMIENTA DE ZOOM
        /// </summary>
        public void Zoom()
        {
            geOS_Gestor.ZoomMapa(_Conexion.ConnectionId, _nIdMapa);
        }

        /// <summary>
        /// REALIZA UN ZOOM IN (ESCALA / 2) CENTRADA EN EL PUNTO CENTRAL ACTUAL DEL MAPA
        /// </summary>
        public void ZoomIn()
        {
            geOS_Gestor.ZoomInMapa(_Conexion.ConnectionId, _nIdMapa);
        }

        /// <summary>
        /// REALIZA UN ZOOM OUT (ESCALA * 2) CENTRADA EN EL PUNTO CENTRAL ACTUAL DEL MAPA
        /// </summary>
        public void ZoomOut()
        {
            geOS_Gestor.ZoomOutMapa(_Conexion.ConnectionId, _nIdMapa);
        }

        /// <summary>
        /// ACTIVA EL ZOOM ANTERIOR EN EL MAPA
        /// </summary>
        public void ZoomPrevious()
        {
            geOS_Gestor.ZoomAnteriorMapa(_Conexion.ConnectionId, _nIdMapa);
        }

        /// <summary>
        /// REALIZA UN ZOOM A LA EXTENSI�N TOTAL DEL MAPA
        /// </summary>
        public void ZoomExtent()
        {
            geOS_Gestor.ZoomExtentMapa(_Conexion.ConnectionId, _nIdMapa);
        }

        /// <summary>
        /// REALIZA UN REDIBUJADO DE LA VENTANA DE MAPA
        /// </summary>
        public void Redraw()
        {
            geOS_Gestor.RedrawMapWindow(_Conexion.ConnectionId, _nIdMapa);
        }

        /// <summary>
        /// ACTIVA LA HERRAMIENTA DE PANNING (MOVERSE POR EL MAPA)
        /// </summary>
        public void Panning()
        {
            geOS_Gestor.HacerPanning(_Conexion.ConnectionId, _nIdMapa);
        }

        /// <summary>
        /// ACTIVA LA HERRAMIENTA DE HACER PANNING POR UN PUNTO:
        /// EL USUARIO PINCHA UN PUNTO Y EL MAPA SE CENTRA EN ESE PUNTO
        /// </summary>
        public void PanningToPoint()
        {
            geOS_Gestor.HacerPanningPunto(_Conexion.ConnectionId, _nIdMapa);
        }

        /// <summary>
        /// PERMITE ROTAR UN MAPA
        /// </summary>
        /// <param name="Angle">ANGULO DE ROTACI�N EN GRADOS</param>
        /// <returns></returns>
        public bool Rotate(double Angle)
        {
            return geOS_Gestor.SetRotacionMapa(_Conexion.ConnectionId, _nIdMapa, Angle);
        }

        /// <summary>
        /// DEVUELVE EL ANGULO DE ROTACI�N DEL MAPA
        /// </summary>
        /// <param name="Angle">ANGULO EN GRADOS</param>
        /// <returns></returns>
        public bool GetRotation(ref double Angle)
        {
            return geOS_Gestor.GetRotacionMapa(_Conexion.ConnectionId, _nIdMapa, ref Angle);
        }

        /// <summary>
        /// DEVUELVE LA POSICI�N ACTUAL EN COORDENADAS DE MAPA DEL CURSOR
        /// </summary>
        /// <param name="X">CORRDENADA X</param>
        /// <param name="Y">COORDENADA Y</param>
        /// <returns></returns>
        public bool GetCursorPos(ref double X, ref double Y)
        {
            return geOS_Gestor.GetPosicionCursorMapa(_Conexion.ConnectionId, _nIdMapa, ref X, ref Y);
        }

        /// <summary>
        /// DEVUELVE LAS COORDENADAS QUE DETERMINAN LA EXTENSI�N ACTUAL DEL MAPA
        /// </summary>
        /// <param name="MinX">COORDENADA X MINIMA</param>
        /// <param name="MinY">COORDENADA Y MINIMA</param>
        /// <param name="MaxX">COORDENADA X MAXIMA</param>
        /// <param name="MaxY">COORDENADA Y MAXIMA</param>
        public void GetExtent(ref double MinX, ref double MinY, ref double MaxX, ref double MaxY)
        {
            geOS_Gestor.GetExtentMapa(_Conexion.ConnectionId, _nIdMapa,ref MinX,ref MinY,ref MaxX,ref MaxY);
        }


        /// <summary>
        /// DEVUELVE LAS COORDENADAS DE LA EXTENSI�N TOTAL DEL MAPA
        /// </summary>
        /// <param name="MinX">COORDENADA X MINIMA</param>
        /// <param name="MinY">COORDENADA Y MINIMA</param>
        /// <param name="MaxX">COORDENADA X MAXIMA</param>
        /// <param name="MaxY">COORDENADA Y MAXIMA</param>
        public void GetFullExtent(ref double MinX, ref double MinY, ref double MaxX, ref double MaxY)
        {
            geOS_Gestor.GetFullExtentMapa(_Conexion.ConnectionId, _nIdMapa, ref MinX, ref MinY, ref MaxX, ref MaxY);
        }

        public bool ProjectUTM(double X, double Y, int GeoSystem, int Projection, ref double XP, ref double YP)
        {
            return geOS_Gestor.ProyectarUtms2(_Conexion.ConnectionId, _nIdMapa, X, Y, GeoSystem, Projection, ref XP, ref YP);
        }

        /// <summary>
        /// REALIZA UN ZOOM A LAS COORDENADAS QUE SE PASAN COMO PAR�METROS
        /// </summary>
        /// <param name="Left">IZQUIERDA (X)</param>
        /// <param name="Bottom">ABAJO (Y)</param>
        /// <param name="Right">DERECHA (X)</param>
        /// <param name="Top">ARRIBA (Y)</param>
        public void ZoomByCoordinates(double Left, double Bottom, double Right, double Top)
        {
            geOS_Gestor.ZoomCoordenadas(_Conexion.ConnectionId, _nIdMapa, Left, Bottom, Right, Top);
        }

        /// <summary>
        /// REALIZA UN ZOOM CENTRADO EN EL PUNTO (X,Y) Y CON ESCALA nSCALE
        /// </summary>
        /// <param name="nScale">ESCALA DEL ZOOM</param>
        /// <param name="X">X DEL PUNTO CENTRAL</param>
        /// <param name="Y">Y DEL PUNTO CENTRAL</param>
        public void ZoomByScale(int nScale, double X, double Y)
        {
            geOS_Gestor.ZoomPorEscala(_Conexion.ConnectionId, _nIdMapa, nScale, X, Y);
        }

        /// <summary>
        /// DEVUELVE EL N�MERO DE TEMAS VECTORIALES CARGADOS EN LA VENTANA DE MAPA
        /// </summary>
        /// <returns></returns>
        public int GetNumThemes()
        {
            return geOS_Gestor.GetNumTemasMapa(_Conexion.ConnectionId, _nIdMapa);
        }

        /// <summary>
        /// DEVUELVE LOS PAR�METROS DE UN TEMA DE LA VISTA
        /// </summary>
        /// <param name="Index">INDICE (BASE 0) DEL TEMA EN LA VISTA</param>
        /// <param name="Theme">C�DIGO DEL TEMA</param>
        /// <param name="Description">DESCRIPCI�N DEL TEMA</param>
        /// <returns></returns>
        public bool GetThemeParams(short Index,ref string Theme,ref string Description)
        {
            StringBuilder sAux1 = new StringBuilder(256);
            StringBuilder sAux2 = new StringBuilder(256);

            if (geOS_Gestor.GetTemaMapa(_Conexion.ConnectionId, _nIdMapa, Index, sAux1, sAux2) == 0)
                return false;

            Theme = sAux1.ToString();
            Description = sAux2.ToString();
            return true;
        }

        /// <summary>
        /// DEVUELVE EL TEMA ACTIVO ACTUAL EN LA VISTA DE MAPA
        /// </summary>
        /// <returns>C�DIGO DEL TEMA</returns>
        public string GetSelectionTheme()
        {
            StringBuilder sAux = new StringBuilder(256);

            if (geOS_Gestor.GetTemaSeleccion(_Conexion.ConnectionId, _nIdMapa, sAux) == 0)
                return "";

            return sAux.ToString();
        }

        /// <summary>
        /// ACTIVA UN TEMA COMO TEMA DE SELECCI�N
        /// </summary>
        /// <param name="Theme">C�DIGO DEL TEMA QUE SE DESEA ACTIVAR</param>
        /// <returns></returns>
        public bool SetSelectionTheme(string Theme)
        {
            return geOS_Gestor.ActivaTemaSeleccion(_Conexion.ConnectionId, _nIdMapa, Theme) == 0 ? false : true;
        }

        /// <summary>
        /// MUESTRA EL DI�LOGO DE SELECCI�N DE TEMA ACTIVO
        /// </summary>
        /// <param name="Theme">DEVUELVE EL TEMA SELECCIONADO POR EL USUARIO</param>
        /// <returns></returns>
        public bool ChooseSelectionTheme(ref string Theme)
        {
            StringBuilder Aux = new StringBuilder(256);

            if (geOS_Gestor.ElegirTemaSeleccion(_Conexion.ConnectionId, _nIdMapa, Aux) == 0)
                return false;

            Theme = Aux.ToString();
            return true;
        }

        /// <summary>
        /// DEVUELVE EL TEMA QUE ESTA SIENDO USADO ACTUALMENTE PARA MOSTRAR LAS ENTIDADES SIN GEOCODIGO
        /// </summary>
        /// <returns>RETORNO EL C�DIGO DEL TEMA</returns>
        public string GetThemeEntitiesWithoutGeocode()
        {
            StringBuilder sAux = new StringBuilder(256);

            if (!geOS_Gestor.GetTemaEntidadesSinInfo(_Conexion.ConnectionId, _nIdMapa, sAux))
                return "";

            return sAux.ToString();
        }

        /// <summary>
        /// DEVUELVE LAS COORDENADAS QUE DETERMINAN LA EXTENSI�N DEL TEMA QUE SE PASA COMO PAR�METRO
        /// </summary>
        /// <param name="Theme">C�DIGO DEL TEMA</param>
        /// <param name="MinX">COORDENADA X MINIMA</param>
        /// <param name="MinY">COORDENADA Y MINIMA</param>
        /// <param name="MaxX">COORDENADA X MAXIMA</param>
        /// <param name="MaxY">COORDENADA Y MAXIMA</param>
        /// <returns></returns>
        public bool GetThemeExtent(string Theme, ref double MinX, ref double MinY, ref double MaxX, ref double MaxY)
        {
            return geOS_Gestor.GetExtentTema(_Conexion.ConnectionId, _nIdMapa, Theme, ref MinX, ref MinY, ref MaxX, ref MaxY);
        }

        /// <summary>
        /// DEVUELVE LAS ESTAD�STICAS DE UN TEMA DE LA VISTA
        /// </summary>
        /// <param name="Theme">TEMA DE LA VISTA</param>
        /// <param name="Filter">FILTRO ALFANUM�RICO SOBRE EL TEMA</param>
        /// <param name="Field">CAMPO SOBRE EL QUE SE DESEA OBTENER LAS ESTAD�STICAS (DEBE SER NUMERICO)</param>
        /// <param name="Count">NUMERO DE REGISTRO</param>
        /// <param name="Min">MINIMO VALOR DEL CAMPO</param>
        /// <param name="Max">MAXIMO VALOR DEL CAMPO</param>
        /// <param name="Mean">MEDIA DEL VALOR DEL CAMPO</param>
        /// <param name="Sum">SUMA DEL VALOR DEL CAMPO</param>
        /// <param name="DevStd">DESVIACI�N ESTANDAR DEL VALOR DEL CAMPO</param>
        /// <returns></returns>
        public bool GetThemeStats(string Theme,string Filter,string Field,
                                  ref int Count,ref int Min,ref int Max, ref int Mean, ref int Sum, ref int DevStd)
        {
            return geOS_Gestor.GetStatsTema(_Conexion.ConnectionId, _nIdMapa, Theme, Filter, Field,
                                         ref Count, ref Min, ref Max, ref Mean, ref Sum, ref DevStd);
        }

        /// <summary>
        /// SOLICITA AL USUARIO QUE SELECCIONE UNA ENTIDAD DEL TEMA QUE SE PASA COMO PAR�METRO
        /// </summary>
        /// <param name="sTheme">C�DIGO DEL TEMA EN EL QUE SE DESEA SELECCIONAR</param>
        /// <returns>ENTIDAD SELECCIONADO (NULL EN CASO DE ERROR O CANCELACI�N)</returns>
        public geOS_MapEntity SelecThemeEntity(string sTheme)
        {
            int nIdEntidad = geOS_Gestor.SelecEntidadTema(_Conexion.ConnectionId, _nIdMapa, sTheme);

            if (nIdEntidad < 0)
                return null;

            return new geOS_MapEntity(_Conexion,nIdEntidad);
        }

        /// <summary>
        /// DEVUELVE UNA ENTIDAD DE UN TEMA MEDIANTE EL GEOC�DIGO
        /// </summary>
        /// <param name="Theme">CODIGO DEL TEMA DE LA ENTIDAD A SELECCIONAR</param>
        /// <param name="Geocode">GEOC�DIGO DE LA ENTIDAD</param>
        /// <returns></returns>
        public geOS_MapEntity SelecEntityByGeocode(string Theme, string Geocode)
        {
            int IdEntidad = geOS_Gestor.SelecEntidadPorGeocodigo(_Conexion.ConnectionId, _nIdMapa, Theme, Geocode);
            return IdEntidad < 0 ? null : new geOS_MapEntity(this._Conexion, IdEntidad);
        }

        /// <summary>
        /// PERMITE SABER SI EXISTE UNA ENTIDAD EN UN TEMA DE LA VISTA SEG�N UN GEOCODIGO
        /// </summary>
        /// <param name="Theme">CODIGO DEL TEMA</param>
        /// <param name="Geocode">GEOCODIGO</param>
        /// <returns></returns>
        public bool ExistsEntityByGeocode(string Theme, string Geocode)
        {
            return geOS_Gestor.ExisteEntidadPorGeocodigo(_Conexion.ConnectionId, _nIdMapa, Theme, Geocode) == 0 ? false : true;
        }

        /// <summary>
        /// SOLICITA AL USUARIO LA SELECCI�N DE UN CONJUNTO DE ENTIDADES DEL TEMA ACTIVO DE LA VISTA
        /// BOT�N IZQUIERDO DEL RAT�N SELECCIONA/DESELECCIONA
        /// BOT�N DERECHO O TECLA [ENTER] TERMINA
        /// TECLA [ESC] CANCELA
        /// </summary>
        /// <returns>NULL SI HAY UN ERROR O EL USUARIO CANCELA</returns>
        public geOS_MapEntities SelecEntities()
        {
            int IdEntidades = geOS_Gestor.SelecEntidades(_Conexion.ConnectionId, _nIdMapa);
            return IdEntidades < 0 ? null : new geOS_MapEntities(_Conexion, IdEntidades);
        }

        /// <summary>
        /// SOLICITA AL USUARIO LA SELECCI�N DE UN CONJUNTO DE ENTIDADES DE UN TEMA
        /// BOT�N IZQUIERDO DEL RAT�N SELECCIONA/DESELECCIONA
        /// BOT�N DERECHO O TECLA [ENTER] TERMINA
        /// TECLA [ESC] CANCELA
        /// </summary>
        /// <param name="Theme">C�DIGO DEL TEMA EN EL QUE SE SELECCIONAN LAS ENTIDADES</param>
        /// <returns>NULL SI HAY UN ERROR O EL USUARIO CANCELA</returns>
        public geOS_MapEntities SelecThemeEntities(string Theme)
        {
            int IdEntidades = geOS_Gestor.SelecEntidadesTema(_Conexion.ConnectionId, _nIdMapa, Theme);
            return IdEntidades < 0 ? null : new geOS_MapEntities(_Conexion, IdEntidades);
        }

        /// <summary>
        /// SELECCIONA UN CONJUNTO DE ENTIDADES DE UN TEMA POR UN FILTRO ALFANUM�RICO
        /// </summary>
        /// <param name="Theme">C�DIGO DEL TEMA DONDE SE DESEAN SELECCIONAR LAS ENTIDADES</param>
        /// <param name="Filter">FILTRO ALFANUM�RICO DE SELECCI�N</param>
        /// <returns>NULL SI HAY ALG�N ERROR</returns>
        public geOS_MapEntities SelecEntitiesByFilter(string Theme, string Filter)
        {
            int IdEntidades = geOS_Gestor.BuscaEntidadesFiltro(_Conexion.ConnectionId, _nIdMapa, Theme, Filter);
            return IdEntidades < 0 ? null : new geOS_MapEntities(_Conexion, IdEntidades);
        }

        /// <summary>
        /// INICIA EL PROCESO DE CREACI�N MANUAL DE UN CONJUNTO DE ENTIDADES
        /// PARA A�ADIR ENTIDADES AL CONJUNTO INVOCAR EL M�TODO geOS_MapEntities.AddEntity
        /// </summary>
        /// <param name="Type">TIPO DE ENTIDADES QUE SE VAN A A�ADIR AL CONJUNTO</param>
        /// <returns></returns>
        public geOS_MapEntities CreateEntitiesSet(geOS_EntityType Type)
        {
            int IdEntidades = geOS_Gestor.CreaConjuntoEntidades(_Conexion.ConnectionId, _nIdMapa, (short)Type);
            return IdEntidades < 0 ? null : new geOS_MapEntities(_Conexion, IdEntidades);
        }

        /// <summary>
        /// PIDE AL USUARIO QUE DIGITALIZE UNA LOCALIZACI�N EN EL MAPA
        /// </summary>
        /// <param name="LocType">TIPO DE LOCALIZACION
        ///                         PUNTO: moPoint
        ///                         LINEA: moLine
        ///                         POLIGONO: moPolygon
        /// </param>
        /// <param name="LocColor">COLOR DE LA LOCALIZACI�N</param>
        /// <param name="LocStyle">ESTILO DE LA LOCALIZACI�N
        ///                         PUNTO: ENUMERACI�N geOS_MarkerType EXCEPTO EL VALOR moTrueTypeMarker
        ///                         LINEA: ENUMERACI�N geOS_LineType
        ///                         POLIGONO: ENUMERACI�N geOS_FillType
        /// </param>
        /// <param name="LocSize">TAMA�O DE LA LOCALIZACI�N
        ///                         PUNTO: TAMA�O DEL PUNTO EN METROS
        ///                         LINEA: ANCHO DE LA LINEA EN PIXELES
        ///                         POLIGONO: ANCHO DEL BORDE DEL POLIGONO EN PIXELES
        /// </param>
        /// <returns>NULL SI HAY ERROR O EL USUARIO HA CANCELADO</returns>
        public geOS_Location GetLocation(geOS_EntityType LocType, Color LocColor, byte LocStyle, double LocSize)
        {
            int IdLoc = geOS_Gestor.DameLocalizacion(_Conexion.ConnectionId, _nIdMapa, (short)LocType, LocColor.ToArgb(), LocStyle, LocSize);
            return IdLoc < 0 ? null : new geOS_Location(this, IdLoc);
        }

        /// <summary>
        /// CREA UNA LOCALIZACI�N DE TIPO SUPERFICIAL COMO UN BUFFER SOBRE UN TEMA Y A UNA DISTANCIA
        /// </summary>
        /// <param name="Theme">CODIGO DEL TEMA EN LA VISTA</param>
        /// <param name="Filter">FILTRO ALFANUM�RICO SOBRE EL TEMA</param>
        /// <param name="Distance">DISTANCIA DEL BUFFER</param>
        /// <param name="LocColor">COLOR DE FONDO DE LA LOCALIZACI�N</param>
        /// <param name="LocStyle">ESTILO DE LA LOCALIZACI�N (ENUMERACI�N geOS_FillType)</param>
        /// <param name="LocSize">ANCHO DEL BORDE DEL POLIGONO EN PIXELES
        /// <returns>NULL SI HA HABIDO UN ERROR</returns>
        public geOS_Location CreateBuffer(string Theme, string Filter, double Distance, Color LocColor, byte LocStyle, int LocSize)
        {
            int IdLoc = geOS_Gestor.CreaBuffer(_Conexion.ConnectionId, _nIdMapa, Theme, Distance, Filter, LocColor.ToArgb(), LocStyle, LocSize);
            return IdLoc < 0 ? null : new geOS_Location(this, IdLoc);
        }

        /// <summary>
        /// INICIA EL PROCESO DE CREACI�N MANUAL DE UNA LOCALIZACI�N
        /// </summary>
        /// <param name="LocType">TIPO DE LOCALIZACION
        ///                         PUNTO: moPoint
        ///                         LINEA: moLine
        ///                         POLIGONO: moPolygon
        /// </param>
        /// <param name="LocColor">COLOR DE LA LOCALIZACI�N</param>
        /// <param name="LocStyle">ESTILO DE LA LOCALIZACI�N
        ///                         PUNTO: ENUMERACI�N geOS_MarkerType EXCEPTO EL VALOR moTrueTypeMarker
        ///                         LINEA: ENUMERACI�N geOS_LineType
        ///                         POLIGONO: ENUMERACI�N geOS_FillType
        /// </param>
        /// <param name="LocSize">TAMA�O DE LA LOCALIZACI�N
        ///                         PUNTO: TAMA�O DEL PUNTO EN METROS
        ///                         LINEA: ANCHO DE LA LINEA EN PIXELES
        ///                         POLIGONO: ANCHO DEL BORDE DEL POLIGONO EN PIXELES
        /// </param>
        /// <returns>NULL SI HAY ERROR O EL USUARIO HA CANCELADO</returns>
        public geOS_Location CreategeOS_Location(geOS_EntityType LocType, Color LocColor, byte LocStyle, double LocSize)
        {
            int IdLoc = geOS_Gestor.CreaLocalizacion(this.Connection.ConnectionId, this.MapId, (short)LocType, LocColor.ToArgb(), LocStyle, LocSize);
            return IdLoc < 0 ? null : new geOS_Location(this, IdLoc);
        }

        /// <summary>
        /// RETORNA EL N�MERO DE LOCALIZACIONES ASOCIADAS AL MAPA
        /// </summary>
        /// <returns></returns>
        public short GetNumLocations()
        {
            return geOS_Gestor.GetNumLocalizaciones(this.Connection.ConnectionId, this.MapId);
        }

        /// <summary>
        /// RETORNA LA LOCALIZACI�N IDENTIFICADA POR EL INDICE (BASE 0)
        /// </summary>
        /// <param name="Index">INDICE DE LA LOCALIZACI�N</param>
        /// <returns>LOCALIZACI�N O NULO SI NO EXISTE</returns>
        public geOS_Location GetLocation(short Index)
        {
            int IdLoc = geOS_Gestor.GetLocalizacion(this.Connection.ConnectionId, this.MapId, Index);
            return IdLoc < 0 ? null : new geOS_Location(this, IdLoc);
        }            

        /// <summary>
        /// LIBERA TODAS LAS LOCALIZACIONES ASOCIADAS AL MAPA
        /// </summary>
        public void FreeLocations()
        {
            geOS_Gestor.LiberaLocalizaciones(this.Connection.ConnectionId, this.MapId);
        }

        /// <summary>
        /// COPIA EL MAPA
        /// </summary>
        /// <param name="CopyStyle">ESTILO
        ///             0: COPIA EL MAPA A UN FICHERO EMF
	    ///             1: COPIA EL MAPA A UN FICHERO BMP
	    ///             2: COPIA EL MAPA AL CLIPBOARD EN FORMATO EMF
	    ///             3: COPIA EL MAPA AL CLIPBOARD EN FORMATO BMP
    	///             4: COPIA EL MAPA A UN FICHERO JPG
        ///</param>
        /// <param name="FilePath">PATH DEL FICHERO</param>
        /// <param name="ScaleFactor">FACTOR DE ESCALA CON RESPECTO A LOS PIXELES EN PANTALLA DEL MAPA</param>
        /// <returns></returns>
        public bool CopyMap(short CopyStyle, string FilePath, double ScaleFactor)
        {
            return geOS_Gestor.CopiarMapa(this.Connection.ConnectionId, this.MapId, CopyStyle, FilePath, ScaleFactor) == 0 ? false : true;
        }

        /// <summary>
        /// INICIALIZA EL PROCESO DE EDICI�N DE UN TEMA
        /// </summary>
        /// <param name="EditTheme">TEMA A EDITAR</param>
        /// <param name="AskForGeocode">SI ENVIA AL CLIENTE EL EVENTO OnGetGeocode</param>
        /// <returns></returns>
        public bool EditionInit(string EditTheme,bool AskForGeocode)
        {
            return geOS_Gestor.EmpezarEdicion(this.Connection.ConnectionId, this.MapId, EditTheme, AskForGeocode ? 1 : 0) == 1 ? true : false;
        }

        /// <summary>
        /// FINALIZA EL PROCESO DE EDICI�N
        /// </summary>
        /// <returns></returns>
        public bool EditionFinish()
        {
            return geOS_Gestor.TerminarEdicion(this.Connection.ConnectionId, this.MapId) == 1 ? true : false;
        }

        /// <summary>
        /// SALVA LAS MODIFICACIONES HECHAS EN LA EDICI�N
        /// </summary>
        /// <returns></returns>
        public bool EditionSave()
        {
            return geOS_Gestor.SalvarTrabajoEdicion(this.Connection.ConnectionId, this.MapId);
        }
    }
}