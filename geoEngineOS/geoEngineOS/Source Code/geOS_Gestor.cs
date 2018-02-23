using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace geoEngineOS
{
    /// <summary>
    /// GeoENGINE internal interface API
    /// </summary>
    public static class geOS_Gestor
    {
        [DllImport("GestorGEO.dll", CharSet = CharSet.Auto)]
        public static extern int InitGESTOR();

        [DllImport("GestorGEO.dll", CharSet = CharSet.Auto)]
        public static extern void ResetGESTOR();

        [DllImport("GestorGEO.dll", CharSet = CharSet.Auto)]
        public static extern int AbreConexion(
           [MarshalAs(UnmanagedType.LPStr)]string sETG,
           [MarshalAs(UnmanagedType.LPStr)]string sUsuario,
           [MarshalAs(UnmanagedType.LPStr)]string sClave);

        [DllImport("GestorGEO.dll", CharSet = CharSet.Auto)]
        public static extern void CierraConexion(int IdConexion);

        [DllImport("GestorGEO.dll", CharSet = CharSet.Auto)]
        public static extern int AttachWindow(int IdConexion, int wHandle, short bUserEnv);

        [DllImport("GestorGEO.dll", CharSet = CharSet.Auto)]
        public static extern void DetachWindow(int IdConexion, int IdMapView);

        [DllImport("GestorGEO.dll", CharSet = CharSet.Auto)]
        public static extern void MuestraDialogoGESTOR(short bShow);

        [DllImport("GestorGEO.dll", CharSet = CharSet.Auto)]
        public static extern int GetUsuarioConexion(int IdConexion, [MarshalAs(UnmanagedType.LPStr)]StringBuilder sUsuario);

        [DllImport("GestorGEO.dll", CharSet = CharSet.Auto)]
        public static extern bool GetPathCompleto(int IdConexion, [MarshalAs(UnmanagedType.LPStr)]StringBuilder sPath);

        [DllImport("GestorGEO.dll", CharSet = CharSet.Auto)]
        public static extern void MuestraProgresoCarga(short bShow);

        [DllImport("GestorGEO.dll", CharSet = CharSet.Auto)]
        public static extern int AttachWindowVista(int IdConexion, int wHandle, short bUserEnv, [MarshalAs(UnmanagedType.LPStr)]string sView);

        [DllImport("GestorGEO.dll", CharSet = CharSet.Auto)]
        public static extern void AjustaMapa(int IdConexion, int IdMapView);

        [DllImport("GestorGEO.dll", CharSet = CharSet.Auto)]
        public static extern int SetVistaPajaro(IntPtr hWnd, int IdConexion, int IdMapView,
                            [MarshalAs(UnmanagedType.LPStr)]string sCodigoVista, double FactorEscala, int ColorFondo, int ColorCaja);

        [DllImport("GestorGEO.dll", CharSet = CharSet.Auto)]
        public static extern int SetTamanosVistaPajaro(int IdConexion, int IdMapView, int MinShowSize, int MinBoxSize);

        [DllImport("GestorGEO.dll", CharSet = CharSet.Auto)]
        public static extern int SetVistaLocalizacion(IntPtr hWnd, int IdConexion, int IdMapView,
                            [MarshalAs(UnmanagedType.LPStr)]string sViewCode, [MarshalAs(UnmanagedType.LPStr)]string sLocTheme);

        [DllImport("GestorGEO.dll", CharSet = CharSet.Auto)]
        public static extern int SetAmbitoVistaLocalizacion(int IdConexion, int IdMapView, [MarshalAs(UnmanagedType.LPStr)]string sTemaAmbito, int EscalaAmbito);

        [DllImport("GestorGEO.dll", CharSet = CharSet.Auto)]
        public static extern void MuestraMensajes(int IdConexion, int IdMapView, short bShow);

        [DllImport("GestorGEO.dll", CharSet = CharSet.Auto)]
        public static extern bool MuestraEntidadesSinInfo(int IdConexion, int IdMapView,
                            [MarshalAs(UnmanagedType.LPStr)]string sTheme, [MarshalAs(UnmanagedType.Bool)]bool bShow);

        [DllImport("GestorGEO.dll", CharSet = CharSet.Auto)]
        public static extern bool GetTemaEntidadesSinInfo(int IdConexion, int IdMapView,
                            [MarshalAs(UnmanagedType.LPStr)]StringBuilder sTheme);

        [DllImport("GestorGEO.dll", CharSet = CharSet.Auto)]
        public static extern void MuestraUTMs(int IdConexion, int IdMapView, short bShow);

        [DllImport("GestorGEO.dll", CharSet = CharSet.Auto)]
        public static extern void SetVentanaUTMS(int IdConexion, int IdMapView, IntPtr hWnd);

        [DllImport("GestorGEO.dll", CharSet = CharSet.Auto)]
        public static extern void SetVentanaUtmsZ(int IdConexion, int IdMapView, IntPtr hWnd);

        [DllImport("GestorGEO.dll", CharSet = CharSet.Auto)]
        public static extern void MuestraEscala(int IdConexion, int IdMapView, short bShow);

        [DllImport("GestorGEO.dll", CharSet = CharSet.Auto)]
        public static extern void SetVentanaEscala(int IdConexion, int IdMapView, IntPtr hWnd);

        [DllImport("GestorGEO.dll", CharSet = CharSet.Auto)]
        public static extern void EdicionTemas(int IdConexion, int IdMapView);

        [DllImport("GestorGEO.dll", CharSet = CharSet.Auto)]
        public static extern void MuestraToolbar(int IdConexion, int IdMapView, short ToolbarId, short bShow);

        [DllImport("GestorGEO.dll", CharSet = CharSet.Auto)]
        public static extern void SetSistemaRapido(int IdConexion, bool bSet);

        [DllImport("GestorGEO.dll", CharSet = CharSet.Auto)]
        public static extern bool AnadeTemasVista(int IdConexion, int IdMapView, bool bShowSimple, bool bShowThematics, bool bShowQuerys);

        [DllImport("GestorGEO.dll", CharSet = CharSet.Auto)]
        public static extern bool AnadeTemaMapa(int IdConexion, int IdMapView, [MarshalAs(UnmanagedType.LPStr)]string sTheme, int nMinScale, int nMaxScale);

        [DllImport("GestorGEO.dll", CharSet = CharSet.Auto)]
        public static extern bool AnadeTemaMapaParams(int IdConexion, int IdMapView, [MarshalAs(UnmanagedType.LPStr)]string sPath,
                        [MarshalAs(UnmanagedType.LPStr)]string sThemeCode, [MarshalAs(UnmanagedType.LPStr)]string sGeocode,
                        short ShapeType, int nMinScale, int nMaxScale, int nColor, short nStyle, short nSize,
                        [MarshalAs(UnmanagedType.LPStr)]string sFont, short nCharacter, bool bScaleDepends, double fSize,
                        bool bOutline, int nOutlineColor);

        [DllImport("GestorGEO.dll", CharSet = CharSet.Auto)]
        public static extern bool SetTemaVisible(int IdConexion, int IdMapView, [MarshalAs(UnmanagedType.LPStr)]string sTheme, bool bVisible);

        [DllImport("GestorGEO.dll", CharSet = CharSet.Auto)]
        public static extern bool EliminaTemaMapa(int IdConexion, int IdMapView, [MarshalAs(UnmanagedType.LPStr)]string sTheme);

        [DllImport("GestorGEO.dll", CharSet = CharSet.Auto)]
        public static extern bool RefreshTemaMapa(int IdConexion, int IdMapView, [MarshalAs(UnmanagedType.LPStr)]string sTheme);

        [DllImport("GestorGEO.dll", CharSet = CharSet.Auto)]
        public static extern void ZoomMapa(int IdConexion, int IdMapView);

        [DllImport("GestorGEO.dll", CharSet = CharSet.Auto)]
        public static extern void ZoomInMapa(int IdConexion, int IdMapView);

        [DllImport("GestorGEO.dll", CharSet = CharSet.Auto)]
        public static extern void ZoomOutMapa(int IdConexion, int IdMapView);

        [DllImport("GestorGEO.dll", CharSet = CharSet.Auto)]
        public static extern void ZoomAnteriorMapa(int IdConexion, int IdMapView);

        [DllImport("GestorGEO.dll", CharSet = CharSet.Auto)]
        public static extern void ZoomExtentMapa(int IdConexion, int IdMapView);

        [DllImport("GestorGEO.dll", CharSet = CharSet.Auto)]
        public static extern void RedrawMapWindow(int IdConexion, int IdMapView);

        [DllImport("GestorGEO.dll", CharSet = CharSet.Auto)]
        public static extern void HacerPanning(int IdConexion, int IdMapView);

        [DllImport("GestorGEO.dll", CharSet = CharSet.Auto)]
        public static extern void HacerPanningPunto(int IdConexion, int IdMapView);

        [DllImport("GestorGEO.dll", CharSet = CharSet.Auto)]
        public static extern void GetExtentMapa(int IdConexion, int IdMapView, ref double MinX, ref double MinY, ref double MaxX, ref double MaxY);

        [DllImport("GestorGEO.dll", CharSet = CharSet.Auto)]
        public static extern bool GetExtentTema(int IdConexion, int IdMapView, [MarshalAs(UnmanagedType.LPStr)]string sTheme, ref double MinX, ref double MinY, ref double MaxX, ref double MaxY);

        [DllImport("GestorGEO.dll", CharSet = CharSet.Auto)]
        public static extern void ZoomCoordenadas(int IdConexion, int IdMapView, double X1, double Y1, double X2, double Y2);

        [DllImport("GestorGEO.dll", CharSet = CharSet.Auto)]
        public static extern void ZoomPorEscala(int IdConexion, int IdMapView, int nScale, double X, double Y);

        [DllImport("GestorGEO.dll", CharSet = CharSet.Auto)]
        public static extern void SetVerImagenesMapa(int IdConexion, int IdMapView, short bShow);

        [DllImport("GestorGEO.dll", CharSet = CharSet.Auto)]
        public static extern void SetVerVectoresMapa(int IdConexion, int IdMapView, short bShow);

        [DllImport("GestorGEO.dll", CharSet = CharSet.Auto)]
        public static extern int GetVerImagenesMapa(int IdConexion, int IdMapView);

        [DllImport("GestorGEO.dll", CharSet = CharSet.Auto)]
        public static extern int GetVerVectoresMapa(int IdConexion, int IdMapView);

        [DllImport("GestorGEO.dll", CharSet = CharSet.Auto)]
        public static extern void SalvaPreferenciasUsuario(int IdConexion, int IdMapView);

        [DllImport("GestorGEO.dll", CharSet = CharSet.Auto)]
        public static extern int SetupColorFondo(int IdConexion, int IdMapView);

        [DllImport("GestorGEO.dll", CharSet = CharSet.Auto)]
        public static extern int GetColorFondo(int IdConexion, int IdMapView);

        [DllImport("GestorGEO.dll", CharSet = CharSet.Auto)]
        public static extern void SetColorFondo(int IdConexion, int IdMapView, int nColor);

        [DllImport("GestorGEO.dll", CharSet = CharSet.Auto)]
        public static extern int GetScrollBars(int IdConexion, int IdMapView);

        [DllImport("GestorGEO.dll", CharSet = CharSet.Auto)]
        public static extern void SetScrollBars(int IdConexion, int IdMapView, short bShow);

        [DllImport("GestorGEO.dll", CharSet = CharSet.Auto)]
        public static extern int SelecEntidadTema(int IdConexion, int IdMapView, [MarshalAs(UnmanagedType.LPStr)]string sTheme);

        [DllImport("GestorGEO.dll", CharSet = CharSet.Auto)]
        public static extern void FreeEntidad(int IdConexion, int IdEntity);

        [DllImport("GestorGEO.dll", CharSet = CharSet.Auto)]
        public static extern void FlashEntidad(int IdConexion, int IdMapView, int IdEntity, short nTimes);

        [DllImport("GestorGEO.dll", CharSet = CharSet.Auto)]
        public static extern bool VerSentidoEntidad(int IdConexion, int IdMapView, int IdEntity);

        [DllImport("GestorGEO.dll", CharSet = CharSet.Auto)]
        public static extern void ZoomEntidad(int IdConexion, int IdMapView, int IdEntity);

        [DllImport("GestorGEO.dll", CharSet = CharSet.Auto)]
        public static extern int GetParamsEntidad(int IdConexion, int IdEntity, [MarshalAs(UnmanagedType.LPStr)]StringBuilder sTheme, [MarshalAs(UnmanagedType.LPStr)]StringBuilder sGeocode);

        [DllImport("GestorGEO.dll", CharSet = CharSet.Auto)]
        public static extern int GetFeatureID(int IdConexion, int IdEntity);

        [DllImport("GestorGEO.dll", CharSet = CharSet.Auto)]
        public static extern int GetColorSeleccion(int IdConexion, int IdMapView);

        [DllImport("GestorGEO.dll", CharSet = CharSet.Auto)]
        public static extern void SetColorSeleccion(int IdConexion, int IdMapView, int nColor);

        [DllImport("GestorGEO.dll", CharSet = CharSet.Auto)]
        public static extern int SetupColorSeleccion(int IdConexion, int IdMapView);

        [DllImport("GestorGEO.dll", CharSet = CharSet.Auto)]
        public static extern int SelecEntidadPorGeocodigo(int IdConexion, int IdMapView, [MarshalAs(UnmanagedType.LPStr)]string sTheme, [MarshalAs(UnmanagedType.LPStr)]string sGeocode);

        [DllImport("GestorGEO.dll", CharSet = CharSet.Auto)]
        public static extern int ExisteEntidadPorGeocodigo(int IdConexion, int IdMapView, [MarshalAs(UnmanagedType.LPStr)]string sTheme, [MarshalAs(UnmanagedType.LPStr)]string sGeocode);

        [DllImport("GestorGEO.dll", CharSet = CharSet.Auto)]
        public static extern int SetGeocodigoEntidad(int IdConexion, int IdEntity, [MarshalAs(UnmanagedType.LPStr)]string sGeocode);

        [DllImport("GestorGEO.dll", CharSet = CharSet.Auto)]
        public static extern double GetAreaEntidad(int IdConexion, int IdEntity);

        [DllImport("GestorGEO.dll", CharSet = CharSet.Auto)]
        public static extern double GetLongitudEntidad(int IdConexion, int IdEntity);

        [DllImport("GestorGEO.dll", CharSet = CharSet.Auto)]
        public static extern int GetExtentEntidad(int IdConexion, int IdEntity, ref double MinX, ref double MinY, ref double MaxX, ref double MaxY);

        [DllImport("GestorGEO.dll", CharSet = CharSet.Auto)]
        public static extern int GetNumCoordsEntidad(int IdConexion, int IdEntity, ref short nNumCoords);

        [DllImport("GestorGEO.dll", CharSet = CharSet.Auto)]
        public static extern bool GetCoordenadaEntidad(int IdConexion, int IdEntity, int nIndex, ref double X, ref double Y, ref short nPart);

        [DllImport("GestorGEO.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr GetEntidadShape(int IdConexion, int IdEntity);

        [DllImport("GestorGEO.dll", CharSet = CharSet.Auto)]
        public static extern int GetEntidadShapeType(int IdConexion, int IdEntity);

        [DllImport("GestorGEO.dll", CharSet = CharSet.Auto)]
        public static extern void GetAtribEntidad(int IdConexion, int IdEntity, [MarshalAs(UnmanagedType.LPStr)]string sAtrib, [MarshalAs(UnmanagedType.LPStr)]StringBuilder sValue);

        [DllImport("GestorGEO.dll", CharSet = CharSet.Auto)]
        public static extern void SetAtribEntidad(int IdConexion, int IdEntity, [MarshalAs(UnmanagedType.LPStr)]string sAtrib, [MarshalAs(UnmanagedType.LPStr)]string sValue);

        [DllImport("GestorGEO.dll", CharSet = CharSet.Auto)]
        public static extern int GetVistaMapa(int IdConexion, int IdMapView, [MarshalAs(UnmanagedType.LPStr)]StringBuilder sView);

        [DllImport("GestorGEO.dll", CharSet = CharSet.Auto)]
        public static extern int GetTemaSeleccion(int IdConexion, int IdMapView, [MarshalAs(UnmanagedType.LPStr)]StringBuilder sTheme);

        [DllImport("GestorGEO.dll", CharSet = CharSet.Auto)]
        public static extern int ActivaTemaSeleccion(int IdConexion, int IdMapView, [MarshalAs(UnmanagedType.LPStr)]string sTheme);

        [DllImport("GestorGEO.dll", CharSet = CharSet.Auto)]
        public static extern int ElegirTemaSeleccion(int IdConexion, int IdMapView, [MarshalAs(UnmanagedType.LPStr)]StringBuilder sTheme);

        [DllImport("GestorGEO.dll", CharSet = CharSet.Auto)]
        public static extern void MuestraTemaActivo(int IdConexion, int IdMapView, short bShow);

        [DllImport("GestorGEO.dll", CharSet = CharSet.Auto)]
        public static extern void RellenaComboBoxTemas(int IdConexion, int IdMapView, IntPtr hWnd);

        [DllImport("GestorGEO.dll", CharSet = CharSet.Auto)]
        public static extern void RellenaListBoxTemas(int IdConexion, int IdMapView, IntPtr hWnd);

        [DllImport("GestorGEO.dll", CharSet = CharSet.Auto)]
        public static extern int GetNumTemasMapa(int IdConexion, int IdMapView);

        [DllImport("GestorGEO.dll", CharSet = CharSet.Auto)]
        public static extern int GetTemaMapa(int IdConexion, int IdMapView, short nIndex, [MarshalAs(UnmanagedType.LPStr)]StringBuilder sTheme, [MarshalAs(UnmanagedType.LPStr)]StringBuilder sDescrip);

        [DllImport("GestorGEO.dll", CharSet = CharSet.Auto)]
        public static extern bool GetTemaMapaX(int IdConexion, int IdMapView, [MarshalAs(UnmanagedType.LPStr)]string sTheme, [MarshalAs(UnmanagedType.LPStr)]StringBuilder sDescrip, ref int ShapeType, ref int ThemeType);

        [DllImport("GestorGEO.dll", CharSet = CharSet.Auto)]
        public static extern void SetVentanaTemaActivo(int IdConexion, int IdMapView, IntPtr hWnd);

        [DllImport("GestorGEO.dll", CharSet = CharSet.Auto)]
        public static extern void SetVentanaDescripcionTemaActivo(int IdConexion, int IdMapView, IntPtr hWnd);

        [DllImport("GestorGEO.dll", CharSet = CharSet.Auto)]
        public static extern void SetVentanaDescripcionVista(int IdConexion, int IdMapView, IntPtr hWnd);

        [DllImport("GestorGEO.dll", CharSet = CharSet.Auto)]
        public static extern int SelecEntidad(int IdConexion, int IdMapView);

        [DllImport("GestorGEO.dll", CharSet = CharSet.Auto)]
        public static extern int SelecEntidades(int IdConexion, int IdMapView);

        [DllImport("GestorGEO.dll", CharSet = CharSet.Auto)]
        public static extern int SelecEntidadesTema(int IdConexion, int IdMapView, [MarshalAs(UnmanagedType.LPStr)]string sTheme);

        [DllImport("GestorGEO.dll", CharSet = CharSet.Auto)]
        public static extern bool GetStatsTema(int IdConexion, int IdMapView,
                    [MarshalAs(UnmanagedType.LPStr)]string sTheme,
                    [MarshalAs(UnmanagedType.LPStr)]string sFilter,
                    [MarshalAs(UnmanagedType.LPStr)]string sField,
                    ref int Count, ref int Min, ref int Max, ref int Mean, ref int Sum, ref int DevStd);

        [DllImport("GestorGEO.dll", CharSet = CharSet.Auto)]
        public static extern int BuscaEntidadesFiltro(int IdConexion, int IdMapView,
                    [MarshalAs(UnmanagedType.LPStr)]string sTheme, [MarshalAs(UnmanagedType.LPStr)]string sFilter);

        [DllImport("GestorGEO.dll", CharSet = CharSet.Auto)]
        public static extern int BuscaEntidadesLocalizacion(int IdConexion, int IdMapView, int IdgeOS_Location, short SpatialOp,
                    [MarshalAs(UnmanagedType.LPStr)]string sTheme, [MarshalAs(UnmanagedType.LPStr)]string sFilter);

        [DllImport("GestorGEO.dll", CharSet = CharSet.Auto)]
        public static extern int BuscaEntidadesDistancia(int IdConexion, int IdMapView, int IdgeOS_Location, double Dist,
                    [MarshalAs(UnmanagedType.LPStr)]string sTheme, [MarshalAs(UnmanagedType.LPStr)]string sFilter);

        [DllImport("GestorGEO.dll", CharSet = CharSet.Auto)]
        public static extern int CreaConjuntoEntidades(int IdConexion, int IdMapView, short ShapeType);

        [DllImport("GestorGEO.dll", CharSet = CharSet.Auto)]
        public static extern bool AddEntidadConjunto(int IdConexion, int IdEntities, int IdEntity);

        [DllImport("GestorGEO.dll", CharSet = CharSet.Auto)]
        public static extern int GetEntidadConjunto(int IdConexion, int IdEntities, short Index);

        [DllImport("GestorGEO.dll", CharSet = CharSet.Auto)]
        public static extern int GetGeocodigoEntidadConjunto(int IdConexion, int IdEntities, short Index, [MarshalAs(UnmanagedType.LPStr)]StringBuilder Geocode);

        [DllImport("GestorGEO.dll", CharSet = CharSet.Auto)]
        public static extern int GetNumEntidadesConjunto(int IdConexion, int IdEntities);

        [DllImport("GestorGEO.dll", CharSet = CharSet.Auto)]
        public static extern void ZoomConjuntoEntidades(int IdConexion, int IdMapView, int IdEntities);

        [DllImport("GestorGEO.dll", CharSet = CharSet.Auto)]
        public static extern void FlashConjuntoEntidades(int IdConexion, int IdMapView, int IdEntities, short Times);

        [DllImport("GestorGEO.dll", CharSet = CharSet.Auto)]
        public static extern void FreeEntidades(int IdConexion, int IdEntities);

        [DllImport("GestorGEO.dll", CharSet = CharSet.Auto)]
        public static extern int GetEscalaMapa(int IdConexion, int IdMapView);

        [DllImport("GestorGEO.dll", CharSet = CharSet.Auto)]
        public static extern void MuestraVentanaDatos(int IdConexion, int IdMapView);

        [DllImport("GestorGEO.dll", CharSet = CharSet.Auto)]
        public static extern void MuestraVentanaDatosTema(int IdConexion, int IdMapView, [MarshalAs(UnmanagedType.LPStr)]string sTheme);

        [DllImport("GestorGEO.dll", CharSet = CharSet.Auto)]
        public static extern void MuestraEditorConsultas(int IdConexion, int IdMapView);

        [DllImport("GestorGEO.dll", CharSet = CharSet.Auto)]
        public static extern void MuestraEditorTematicos(int IdConexion, int IdMapView);

        [DllImport("GestorGEO.dll", CharSet = CharSet.Auto)]
        public static extern void MuestraEditorTemas(int IdConexion, int IdMapView);

        [DllImport("GestorGEO.dll", CharSet = CharSet.Auto)]
        public static extern void PermitirEdicionVistas(int IdConexion, int IdMapView, short bAllow);

        [DllImport("GestorGEO.dll", CharSet = CharSet.Auto)]
        public static extern void MuestraEditorVistas(int IdConexion, int IdMapView);

        [DllImport("GestorGEO.dll", CharSet = CharSet.Auto)]
        public static extern int MuestraSeleccionarVista(int IdConexion, int IdMapView, int bActivar, [MarshalAs(UnmanagedType.LPStr)]StringBuilder View);

        [DllImport("GestorGEO.dll", CharSet = CharSet.Auto)]
        public static extern void GetDescripcionVista(int IdConexion, [MarshalAs(UnmanagedType.LPStr)]string View, [MarshalAs(UnmanagedType.LPStr)]StringBuilder Descrip);

        [DllImport("GestorGEO.dll", CharSet = CharSet.Auto)]
        public static extern void GetObservacionVista(int IdConexion, [MarshalAs(UnmanagedType.LPStr)]string View, [MarshalAs(UnmanagedType.LPStr)]StringBuilder Observ);

        [DllImport("GestorGEO.dll", CharSet = CharSet.Auto)]
        public static extern int MuestraSeleccionarCampoTema(int IdConexion, int IdMapView, [MarshalAs(UnmanagedType.LPStr)]string Theme, [MarshalAs(UnmanagedType.LPStr)]StringBuilder Field);

        [DllImport("GestorGEO.dll", CharSet = CharSet.Auto)]
        public static extern void MuestraBuscarToponimos(int IdConexion, int IdMapView, [MarshalAs(UnmanagedType.LPStr)]string TopoBase);

        [DllImport("GestorGEO.dll", CharSet = CharSet.Auto)]
        public static extern void MuestraBuscarToponimosConParametros(int IdConexion, int IdMapView, [MarshalAs(UnmanagedType.LPStr)]string TopoBase, int MinScale, [MarshalAs(UnmanagedType.LPStr)]string SearchTheme);

        [DllImport("GestorGEO.dll", CharSet = CharSet.Auto)]
        public static extern void ActivarToolTip(int IdConexion, int IdMapView, short bActivate);

        [DllImport("GestorGEO.dll", CharSet = CharSet.Auto)]
        public static extern int EstaToolTipActivo(int IdConexion, int IdMapView);

        [DllImport("GestorGEO.dll", CharSet = CharSet.Auto)]
        public static extern int SetCampoTip(int IdConexion, int IdMapView, [MarshalAs(UnmanagedType.LPStr)]string Field);

        [DllImport("GestorGEO.dll", CharSet = CharSet.Auto)]
        public static extern int GetCampoTip(int IdConexion, int IdMapView, [MarshalAs(UnmanagedType.LPStr)]StringBuilder Field);

        [DllImport("GestorGEO.dll", CharSet = CharSet.Auto)]
        public static extern int DibujarImpreso(int IdConexion, IntPtr hWnd);

        [DllImport("GestorGEO.dll", CharSet = CharSet.Auto)]
        public static extern int DibujarImpresoDC(int IdConexion, IntPtr hDC);

        [DllImport("GestorGEO.dll", CharSet = CharSet.Auto)]
        public static extern int ImprimirImpreso(int IdConexion);

        [DllImport("GestorGEO.dll", CharSet = CharSet.Auto)]
        public static extern int ImprimirImpresoDirectamente(int IdConexion, IntPtr hWnd);

        [DllImport("GestorGEO.dll", CharSet = CharSet.Auto)]
        public static extern int InicializaImpreso(int IdConexion, [MarshalAs(UnmanagedType.LPStr)]string Template);

        [DllImport("GestorGEO.dll", CharSet = CharSet.Auto)]
        public static extern int TituloImpreso(int IdConexion, [MarshalAs(UnmanagedType.LPStr)]string Template, [MarshalAs(UnmanagedType.LPStr)]StringBuilder Title);

        [DllImport("GestorGEO.dll", CharSet = CharSet.Auto)]
        public static extern void DestruyeImpreso(int IdConexion);

        [DllImport("GestorGEO.dll", CharSet = CharSet.Auto)]
        public static extern int ImpresoSetRectFichero(int IdConexion, [MarshalAs(UnmanagedType.LPStr)]string Key, [MarshalAs(UnmanagedType.LPStr)]string File, short bAdjust);

        [DllImport("GestorGEO.dll", CharSet = CharSet.Auto)]
        public static extern int ImpresoSetTexto(int IdConexion, [MarshalAs(UnmanagedType.LPStr)]string Key, [MarshalAs(UnmanagedType.LPStr)]string text);

        [DllImport("GestorGEO.dll", CharSet = CharSet.Auto)]
        public static extern int ImpresoSetRectMapa(int IdConexion, [MarshalAs(UnmanagedType.LPStr)]string Key, int IdMapView, double X, double Y, int Scale);

        [DllImport("GestorGEO.dll", CharSet = CharSet.Auto)]
        public static extern int ImpresoSetRectLeyendaMapa(int IdConexion, [MarshalAs(UnmanagedType.LPStr)]string Key, int IdMapView);

        [DllImport("GestorGEO.dll", CharSet = CharSet.Auto)]
        public static extern int ImpresoSetRectMapaUtms(int IdConexion, [MarshalAs(UnmanagedType.LPStr)]string Key, int UtmDist);

        [DllImport("GestorGEO.dll", CharSet = CharSet.Auto)]
        public static extern int ImpresoMapaConEscalaDeReferencia(int IdConexion, [MarshalAs(UnmanagedType.LPStr)]string Key, int IdMapView, double X, double Y, int Scale, int RefScale);

        [DllImport("GestorGEO.dll", CharSet = CharSet.Auto)]
        public static extern int ImpresoPintarRectangulo(int IdConexion, [MarshalAs(UnmanagedType.LPStr)]string Key, double X1, double Y1, double X2, double Y2, int Color, short FillStyle, int EdgeColor, short Size);

        [DllImport("GestorGEO.dll", CharSet = CharSet.Auto)]
        public static extern int ImpresoPintarEntidad(int IdConexion, [MarshalAs(UnmanagedType.LPStr)]string Key, int IdEntity, int Color, short Style, int EdgeColor, short Size);

        [DllImport("GestorGEO.dll", CharSet = CharSet.Auto)]
        public static extern int ImpresoPintarTextoEntidad(int IdConexion, [MarshalAs(UnmanagedType.LPStr)]string Key, int IdEntity, [MarshalAs(UnmanagedType.LPStr)]string Font, [MarshalAs(UnmanagedType.LPStr)]string text,
                        int Color, double MMsSize, double OffsetFactor);

        [DllImport("GestorGEO.dll", CharSet = CharSet.Auto)]
        public static extern int ImpresoCalculaEscalaMinima(int IdConexion, [MarshalAs(UnmanagedType.LPStr)]string Key, double MinX, double MinY, double MaxX, double MaxY);

        [DllImport("GestorGEO.dll", CharSet = CharSet.Auto)]
        public static extern int ImpresoExtensionMapa(int IdConexion, [MarshalAs(UnmanagedType.LPStr)]string Key, ref double MinX, ref double MinY, ref double MaxX, ref double MaxY);

        [DllImport("GestorGEO.dll", CharSet = CharSet.Auto)]
        public static extern int ImpresoSetFactorResolucion(int IdConexion, double Factor);

        [DllImport("GestorGEO.dll", CharSet = CharSet.Auto)]
        public static extern int ImpresoSetTipoSalida(int IdConexion, short Type);

        [DllImport("GestorGEO.dll", CharSet = CharSet.Auto)]
        public static extern int ImpresoMuestraDialogoImpresora(int IdConexion, short bShow);

        [DllImport("GestorGEO.dll", CharSet = CharSet.Auto)]
        public static extern void MedirDistancias(int IdConexion, int IdMapView);

        [DllImport("GestorGEO.dll", CharSet = CharSet.Auto)]
        public static extern void MedirAreaPoligono(int IdConexion, int IdMapView);

        [DllImport("GestorGEO.dll", CharSet = CharSet.Auto)]
        public static extern void MedirAreaCirculo(int IdConexion, int IdMapView);

        [DllImport("GestorGEO.dll", CharSet = CharSet.Auto)]
        public static extern void MedirPerfil(int IdConexion, int IdMapView);

        [DllImport("GestorGEO.dll", CharSet = CharSet.Auto)]
        public static extern void BorrarMediciones(int IdConexion, int IdMapView);

        [DllImport("GestorGEO.dll", CharSet = CharSet.Auto)]
        public static extern void BorrarMediciones2(int IdConexion, int IdMapView, bool bDeleteAll);

        [DllImport("GestorGEO.dll", CharSet = CharSet.Auto)]
        public static extern void CopiarMedicionesPortaPapeles(int IdConexion, int IdMapView);

        [DllImport("GestorGEO.dll", CharSet = CharSet.Auto)]
        public static extern bool PuedoCalcularPerfil(int IdConexion, int IdMapView);

        [DllImport("GestorGEO.dll", CharSet = CharSet.Auto)]
        public static extern void EditarPreferencias(int IdConexion, int IdMapView, short bCanChangeView);

        [DllImport("GestorGEO.dll", CharSet = CharSet.Auto)]
        public static extern void ConfigurarMonitor();

        [DllImport("GestorGEO.dll", CharSet = CharSet.Auto)]
        public static extern int EmpezarEdicion(int IdConexion, int IdMapView, [MarshalAs(UnmanagedType.LPStr)]string Theme, int bAskGeocode);

        [DllImport("GestorGEO.dll", CharSet = CharSet.Auto)]
        public static extern int TerminarEdicion(int IdConexion, int IdMapView);

        [DllImport("GestorGEO.dll", CharSet = CharSet.Auto)]
        public static extern bool SalvarTrabajoEdicion(int IdConexion, int IdMapView);

        [DllImport("GestorGEO.dll", CharSet = CharSet.Auto)]
        public static extern void NotificaEdicion(int IdConexion, int IdMapView, bool Notify, bool Lock);

        [DllImport("GestorGEO.dll", CharSet = CharSet.Auto)]
        public static extern void SetTipoTopologia(int IdConexion, int IdMapView, short Type);

        [DllImport("GestorGEO.dll", CharSet = CharSet.Auto)]
        public static extern int HacerUndoEdicion(int IdConexion, int IdMapView);

        [DllImport("GestorGEO.dll", CharSet = CharSet.Auto)]
        public static extern bool SetPedirGeocodigoEdicion(int IdConexion, int IdMapView, bool bAskGeocode);

        [DllImport("GestorGEO.dll", CharSet = CharSet.Auto)]
        public static extern int CrearEntidad(int IdConexion, int IdMapView,
                        [MarshalAs(UnmanagedType.LPStr)]string Geocode, bool TransacInc);

        [DllImport("GestorGEO.dll", CharSet = CharSet.Auto)]
        public static extern int CrearEntidadPorLocalizacion(int IdConexion, int IdMapView,
                        int IdgeOS_Location, [MarshalAs(UnmanagedType.LPStr)]string Geocode, bool TransacInc);

        [DllImport("GestorGEO.dll", CharSet = CharSet.Auto)]
        public static extern bool BorrarEntidad(int IdConexion, int IdMapView,
                                                int IdEntity, bool TransacInc);

        [DllImport("GestorGEO.dll", CharSet = CharSet.Auto)]
        public static extern bool EditarEntidad(int IdConexion, int IdMapView, int IdEntity);

        [DllImport("GestorGEO.dll", CharSet = CharSet.Auto)]
        public static extern bool RotarEntidad(int IdConexion, int IdMapView, int IdEntity, ref double Angle);

        [DllImport("GestorGEO.dll", CharSet = CharSet.Auto)]
        public static extern bool UnirEntidades(int IdConexion, int IdMapView,
                                int IdEntity1, int IdEntity2, bool bBorrarOriginales, bool TransacInc);

        [DllImport("GestorGEO.dll", CharSet = CharSet.Auto)]
        public static extern int RomperEntidadPorLinea(int IdConexion, int IdMapView,
                                int IdEntity, int IdLocLinea, bool DeleteOriginals, bool TransacInc);

        [DllImport("GestorGEO.dll", CharSet = CharSet.Auto)]
        public static extern int GetTemaEdicion(int IdConexion, int IdMapView,
                                [MarshalAs(UnmanagedType.LPStr)]StringBuilder EditTheme);

        [DllImport("GestorGEO.dll", CharSet = CharSet.Auto)]
        public static extern void MuestraLeyenda(int IdConexion, int IdMapView, bool bShow);

        [DllImport("GestorGEO.dll", CharSet = CharSet.Auto)]
        public static extern void SetVentanaLeyenda(int IdConexion, int IdMapView, IntPtr hWnd, int MinSize, int MaxSize, short bAdjust);

        [DllImport("GestorGEO.dll", CharSet = CharSet.Auto)]
        public static extern void SetVentanaVistas(int IdConexion, int IdMapView, IntPtr hWnd, int MinSize, int MaxSize, short bAdjust);

        [DllImport("GestorGEO.dll", CharSet = CharSet.Auto)]
        public static extern void SetVentanaInfoEntidad(int IdConexion, int IdMapView, short bShow);

        [DllImport("GestorGEO.dll", CharSet = CharSet.Auto)]
        public static extern void InfoEntidadMapa(int IdConexion, int IdMapView);

        [DllImport("GestorGEO.dll", CharSet = CharSet.Auto)]
        public static extern void InfoGeneralMapa(int IdConexion, int IdMapView);

        [DllImport("GestorGEO.dll", CharSet = CharSet.Auto)]
        public static extern void ConsultaCorreo(int IdConexion);

        [DllImport("GestorGEO.dll", CharSet = CharSet.Auto)]
        public static extern int CopiarMapa(int IdConexion, int IdMapView, short CopyStyle, [MarshalAs(UnmanagedType.LPStr)]string File, double Scale);

        [DllImport("GestorGEO.dll", CharSet = CharSet.Auto)]
        public static extern int DameLocalizacion(int IdConexion, int IdMapView, short LocType, int nColor, byte Style, double Size);

        [DllImport("GestorGEO.dll", CharSet = CharSet.Auto)]
        public static extern int CreaLocalizacion(int IdConexion, int IdMapView, short LocType, int Color, byte Style, double Size);

        [DllImport("GestorGEO.dll", CharSet = CharSet.Auto)]
        public static extern int CreaBuffer(int IdConexion, int IdMapView, [MarshalAs(UnmanagedType.LPStr)]string Theme, double Dist,
                            [MarshalAs(UnmanagedType.LPStr)]string Filter, int Color, byte Style, double Size);

        [DllImport("GestorGEO.dll", CharSet = CharSet.Auto)]
        public static extern int CreaBufferPorLocalizacion(int IdConexion, int IdMapView, int IdgeOS_Location,
                            double Dist, int Color, byte Style, double Size);

        [DllImport("GestorGEO.dll", CharSet = CharSet.Auto)]
        public static extern void AnadeCoordenadaLocalizacion(int IdConexion, int IdMapView, int IdgeOS_Location, double X, double Y);

        [DllImport("GestorGEO.dll", CharSet = CharSet.Auto)]
        public static extern void AnadeCoordenadaLocalizacionX(int IdConexion, int IdMapView, int IdgeOS_Location, double X, double Y, bool bAddPart);

        [DllImport("GestorGEO.dll", CharSet = CharSet.Auto)]
        public static extern void AnadeCoordenadaLocalizacion2(int IdConexion, int IdMapView, int IdgeOS_Location, double X, double Y, bool bAddPart, bool RefreshTLayer);

        [DllImport("GestorGEO.dll", CharSet = CharSet.Auto)]
        public static extern int EntidadALocalizacion(int IdConexion, int IdMapView, int IdEntity, int Color, byte Style, double Size);

        [DllImport("GestorGEO.dll", CharSet = CharSet.Auto)]
        public static extern int EntidadesALocalizacion(int IdConexion, int IdMapView, int IdEntities, int Color, byte Style, double Size);

        [DllImport("GestorGEO.dll", CharSet = CharSet.Auto)]
        public static extern short GetNumLocalizaciones(int IdConexion, int IdMapView);

        [DllImport("GestorGEO.dll", CharSet = CharSet.Auto)]
        public static extern int GetLocalizacion(int IdConexion, int IdMapView, short Index);

        [DllImport("GestorGEO.dll", CharSet = CharSet.Auto)]
        public static extern short GetTipoLocalizacion(int IdConexion, int IdMapView, int IdgeOS_Location);

        [DllImport("GestorGEO.dll", CharSet = CharSet.Auto)]
        public static extern void LiberaLocalizacion(int IdConexion, int IdMapView, int IdgeOS_Location);

        [DllImport("GestorGEO.dll", CharSet = CharSet.Auto)]
        public static extern void LiberaLocalizaciones(int IdConexion, int IdMapView);

        [DllImport("GestorGEO.dll", CharSet = CharSet.Auto)]
        public static extern int GetParamsLocalizacion(int IdConexion, int IdMapView, int IdgeOS_Location, ref int Color, ref byte Style, ref double Size);

        [DllImport("GestorGEO.dll", CharSet = CharSet.Auto)]
        public static extern int GetNumCoordenadasLocalizacion(int IdConexion, int IdMapView, int IdgeOS_Location, ref short NumCoords);

        [DllImport("GestorGEO.dll", CharSet = CharSet.Auto)]
        public static extern int GetCoordenadaLocalizacion(int IdConexion, int IdMapView, int IdgeOS_Location, int Coord, ref double X, ref double Y, ref short Part);

        [DllImport("GestorGEO.dll", CharSet = CharSet.Auto)]
        public static extern bool GetExtentLocalizacion(int IdConexion, int IdMapView, int IdgeOS_Location, ref double MinX, ref double MinY, ref double MaxX, ref double MaxY);

        [DllImport("GestorGEO.dll", CharSet = CharSet.Auto)]
        public static extern int FlashLocalizacion(int IdConexion, int IdMapView, int IdgeOS_Location, short Times);

        [DllImport("GestorGEO.dll", CharSet = CharSet.Auto)]
        public static extern int ZoomLocalizacion(int IdConexion, int IdMapView, int IdgeOS_Location);

        [DllImport("GestorGEO.dll", CharSet = CharSet.Auto)]
        public static extern double DistanciaLocalizacionTema(int IdConexion, int IdMapView, int IdgeOS_Location, [MarshalAs(UnmanagedType.LPStr)]string Theme);

        [DllImport("GestorGEO.dll", CharSet = CharSet.Auto)]
        public static extern int GetEntidadesMasCercanas(int IdConexion, int IdMapView, int IdgeOS_Location, short MaxEnts,
                            [MarshalAs(UnmanagedType.LPStr)]string Theme, [MarshalAs(UnmanagedType.LPStr)]string Filter,
                            [MarshalAs(UnmanagedType.LPArray)]byte[] IdEntities,
                            [MarshalAs(UnmanagedType.LPArray)]byte[] Distances);

        [DllImport("GestorGEO.dll", CharSet = CharSet.Auto)]
        public static extern int GetEntidadesMasCercanasE(int IdConexion, int IdMapView, int IdgeOS_Location, short MaxEnts,
                            [MarshalAs(UnmanagedType.LPStr)]string Theme, [MarshalAs(UnmanagedType.LPStr)]string Filter,
                            [MarshalAs(UnmanagedType.LPArray)]byte[] IdEntities,
                            [MarshalAs(UnmanagedType.LPArray)]byte[] Distances);

        [DllImport("GestorGEO.dll", CharSet = CharSet.Auto)]
        public static extern double DistanciaEntreEntidades(int IdConexion, int IdEntity1, int IdEntity2);

        [DllImport("GestorGEO.dll", CharSet = CharSet.Auto)]
        public static extern double DistanciaLocalizacionEntidad(int IdConexion, int IdMapView, int IdgeOS_Location, int IdEntity);

        [DllImport("GestorGEO.dll", CharSet = CharSet.Auto)]
        public static extern double DistanciaLocalizacionLocalizacion(int IdConexion, int IdMapView, int IdgeOS_Location1, int IdgeOS_Location2);

        [DllImport("GestorGEO.dll", CharSet = CharSet.Auto)]
        public static extern int SetErrorCargaTemas(int IdConexion, int IdMapView, bool ShowError);

        [DllImport("GestorGEO.dll", CharSet = CharSet.Auto)]
        public static extern int GetNumMdts(int IdConexion);

        [DllImport("GestorGEO.dll", CharSet = CharSet.Auto)]
        public static extern int GetMdt(int IdConexion, short Index, [MarshalAs(UnmanagedType.LPStr)]StringBuilder Code,
                            [MarshalAs(UnmanagedType.LPStr)]StringBuilder Descrip,
                            [MarshalAs(UnmanagedType.LPStr)]StringBuilder Tip,
                            [MarshalAs(UnmanagedType.LPStr)]StringBuilder Path);

        [DllImport("GestorGEO.dll", CharSet = CharSet.Auto)]
        public static extern int SetEtiquetaMdt(int IdConexion, int IdMapView, short Activate, [MarshalAs(UnmanagedType.LPStr)]string Label);

        [DllImport("GestorGEO.dll", CharSet = CharSet.Auto)]
        public static extern int IrCoordenadaEscala(int IdConexion, int IdMapView);

        [DllImport("GestorGEO.dll", CharSet = CharSet.Auto)]
        public static extern int SetRestriccionCoordenadas(int IdConexion, double MinX, double MinY, double MaxX, double MaxY);

        [DllImport("GestorGEO.dll", CharSet = CharSet.Auto)]
        public static extern int GetRestriccionCoordenadas(int IdConexion, ref double MinX, ref double MinY, ref double MaxX, ref double MaxY);


        [DllImport("GestorGEO.dll", CharSet = CharSet.Auto)]
        public static extern int FlashShape(int IdConexion, int IdMapView, IntPtr pShape, short nTimes);


        [DllImport("GestorGEO.dll", CharSet = CharSet.Auto)]
        public static extern int GetDatosUsuario(int IdConexion, [MarshalAs(UnmanagedType.LPStr)]string sCodigoUsuario,
                        [MarshalAs(UnmanagedType.LPStr)]StringBuilder Name,
                        [MarshalAs(UnmanagedType.LPStr)]StringBuilder Descrip,
                        [MarshalAs(UnmanagedType.LPStr)]StringBuilder Group, ref short ReadOnlyUser);

        [DllImport("GestorGEO.dll", CharSet = CharSet.Auto)]
        public static extern int EjecutaFuncionEntidad(int IdConexion, int IdMapView,
                        [MarshalAs(UnmanagedType.LPStr)]string sTema, [MarshalAs(UnmanagedType.LPStr)]string Dll,
                        [MarshalAs(UnmanagedType.LPStr)]string Function);

        [DllImport("GestorGEO.dll", CharSet = CharSet.Auto)]
        public static extern bool SetupTemaUsuarioFromTemaMapa(int IdConexion, int IdMapView, [MarshalAs(UnmanagedType.LPStr)]string sCodigoTema, [MarshalAs(UnmanagedType.LPStr)]string sCodigoTemaUsuario);


        [DllImport("GestorGEO.dll", CharSet = CharSet.Auto)]
        public static extern bool CargarCapaUsuario(int IdConexion, int IdMapView, int IdCapa, [MarshalAs(UnmanagedType.LPStr)]string sDbCapaUsuarios);

        [DllImport("GestorGEO.dll", CharSet = CharSet.Auto)]
        public static extern bool ModificarCapaUsuario(int IdConexion, int IdMapView, int IdCapa, [MarshalAs(UnmanagedType.LPStr)]string sDbCapaUsuarios);

        [DllImport("GestorGEO.dll", CharSet = CharSet.Auto)]
        public static extern bool EliminarCapaUsuario(int IdConexion, int IdMapView, int IdCapa, [MarshalAs(UnmanagedType.LPStr)]string sDbCapaUsuarios);

        [DllImport("GestorGEO.dll", CharSet = CharSet.Auto)]
        public static extern bool SetUsuarioCapaUsuario(int IdConexion, int IdMapView, [MarshalAs(UnmanagedType.LPStr)]string sUsuario);

        [DllImport("GestorGEO.dll", CharSet = CharSet.Auto)]
        public static extern bool GetUsuarioCapaUsuario(int IdConexion, int IdMapView, [MarshalAs(UnmanagedType.LPStr)]string sUsuario);

        [DllImport("GestorGEO.dll", CharSet = CharSet.Auto)]
        public static extern bool CapaUsuarioEnMapa(int IdConexion, int IdMapView, [MarshalAs(UnmanagedType.LPStr)]string sCodigoTema);


        [DllImport("GestorGEO.dll", CharSet = CharSet.Auto)]
        public static extern int DameNumViewsTema(int IdConexion, int IdMapView, [MarshalAs(UnmanagedType.LPStr)]string Theme);

        [DllImport("GestorGEO.dll", CharSet = CharSet.Auto)]
        public static extern bool SetFiltroTema(int IdConexion, int IdMapView, [MarshalAs(UnmanagedType.LPStr)]string Theme, [MarshalAs(UnmanagedType.LPStr)]string Filter);


        [DllImport("GestorGEO.dll", CharSet = CharSet.Auto)]
        public static extern bool SetEscalaReferencia(int IdConexion, int IdMapView, int Scale);

        [DllImport("GestorGEO.dll", CharSet = CharSet.Auto)]
        public static extern bool SetEnabledMapa(int IdConexion, int IdMapView, bool bEnabled);

        [DllImport("GestorGEO.dll", CharSet = CharSet.Auto)]
        public static extern bool SetEscogerImpresoPerfil(int IdConexion, bool bChoose);

        [DllImport("GestorGEO.dll", CharSet = CharSet.Auto)]
        public static extern bool AnadeDirsImpresosPerfil(int IdConexion, [MarshalAs(UnmanagedType.LPStr)]string sDir);


        [DllImport("GestorGEO.dll", CharSet = CharSet.Auto)]
        public static extern bool SetRotacionMapa(int IdConexion, int IdMapView, double Angle);

        [DllImport("GestorGEO.dll", CharSet = CharSet.Auto)]
        public static extern bool GetRotacionMapa(int IdConexion, int IdMapView, ref double Angle);

        [DllImport("GestorGEO.dll", CharSet = CharSet.Auto)]
        public static extern bool SetSistemaProyeccion(int IdConexion, int IdMapView, int Projection, int Geosystem);


        [DllImport("GestorGEO.dll", CharSet = CharSet.Auto)]
        public static extern bool GetSistemaProyeccion(int IdConexion, int IdMapView, ref int Projection, ref int Geosystem);


        [DllImport("GestorGEO.dll", CharSet = CharSet.Auto)]
        public static extern bool SetDibujaShapeTrabajo(int IdConexion, int IdMapView, bool bPoints, bool bLabels);

        [DllImport("GestorGEO.dll", CharSet = CharSet.Auto)]
        public static extern bool GetDibujaShapeTrabajo(int IdConexion, int IdMapView, ref bool bPoints, ref bool bLabels);

        [DllImport("GestorGEO.dll", CharSet = CharSet.Auto)]
        public static extern bool GetPosicionCursorMapa(int IdConexion, int IdMapView, ref double X, ref double Y);

        [DllImport("GestorGEO.dll", CharSet = CharSet.Auto)]
        public static extern int DuplicaMapa(int IdConexion, int IdMapViewIdMap, IntPtr hWnd);

        /// CONSTRUCCIONES SDO
        [DllImport("GestorGEO.dll", CharSet = CharSet.Auto)]
        public static extern bool GetSDOGeometriaLocalizacion(int IdConexion, int IdMapView, int IdgeOS_Location,
                        [MarshalAs(UnmanagedType.LPStr)]string Schema,
                        [MarshalAs(UnmanagedType.LPStr)]StringBuilder Value, int MaxSize);

        [DllImport("GestorGEO.dll", CharSet = CharSet.Auto)]
        public static extern bool GetSDOGeometriaEntidad(int IdConexion, int IsEntity,
                        [MarshalAs(UnmanagedType.LPStr)]string Schema,
                        [MarshalAs(UnmanagedType.LPStr)]StringBuilder Value, int MaxSize);


        [DllImport("GestorGEO.dll", CharSet = CharSet.Auto)]
        public static extern int GetPathTema([MarshalAs(UnmanagedType.LPStr)]string sETG, [MarshalAs(UnmanagedType.LPStr)]string sNombreTema, [MarshalAs(UnmanagedType.LPStr)]string sPath);

        [DllImport("GestorGEO.dll", CharSet = CharSet.Auto)]
        public static extern bool CreaConsultaX(int IdConexion, int IdMapView, [MarshalAs(UnmanagedType.LPStr)]string sNombreTema, [MarshalAs(UnmanagedType.LPStr)]string sRelate,
                                                [MarshalAs(UnmanagedType.LPStr)]string sTabla, [MarshalAs(UnmanagedType.LPStr)]string sCampoRelate);

        [DllImport("GestorGEO.dll", CharSet = CharSet.Auto)]
        public static extern bool CreaTematicoIntervalos(int IdConexion, int IdMapView, [MarshalAs(UnmanagedType.LPStr)]string sNombreTema, [MarshalAs(UnmanagedType.LPStr)]string sNombreTemaIntervalo,
                                          [MarshalAs(UnmanagedType.LPStr)]string sDescripcionTemaIntervalo, [MarshalAs(UnmanagedType.LPStr)]string sRelate, [MarshalAs(UnmanagedType.LPStr)]string sTabla, [MarshalAs(UnmanagedType.LPStr)]string sCampoRelate,
                                        [MarshalAs(UnmanagedType.LPStr)]string sCampoIntervalo, int nNumIntervalos, int nColorInicial, int nColorFinal);

        [DllImport("GestorGEO.dll", CharSet = CharSet.Auto)]
        public static extern bool CreaTematicoIntervalosX(int IdConexion, int IdMapView, [MarshalAs(UnmanagedType.LPStr)]string sNombreTema,
                                        [MarshalAs(UnmanagedType.LPStr)]string sRelate, [MarshalAs(UnmanagedType.LPStr)]string sTabla, [MarshalAs(UnmanagedType.LPStr)]string sCampoRelate, [MarshalAs(UnmanagedType.LPStr)]string sCampoIntervalo);

        [DllImport("GestorGEO.dll", CharSet = CharSet.Auto)]
        public static extern bool CreaTematicoValoresX(int IdConexion, int IdMapView, [MarshalAs(UnmanagedType.LPStr)]string sNombreTema,
                                        [MarshalAs(UnmanagedType.LPStr)]string sRelate, [MarshalAs(UnmanagedType.LPStr)]string sTabla, [MarshalAs(UnmanagedType.LPStr)]string sCampoRelate, [MarshalAs(UnmanagedType.LPStr)]string sCampoValores);

        [DllImport("GestorGEO.dll", CharSet = CharSet.Auto)]
        public static extern bool CreaTematicoEtiquetas(int IdConexion, int IdMapView, [MarshalAs(UnmanagedType.LPStr)]string sNombreTema, [MarshalAs(UnmanagedType.LPStr)]string sNombreTemaEtiquetas,
                                        [MarshalAs(UnmanagedType.LPStr)]string sDescripcionTemaEtiquetas, [MarshalAs(UnmanagedType.LPStr)]string sRelate, [MarshalAs(UnmanagedType.LPStr)]string sTabla, [MarshalAs(UnmanagedType.LPStr)]string sCampoRelate,
                                        [MarshalAs(UnmanagedType.LPStr)]string sCampoEtiqueta, [MarshalAs(UnmanagedType.LPStr)]string sFont, int nFontColor, int nFontSize, int nFontStyle);

        [DllImport("GestorGEO.dll", CharSet = CharSet.Auto)]
        public static extern void SetVentanaGeograficas(int IdConexion, int IdMapView, IntPtr hWnd);


        [DllImport("GestorGEO.dll", CharSet = CharSet.Auto)]
        public static extern int CreaLocalizacionX(int IdConexion, int IdMapView, short nTipoLoc,
                                                int nColor, short nEstilo, double fSize, [MarshalAs(UnmanagedType.LPStr)]string sFont, short Character, bool bOutline, int nColorOutline, double fAngRotation);

        [DllImport("GestorGEO.dll", CharSet = CharSet.Auto)]
        public static extern int CambiarSimbologiaLocalizacion(int IdConexion, int IdMapView, int IdgeOS_Location, int nColor, short nEstilo, double fSize, [MarshalAs(UnmanagedType.LPStr)]string sFont, short Character, bool bOutline, int nColorOutline, double fAngRotation);


        [DllImport("GestorGEO.dll", CharSet = CharSet.Auto)]
        public static extern void GetFullExtentMapa(int IdConexion, int IdMapView, ref double MinX, ref double MinY, ref double MaxX, ref double MaxY);

        [DllImport("GestorGEO.dll", CharSet = CharSet.Auto)]
        public static extern void RefreshTrackingLayer(int IdConexion, int IdMapView);


        [DllImport("GestorGEO.dll", CharSet = CharSet.Auto)]
        public static extern void SetIconosPequenos(bool bLittle);


        [DllImport("GestorGEO.dll", CharSet = CharSet.Auto)]
        public static extern bool RelacionEntidades(int IdConexion, int IdMapView, int IdLocation);


        [DllImport("GestorGEO.dll", CharSet = CharSet.Auto)]
        public static extern int AbreConexionX([MarshalAs(UnmanagedType.LPStr)]string ETG,
                        [MarshalAs(UnmanagedType.LPStr)]string User, [MarshalAs(UnmanagedType.LPStr)]string Password,
                        bool bReadMail);

        [DllImport("GestorGEO.dll", CharSet = CharSet.Auto)]
        public static extern bool ProyectarUtms2(int IdConexion, int IdMapView, double X, double Y, int nGeosistema, int nProyeccion, ref double XP, ref double YP);
    }
}
