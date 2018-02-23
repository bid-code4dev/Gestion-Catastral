using System;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Text;
using System.Drawing;

namespace geoEngineOS.Connection
{
    using geoEngineOS.Map;
    using geoEngineOS.Entity;

    /// <summary>
    /// CLASE QUE MANEJA LA CONEXIÓN A UN ESPACIO DE TRABAJO GEOGRÁFICO (ETG)
    /// </summary>
    public class geOS_EtgConnection : IDisposable
    {
        protected static Int32 _nNumConexiones = 0;

        protected Int32 _IdConexion;        // IDENTIFICADOR DE LA CONEXIÓN
        protected string _sETG;             // PATH DEL ETG
        protected string _sUsuario;         // USUARIO
        protected string _sPassword;        // PASSWORD

        /// <summary>
        /// IDENTIFICADOR DE CONEXIÓN
        /// </summary>
        public Int32 ConnectionId
        {
            get { return _IdConexion; }
        }

        /// <summary>
        /// PATH DEL ESPACIO DE TRABAJO GEOGRAFICO
        /// </summary>
        public string ETG
        {
            get { return _sETG; }
        }

        /// <summary>
        /// CODIGO DE USUARIO DE LA CONEXIÓN
        /// </summary>
        public string User
        {
            get { return _sUsuario; }
        }

        /// <summary>
        /// PASSWORD DEL USUARIO
        /// </summary>
        public string Password
        {
            get { return _sPassword; }
        }

        public geOS_EtgConnection()
        {
            this._IdConexion = -1;
        }

        ~geOS_EtgConnection()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            geOS_Gestor.CierraConexion(_IdConexion);

            if (disposing)
            {
                _IdConexion = -1;
                _sETG = _sUsuario = _sPassword = null;
            }
        }

        /// <summary>
        /// ABRE UNA CONEXIÓN A UN ESPACIO DE TRABAJO GEOGRÁFICO (ETG)
        /// </summary>
        /// <param name="sETG">PATH DEL ESPACIO DE TRABAJO GEOGRÁFICO</param>
        /// <param name="sUsuario">CÓDIGO DE USUARIO</param>
        /// <param name="sPassword">PASSWORD DEL USUARIO</param>
        /// <returns>TRUE/FALSE</returns>
        public bool Open(string sETG, string sUser, string sPassword)
        {
            // Por si acaso inicializamos el GESTOR
            geOS_Gestor.InitGESTOR();

            this._IdConexion = geOS_Gestor.AbreConexion(sETG, sUser, sPassword);

            if (this._IdConexion >= 0)
            {
                _sETG = sETG;
                _sUsuario = sUser;
                _sPassword = sPassword;
                _nNumConexiones++;
                return true;
            }
            else
            {
                _sETG = _sUsuario = _sPassword = "";
                return false;
            }
        }

        /// <summary>
        /// CIERRA LA CONEXIÓN
        /// </summary>
        public void Close()
        {
            geOS_Gestor.CierraConexion(_IdConexion);
            _IdConexion = -1;
            _sETG = _sUsuario = _sPassword = null;
            _nNumConexiones--;

            if (_nNumConexiones == 0)
                geOS_Gestor.ResetGESTOR();
        }

        public bool SetCoordRestriction(double MinX, double MinY, double MaxX, double MaxY)
        {
            return geOS_Gestor.SetRestriccionCoordenadas(_IdConexion, MinX, MinY, MaxX, MaxY) == 0 ? false : true;
        }

        /// <summary>
        /// INICIALIZA EL PROCESO DE PLOT
        /// </summary>
        /// <param name="sMgiTemplate">PATH DE LA PLANTILLA USADA PARA HACER EL PLOT</param>
        /// <returns></returns>
        public bool PlotInit(string sMgiTemplate)
        {
            return geOS_Gestor.InicializaImpreso(_IdConexion, sMgiTemplate) == 0 ? true : false;
        }

