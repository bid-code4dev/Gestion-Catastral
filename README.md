## Plataforma de Gestión Catastral Multipaís
---
La Plataforma de Gestión Catastral multipaís está dirigida a aquellas entidades públicas que quieran hacer uso extenso y eficiente de la información catastral en las múltiples áreas involucradas en una administración. 

Esta permite el tratamiento, consulta y actualización continua de la información de mapas del territorio, información física y jurídica de inmuebles y propietarios, y de información necesaria para la gestión de cobros y contribuyentes. Como elemento diferenciador:
1. el sistema contiene un módulo que le permite adaptarse a la realidad de los modelos de datos catastrales básicos de cada país; 
2. un sistema de carga de datos que facilita la puesta en marcha de la plataforma de manera autónoma; y 
3. se basa en el modelo de datos catastral LAMD (ISO 19152).
---

Este repositorio contiene las clases de .NET creadas para el [software de Gestión Catastral](http://www.galileoiys.es/portfolio-item/gestion-catastral/) de Galileo IyS. Esas clases permiten el tratamiento, consulta y actualización continua de: 
* la información de mapas del territorio,
* la información física y jurídica de inmuebles y propietarios, y 
* la información necesaria para la gestión de cobros y contribuyentes.   

**Estas funcionalidades permiten a la administración gestionar una de sus actividades más importantes para mantener el equilibrio y sustentabilidad económica: La gestión de la riqueza territorial.**

### Visión
El objetivo de Galileo es ofrecer una plataforma de código abierto con las funcionalidades catastrales básicas y trabajar para compartir una plataforma inicial y extenderla para dotarla de mecanismos más extensos aplicados al contexto de información catastral. El código que se libera inicialmente es la plataforma básica a partir de la cual construir otras herramientas que permitan extender el alcance y poder extender la influencia y la eficacia de un sistema de gestión catastral.

Los procesos básicos implementados en la herramienta son:

 * **Procedimiento de carga de información:** Existirá un procedimiento de carga inicial de información en el sistema que ofrezca la posibilidad de poner en marcha la plataforma de forma autónoma.

 * **Herramientas de consulta de datos prediales:** En cuanto a herramientas de consulta se ofrecerá la posibilidad de disponer de un formulario de extracción de datos catastrales según filtros definibles por el usuario por diferentes atributos de los predios, como cédula de identificación y/o nombre de los titulares, código de identificación predial y/o localización administrativa, etc. (información básica según modelo LADM).

 * **Ventana de Mapa (Visor):** La solución dispondrá de una ventana de mapa desde la que los usuarios puedan acceder a la información geográfica catastral, o de otro tipo, que se desee integrar (tanto ráster como vectorial).

 * **Herramientas de explotación:** Consideramos de particular interés la implementación de herramientas de tematización de datos prediales sobre la ventana de mapa, de modo que el usuario pueda seleccionar un atributo del repositorio de datos asociados a los predios y construir, de forma dinámica, un mapa temático por valores o por rangos, si el atributo es numérico, que sea proyectado en el mapa.

 * **Extensibilidad de la plataforma mediante Plugins:** La arquitectura permite la extensibilidad de la plataforma mediante la creación e integración con plugins específicos.

Puede ver enmás detalle el alcance de la plataforma tecnológica de catastro de Galileo en los siguientes enlaces y video:

 * [**SISTEMA DE GESTIÓN INTEGRAL CATASTRAL DE GALILEO:**](https://youtu.be/ovHF9xIQQAw?t=31)
 * [**CATASTRO MULTIPROPÓSITO IMPLANTADO EN TULUÁ (COLOMBIA):**](https://youtu.be/DFPdrn-bul8)
 * [**DESARROLLO TERRITORIAL Y URBANÍSTICO:**](https://youtu.be/DqZQLUX-ivk)
 * [**PLANES ORDENACIÓN TERRITORIAL:**](https://youtu.be/I5aZzfXbq-s?t=252)

[![IMAGE ALT TEXT HERE](https://user-images.githubusercontent.com/36766747/36648677-87fca8b0-1a64-11e8-8c02-33307fbc833f.png)](https://youtu.be/ovHF9xIQQAw?t=31)

### Componente geoEngine
---

El componente transversal **geoEngine** encapsula el acceso a las funcionalidades GIS de la Plataforma de Catastro. 
Históricamente la inclusión de herramientas geográficas en los desarrollos implicaba, por parte de los desarrolladores, un conocimiento profundo de los Sistemas de Información Geográficos. Además la integración de *'ventanas de mapa'* en aplicaciones de gestión era complicada. Este componente facilita la inclusión de este tipo de herramientas geográficas en todo tipo de proyectos de software.

#### Guía de Usuario
El componente funciona como un ensamblado que puede ser integrado en proyectos de Visual Studio y que da acceso a una serie de clases funcionales que dan acceso a un repositorio de herramientas geográficas como visualizar un mapa, medidas, edición, consultas, etc.

Para desarrollar una nueva aplicación utilizando este respositorio de clases es recomendable utilizar el IDE de Visual Studio en su versión 2015 o superior y .NET Framework 4.0 o superior, siguiendo los siguientes pasos:

El componente **geoEngineOS.dll** deberá ser incluido como una referencia (ensamblado externo) en el proyecto de VisualStudio. A partir de este momento se tendrá acceso a las siguientes clases funcionales:
+ `geOS_EtgConnection`: Conexión a un Espacio de Trabajo Geográfico (etg).
+ `geOS_MapWindow`: Ventana de Mapa en la que se desee proyectar la información geográfica.
+ `geOS_MapEntity`: Entidad obtenida del Mapa mediante selección o búsqueda por geocódigo.
+ `geOS_MapEntities`: Conjunto de entidades obtenida del Mapa.
+ `geOS_Location`: Localización obtenida del Mapa mediante digitalización o copia desde una entidad.
+ `geOS_Gestor`: Encapsulación de los métodos ofrecidos por el API de soporte GestorGEO.dll.

El API de soporte _GestorGEO.dll_ (disponible en directorio bin de la solución) deberá ser accesible al entorno de desarrollo. Por último deberá tener instalado un cliente _ESRI MapObjects 2.3_.

#### Dependencias y requisitos técnicos. Como instalar
Este componente trabaja en versiones de Windows XP, Vista, 7, 8 y 10 en las distintas distribuciones. El único requisito que debe cumplir es, tal y como se describió en la guía de usuario, es disponer del .NET Framework 4.0 o superior.

Respecto a las dependencias necesarias en la fase de despliegue tenemos:

+ El ensamblado **geoEngineOS.dll**, que deberá estar ubicado en el directorio de despliegue o bien referenciado en el fichero .config de la solución mediante la directiva assemblyBinding.

+ El API de soporte **GestorGEO.dll**, deberá estar ubicado en el directorio de despliegue o bien ser accesible mediante la variable de entorno PATH.

+ Un cliente **ESRI MapObjects 2.3**.

### Cómo contribuir
---
Si quieres contribuir al desarrollo de nuevas clases, añadir funcionalidades o hacer una aplicación adaptada a las necesidades de tu administración, puedes contactarno a través del email galileo@galileoiys.es.

**Consulta más información sobre nosotros en: http://www.galileoiys.es/**

### Colaboradores
---
Este software ha sido desarrollado gracias a la colaboración de expertos en Gestión Catastral y Representación de Mapas y Motores Geográficos durante más de 30 años por los técnicos de Galileo y numerosos clientes.

### Licencia 
---
[Apache v2](https://github.com/GalileoIyS/Gestion-Catastral/blob/master/LICENSE)

## Sobre Galileo Ingeniera y Servicios 
---

**Galileo Ingeniería y Servicios S.L.** es una empresa tecnológica perteneciente al grupo Maggioli que tiene 30 años de experiencia en consultoría económica y tecnológica para administraciones públicas, especializándose en el desarrollo y modernización de procesos de gestión.

 > en Galileo trabajamos para acompañar y ayudar en la mejora de las administraciones públicas gracias su tecnologías y servicios

Galileo mantiene su foco en:

 > la **modernización de los Procesos de Gestión** de las Administraciones Públicas

 > el **diseño, desarrollo e implantación de Sistemas de Gestión** para las Administraciones Públicas

 > la **implantación de Procesos de Formación y Capacitación** en las Administraciones Públicas para favorecer la autogestión descentralización

Galileo dispone de **[más de 25 productos  y servicios](http://www.galileoiys.es/productos-3/)** que abarcan todo el espectro de necesidades de administraciones públicas en sistemas de gestión municipal, económica y contable, gestión de población y territorial. Durante estos años, Galileo ha dedicado gran parte de sus beneficios a I+D+i con el fin de lograr una mayor evolución en sus productos que y ampliar la satisfacción de sus clientes, logrando ser nombrada **Pyme Innovadora** según la **Dirección General de Innovación y Competitividad, Ministerio de Economía y Competitividad**

Más información de las [entidades con las que hemos colaborado](https://github.com/GalileoIyS/Sistema-de-Informacion-Economica/blob/master/Referencias.md).

Lista con [Referencias y demos de los proyectos de Galileo](https://github.com/GalileoIyS/Sistema-de-Informacion-Economica/blob/master/ReferenciasDemos.md)

*[Noticia sobre Galileo IyS y código abierto](http://www.galileoiys.es/por-que-la-gestion-del-territorio-y-el-acceso-a-la-informacion-son-importantes-para-galileo/)*

