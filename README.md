## Gestión Catastral
---
El objetivo de Galileo es ofrecer una plataforma de software libre con funcionalidades catastrales básicas que pueda ser puesta en operación de forma rápida, y que permita adaptarse a la realidad de diferentes países en cuanto a sus distintas especificaciones de modelos de datos catastrales básicos o, por supuesto, a funcionalidades específicas.

Nuestra idea es trabajar para compartir una plataforma inicial y extenderla para dotarla de mecanismos más extensos aplicados al contexto de información catastral. El código que se presenta es la plataforma inicial a partir de la cual construir el producto final.

El Sistema de Gestión Catastral contiene las clases de .NET creadas para el (software de Gestión Catastral)[http://www.galileoiys.es/portfolio-item/gestion-catastral/] de Galileo IyS. Este software tiene como función la gestión y actualización de datos catastrales así como la emisión y cobro de recibos a los ciudadanos. Permite el tratamiento, consulta y actualización continua de: 
* la información de mapas del territorio,
* la información física y jurídica de immuebles y propietarios, y 
* la información necesaria para la gestión de cobros y contribuyentes.   

Estas funcionalidades permiten a la administración gestionar una de sus actividades más importantes para mantener el equilibrio y sustentabilidad económica: La gestión de la riqueza territorial.

Ver más información en **[este video](https://youtu.be/ovHF9xIQQAw?t=31)**.

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
[Apache v2](https://www.apache.org/licenses/LICENSE-2.0)

## Sobre Galileo Ingeniera y Servicios 
---

**Galileo Ingeniería y Servicios S.L.** es una empresa que lleva más de 30 años desarrollando su actividad en el campo de las Tecnologías de la Información y Comunicaciones (TIC) con una presencia destacada en Canarias, en la Península y enLatinoamérica.Desde 2016, Galileo está integrada en el Grupo Maggioli, grupo líder en innovación para administraciones públicas desde hace más de 100 años en Italia, que le aporta una mayor proyección de sus productos y experiencia a nivel internacional.

 > en Galileo trabajamos para acompañar y ayudar en la mejora de las administraciones públicas gracias su tecnologías y servicios


Galileo mantiene su foco en:

 > la **modernización de los Procesos de Gestión** de las Administraciones Públicas

 > el **diseño, desarrollo e implantación de Sistemas de Gestión** para las Administraciones Públicas

 > la **implantación de Procesos de Formación y Capacitación** en las Administraciones Públicas para favorecer la autogestión descentralización

Galileo dispone de productos y servicios que abarcan todo el espectro de necesidades de administraciones públicas en sistemas de gestión municipal, económica y contable, gestión de población y territorial. Durante estos años, Galileo ha dedicado gran parte de sus beneficios a I+D+i con el fin de lograr una mayor evolución en sus productos que y ampliar la satisfacción de sus clientes, logrando ser nombrada **Pyme Innovadora** según la **Dirección General de Innovación y Competitividad, Ministerio de Economía y Competitividad**