        /// <summary>
        /// DESTRUYE EL PROCESO DE PLOT
        /// DEBE SER INVOCADO UNA VEZ QUE SE HA REALIZADO LA IMPRESIÓN O SE HA CANCALADO
        /// </summary>
        public void PlotDestroy()
        {
            geOS_Gestor.DestruyeImpreso(_IdConexion);
        }

        /// <summary>
        /// DIBUJA EL RESULTADO DEL PLOT EN LA VENTANA QUE SE PASA COMO PARAMETRO
        /// </summary>
        /// <param name="hWnd">HANDLE DE LA VENTANA</param>
        /// <returns></returns>
        public bool PlotDraw(IntPtr hWnd)
        {
            return geOS_Gestor.DibujarImpreso(_IdConexion, hWnd) == 0 ? false : true;
        }

        /// <summary>
        /// DIBUJA EL RESULTADO DEL PLOT EN EL DEVICE CONTEXT QUE SE PASA COMO PARAMETRO
        /// </summary>
        /// <param name="hDC">HANDLE AL DEVICE CONTEXT</param>
        /// <returns></returns>
        public bool PlotDrawDC(IntPtr hDC)
        {
            return geOS_Gestor.DibujarImpresoDC(_IdConexion, hDC) == 0 ? false : true;
        }

        /// <summary>
        /// IMPRIME EL RESULTADO DEL PLOT MOSTRANDO UN DIALOGO DE PREVISUALIZACIÓN QUE PERMITE AL USUARIO
        /// CONFIGURAR CIERTOS ASPECTOS DE LA IMPRESIÓN
        /// </summary>
        /// <returns></returns>
        public bool PlotPrint()
        {
            return geOS_Gestor.ImprimirImpreso(_IdConexion) == 0 ? false : true;
        }

        /// <summary>
        /// IMPRIME EL RESULTADO DEL PLOT SIN MOSTRAR DIÁLOGO DE PREVISUALIZACIÓN
        /// </summary>
        /// <param name="hWnd">HANDLE DE LA VENTANA PADRE</param>
        /// <returns></returns>
        public bool PlotPrintDirect(IntPtr hWnd)
        {
            return geOS_Gestor.ImprimirImpresoDirectamente(_IdConexion, hWnd) == 0 ? false : true;
        }

        /// <summary>
        /// DEVUELVE EL TITULO (INFORMACIÓN EXISTENTE EN EL SummaryInfo DEL FICHERO MGI) DE UNA PLANTILLA DE PLOT
        /// ÚTIL CUANDO SE PRETENDE QUE EL USUARIO SELECCIONE UNA PLANTILLA PARA PLOTEAR
        /// </summary>
        /// <param name="sMgiTemplate">PATH DE LA PLANTILLA</param>
        /// <param name="sTitle">TITULO</param>
        /// <returns></returns>
        public bool PlotGetMgiTitle(string sMgiTemplate, ref string sTitle)
        {
            StringBuilder sAux = new StringBuilder(255);

            if (geOS_Gestor.TituloImpreso(_IdConexion, sMgiTemplate, sAux) == 0)
                return false;

            sTitle = sAux.ToString();
            return true;
        }

        /// <summary>
        /// MUESTRA UNA IMAGEN (BMP O EMF) EN EL MARCO QUE SE PASA COMO PARAMETRO
        /// </summary>
        /// <param name="sKey">CLAVE DEL MARCO EN LA PLANTILLA DE PLOT (MGI)</param>
        /// <param name="sPath">RUTA AL FICHERO (DEBEN SER FORMATOS BMP Y EMF)</param>
        /// <param name="bAdjust">SI LA IMAGEN SE DEBE AJUSTAR AL MARCO</param>
        /// <returns></returns>
        public bool PlotSetImage(string sKey, string sPath, bool bAdjust)
        {
            return geOS_Gestor.ImpresoSetRectFichero(_IdConexion, sKey, sPath, (short)(bAdjust ? 1 : 0)) == 0 ? false : true;
        }

