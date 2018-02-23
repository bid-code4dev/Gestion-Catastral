using System;
using System.Runtime.InteropServices;
using System.Text;
using System.Drawing;

namespace geoEngineOS
{
    /// <summary>
    /// MENSAJES ENVIADOS POR EL GEOMOTOR...
    /// </summary>
    public enum geOS_Message
    {
        msgBeforeMapDraw    = 0x4200,   // ENVIADO ANTES DE PINTAR EL MAPA  
        msgAfterMapDraw     = 0x4201,   // ENVIADO DESPUES DE QUE EL MAPA HAYA SIDO PINTADO
        msgMapClick         = 0x4202,   // ENVIADO CUANDO EL USUARIO HACE CLICK EN LA VENTANA DE MAPA
        msgCanceledMapDraw  = 0x4203,   // ENVIADO CUANDO EL USUARIO CANCELA EL REDIBUJADO DEL MAPA CON <ESCAPE>
        msgInfoEntity       = 0x4204,   // ENVIADO CUANDO EL USUARIO SELECCIONA UNA ENTIDAD EN EL MAPA
        msgOpenHyperlink    = 0x4205,   // ENVIADO CUANDO EL USUARIO SOLICITA ABRIR UN HYPERLINK ASOCIADO A UNA ENTIDAD
        msgChangeView       = 0x4206,   // ENVIADO CUANDO EL USUARIO CAMBIA LA VISTA DE MAPA
        msgPrintMGI         = 0x4207,   // ENVIADO CUANDO SE VA A IMPRIMIR UN IMPRESO DEL GEOMOTOR (PLANTILLA MGI)
        
        // MENSAJES RELACIONADOS CON LA EDICIÓN (ENVIADOS SI EL USUARIO LO SOLICITA)
        msgNewEntity        = 0x4220,   // ENVIADO CUANDO EL USUARIO CREA UNA NUEVA ENTIDAD
        msgDeleteEntity     = 0x4221,   // ENVIADO CUANDO EL USUARIO BORRA NUEVA ENTIDAD
        msgModifyEntity     = 0x4222,   // ENVIADO SI EL USUARIO HA MODIFICADO ENTIDAD
        msgGetGeocode       = 0x4223,   // ENVIADO PARA NOTIFICAR EL GEOCODIGO AL GEOMOTOR
        msgUndo             = 0x4224,   // ENVIADO CUANDO EL USUARIO HACE UNDO EN LA EDICION
        msgEditionInit      = 0x4225,   // ENVIADO CUANDO SE INICIA LA EDICIÓN
        msgEditionFinished  = 0x4226,   // ENVIADO CUANDO SE HA TERMINADO LA EDICIÓN
        msgEditionSaved     = 0x4227   // ENVIADO CUANDO LA EDICIÓN HA SIDO SALVADA
    }

    /// <summary>
    /// TOOLBARS DEL MAPA...
    /// </summary>
    public enum geOS_MapToolbar
    {
        Screen        = 1,        // TOOLBAR CON LAS HERRAMIENTAS DE PANTALLA (ZOOM, PANNING,...)
        Measures         = 2,     // TOOLBAR DE MEDICIONES (AREA, LONGITUD,...)
        Edition         = 3       // TOOLBAR CON LAS HERRAMIENTAS DE LA EDICIÓN
    }

    /// <summary>
    /// TIPOS DE ENTIDADES
    /// </summary>
    public enum geOS_EntityType
    {
        moPoint = 21,
        moLine = 22,
        moPolygon = 23,
        moRectangle = 25,
        moEllipse = 26
    }

    /// <summary>
    /// TIPOS DE RELLENOS DE POLÍGONO
    /// </summary>
    public enum geOS_FillType
    {
        moSolidFill = 0,
        moTransparentFill = 1,
        moHorizontalFill = 2,
        moVerticalFill = 3,
        moUpwardDiagonalFill = 4,
        moDownwardDiagonalFill = 5,
        moCrossFill = 6,
        moDiagonalCrossFill = 7,
        moLightGrayFill = 8,
        moGrayFill = 9,
        moDarkGrayFill = 10
    }

    /// <summary>
    /// ESTILOS DE ENTIDADES PUNTUALES
    /// </summary>
    public enum geOS_MarkerType
    {
        moCircleMarker = 0,
        moSquareMarker = 1,
        moTriangleMarker = 2,
        moCrossMarker = 3,
        moTrueTypeMarker = 4
    }

    /// <summary>
    /// ESTILOS DE ENTIDADES LINEALES
    /// </summary>
    public enum geOS_LineType
    {
        moSolidLine = 0,
        moDashLine = 1,
        moDotLine = 2,
        moDashdDotLine = 3,
        moDashdDotDotLine = 4
    }

    /// <summary>
    /// TIPOS DE SÍMBOLOS
    /// </summary>
    public enum geOS_SymbolType
    {
        moPointSymbol = 0,
        moLineSymbol = 1,
        moFillSymbol = 2
    }

    /// <summary>
    /// OPERADORES EN CONSULTAS ESPACIALES
    /// </summary>
    public enum geOS_SpatialOperator
    {
        moExtentOverlap = 0,
        moCommonPoInt32 = 1,
        moLineCross = 2,
        moCommonLine = 3,
        moCommonPointOrLineCross = 4,
        moEdgeTouchOrAreaIntersect = 5,
        moAreaIntersect = 6,
        moAreaIntersectNoEdgeTouch = 7,
        moContainedBy = 8,
        moContaining = 9,
        moContainedByNoEdgeTouch = 10,
        moContainingNoEdgeTouch = 11,
        moPointInPolygon = 12,
        moCentroidInPolygon = 13,
        moIdentical = 14
    }
}