        /// <summary>
        /// MUESTRA UN TEXTO EN LA CAJA DE TEXTO DEL MGI QUE SE PASA COMO PARAMETRO
        /// </summary>
        /// <param name="sKey">CLAVE EN EL MGI DE LA CAJA DE TEXTO</param>
        /// <param name="sText">VALOR DEL TEXTO</param>
        /// <returns></returns>
        public bool PlotSetText(string sKey, string sText)
        {
            return geOS_Gestor.ImpresoSetTexto(_IdConexion, sKey, sText) == 0 ? false : true;
        }

        /// <summary>
        /// MUESTRA EL MAPA EN EL MARCO DEL MGI QUE SE PASA COMO PARAMETRO
        /// </summary>
        /// <param name="sKey">CLAVE DEL MARCO EN EL MGI</param>
        /// <param name="Map">VENTANA DE MAPA QUE SE DESEA MOSTRAR</param>
        /// <param name="X">COORDENADA X DEL CENTRO DEL MAPA</param>
        /// <param name="Y">COORDENADA Y DEL CENTRO DEL MAPA</param>
        /// <param name="nScale">ESCALA DE SALIDA DEL MAPA</param>
        /// <returns></returns>
        public bool PlotSetMap(string sKey, geOS_MapWindow Map, double X, double Y, int Scale)
        {
            return geOS_Gestor.ImpresoSetRectMapa(_IdConexion, sKey, Map.MapId, X, Y, Scale) == 0 ? false : true;
        }

        /// <summary>
        /// MUESTRA EL MAPA EN EL MARCO DEL MGI QUE SE PASA COMO PARAMETRO
        /// </summary>
        /// <param name="sKey">CLAVE DEL MARCO EN EL MGI</param>
        /// <param name="Map">VENTANA DE MAPA QUE SE DESEA MOSTRAR</param>
        /// <param name="X">COORDENADA X DEL CENTRO DEL MAPA</param>
        /// <param name="Y">COORDENADA Y DEL CENTRO DEL MAPA</param>
        /// <param name="nScale">ESCALA DE SALIDA DEL MAPA</param>
        /// <param name="nRefScale">ESCALA DE REFERENCIA (DETERMINA LAS CAPAS Y EL RASTER)</param>
        /// <returns></returns>
        public bool PlotSetMapWithRefScale(string sKey, geOS_MapWindow Map, double X, double Y, int nScale, int nRefScale)
        {
            return geOS_Gestor.ImpresoMapaConEscalaDeReferencia(_IdConexion, sKey, Map.MapId, X, Y, nScale, nRefScale) == 0 ? false : true;
        }

        /// <summary>
        /// MUESTRA LA LEYENDA DEL MAPA EN EL MARCO DEL MGI QUE SE PASA COMO PARAMETRO
        /// </summary>
        /// <param name="sKey">CLAVE DEL MARCO EN EL MGI</param>
        /// <param name="Map">VENTANA DE MAPA CUYA LEYENDA SE DESEA MOSTRAR</param>
        /// <returns></returns>
        public bool PlotSetLegend(string sKey, geOS_MapWindow Map)
        {
            return geOS_Gestor.ImpresoSetRectLeyendaMapa(_IdConexion, sKey, Map.MapId) == 0 ? false : true;
        }

        /// <summary>
        /// MUESTRA UNA REJILLA DE COORDENADAS EN EL MARCO DEL MGI QUE SE PASA COMO PARAMETRO
        /// ESTE MARCO DEBE CONTENER UNA VENTANA DE MAPA
        /// </summary>
        /// <param name="sKey">CLAVE DEL MARCO EN EL MGI</param>
        /// <param name="nDistance">TAMAÑO EN METROS DE LA REJILLA</param>
        /// <returns></returns>
        public bool PlotSetCoordsGrid(string sKey, int nDistance)
        {
            return geOS_Gestor.ImpresoSetRectMapaUtms(_IdConexion, sKey, nDistance) == 0 ? false : true;
        }

        /// <summary>
        /// DIBUJA UN RECTÁNGULO EN EL MARCO DEL MGI QUE SE PASA COMO PARAMETRO
        /// </summary>
        /// <param name="sKey">CLAVE DEL MARCO EN EL MGI</param>
        /// <param name="Left">X IZQUIERDA DEL RECTÁNGULO</param>
        /// <param name="Bottom">Y ABAJO DEL RECTANGULO</param>
        /// <param name="Right">X DERECHA DEL RECTANGULO</param>
        /// <param name="Top">Y ARRIBA DEL RECTANGULO</param>
        /// <param name="BackColor">COLOR DE FONDO</param>
        /// <param name="FillStyle">ESTILO DE RELLENO</param>
        /// <param name="EdgeColor">COLOR DE BORDE</param>
        /// <param name="Size">ANCHO EN UNIDADES DE DIBUJO DEL BORDE</param>
        /// <returns></returns>
        public bool PlotDrawRect(string sKey, double Left, double Bottom, double Right, double Top, Color BackColor, geOS_FillType FillStyle, Color EdgeColor, short Size)
        {
            return geOS_Gestor.ImpresoPintarRectangulo(_IdConexion, sKey, Left, Bottom, Right, Top, BackColor.ToArgb(), (short)FillStyle, EdgeColor.ToArgb(), Size) == 0 ? false : true;
        }

        /// <summary>
        /// DIBUJA UNA ENTIDAD EN EL MARCO DEL MGI QUE SE PASA COMO PARAMETRO
        /// </summary>
        /// <param name="sKey">CLAVE DEL MARCO EN EL MGI</param>
        /// <param name="Entity">ENTIDAD QUE SE DESEA PINTAR</param>
        /// <param name="BackColor">COLOR DE FONDO</param>
        /// <param name="FillStyle">ESTILO DE RELLENO (SI LA ENTIDAD ES POLIGONAL)</param>
        /// <param name="EdgeColor">COLOR DE BORDE (SI LA ENTIDAD ES POLIGONAL)</param>
        /// <param name="Size">ANCHO EN UNIDADES DE DIBUJO DEL BORDE</param>
        /// <returns></returns>
        public bool PlotDrawEntity(string sKey, geOS_MapEntity Entity, Color BackColor, geOS_FillType FillStyle, Color EdgeColor, short Size)
        {
            return geOS_Gestor.ImpresoPintarEntidad(_IdConexion, sKey, Entity.Id, BackColor.ToArgb(), (short)FillStyle, EdgeColor.ToArgb(), Size) == 0 ? false : true;
        }

        /// <summary>
        /// DIBUJA UN TEXTO ASOCIADO A UNA ENTIDAD EN EL MARCO DEL MGI QUE SE PASA COMO PARAMETRO
        /// </summary>
        /// <param name="sKey">CLAVE DEL MARCO EN EL MGI</param>
        /// <param name="Entity">ENTIDAD CUYO TEXTO SE DESEA PINTAR</param>
        /// <param name="sText">TEXTO</param>
        /// <param name="sFont">FUENTE DE LETRA</param>
        /// <param name="TextColor">COLOR DEL TEXTO</param>
        /// <param name="MMsSize">TAMAÑO EN MILIMETROS DEL TEXTO</param>
        /// <param name="OffsetFactor">USADO EN EL CASO DE PINTAR TEXTOS ASOCIADOS A ENTIDADES PUNTUALES
        ///                            DEFINE EL FACTOR DE OFFSET DONDE SERA DIBUJADO EL TEXTO RESPECTO AL TAMAÑO DEL PUNTO
        /// </param>
        /// <returns></returns>
        public bool PlotDrawEntityText(string sKey, geOS_MapEntity Entity, string sText, string sFont, Color TextColor, double MMsSize, double OffsetFactor)
        {
            return geOS_Gestor.ImpresoPintarTextoEntidad(_IdConexion, sKey, Entity.Id, sFont, sText, TextColor.ToArgb(), MMsSize, OffsetFactor) == 0 ? false : true;
        }

        /// <summary>
        /// CALCULA LA ESCALA MINIMA A LA QUE SE DEBE DIBUJAR UN MAPA PARA QUE EL RECTANGULO DEFINIDO POR LAS COORDENADAS
        /// MinX, MinY, MaxX y MaxY PUEDA SER REPRESENTADO EN EL PLOT
        /// </summary>
        /// <param name="sKey">CLAVE DEL MARCO EN EL MGI</param>
        /// <param name="MinX">COORDENADA X MINIMA DEL RECTANGULO</param>
        /// <param name="MinY">COORDENADA Y MINIMA DEL RECTANGULO</param>
        /// <param name="MaxX">COORDENADA X MAXIMA DEL RECTANGULO</param>
        /// <param name="MaxY">COORDENADA Y MAXIMA DEL RECTANGULO</param>
        /// <returns>ESCALA</returns>
        public int PlotGetMinScale(string sKey, double MinX, double MinY, double MaxX, double MaxY)
        {
            return geOS_Gestor.ImpresoCalculaEscalaMinima(_IdConexion, sKey, MinX, MinY, MaxX, MaxY);
        }

        /// <summary>
        /// DEVUELVE LA EXTENSIÓN REAL DE MAPA QUE SERÁ DIBUJADA
        /// SI SE DESEA CONOCER LA EXTENSIÓN FINAL DE LA SALIDA, ESTA FUNCIÓN DEBERÁ SER INVOCADA DESPUÉS DE
        /// INVOCAR LA FUNCIÓN PlotSetMap
        /// </summary>
        /// <param name="sKey">CLAVE DEL MARCO EN EL MGI LIGADO A UN MAPA</param>
        /// <param name="MinX">COORDENADA X MINIMA DEL RECTANGULO</param>
        /// <param name="MinY">COORDENADA Y MINIMA DEL RECTANGULO</param>
        /// <param name="MaxX">COORDENADA X MAXIMA DEL RECTANGULO</param>
        /// <param name="MaxY">COORDENADA Y MAXIMA DEL RECTANGULO</param>
        /// <returns></returns>
        public bool PlotGetMapExtent(string sKey, ref double MinX, ref double MinY, ref double MaxX, ref double MaxY)
        {
            return geOS_Gestor.ImpresoExtensionMapa(_IdConexion, sKey, ref MinX, ref MinY, ref MaxX, ref MaxY) == 0 ? false : true;
        }

        /// <summary>
        /// ESTABLECE EL MODO DE PLOTEO
        /// </summary>
        /// <param name="Type">TIPO DE SALIDA
        /// 0: DIRECTA      => EL PLOT ES ENVIADO DIRECTAMENTE A LA IMPRESORA
        /// 1: PREPROCESADA => EL PLOT ES PREPROCESADO EN LA MAQUINA DEL USUARIO Y EL BITMAP RESULTANTE
        ///                    ES ENVIADO A LA IMPRESORA
        /// </param>
        /// <returns></returns>
        public bool PlotSetOutputType(short Type)
        {
            return geOS_Gestor.ImpresoSetTipoSalida(_IdConexion, Type) == 0 ? false : true;
        }

        /// <summary>
        /// ESTABLECE EL FACTOR DE RESOLUCIÓN DEL PLOTEO (APLICABLE SÓLO CUANDO EL TIPO DE SALIDA ES PREPROCESADA)
        /// </summary>
        /// <param name="Factor">FACTOR DE RESOLUCIÓN DESEADO EN MULTIPLOS DE LOS PIXELES DE PANTALLA</param>
        /// <returns></returns>
        public bool PlotSetResolutionFactor(double Factor)
        {
            return geOS_Gestor.ImpresoSetFactorResolucion(_IdConexion, Factor) == 0 ? false : true;
        }

        /// <summary>
        /// ESTABLECE SI SE DESEA MOSTRAR O NO EL DIALOGO ESTANDAR DE SELECCIÓN DE IMPRESORA
        /// ANTES DE IMPRIMIR EL MAPA
        /// </summary>
        /// <param name="bShow"></param>
        /// <returns></returns>
        public bool PlotShowPrintDialog(bool bShow)
        {
            return geOS_Gestor.ImpresoMuestraDialogoImpresora(_IdConexion, bShow ? (short)1 : (short)0) == 0 ? false : true;
        }
    }
}