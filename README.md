# Bruckel.Influx.Performance.Test

### Prueba de concepto de CRUDs de [InfluxDB](https://www.influxdata.com/) utilizando [InfluxDB.Client .NET](https://github.com/MikaelGRA/InfluxDB.Client).
* Utiliza una imagen de Docker de InfluxDB.
  - Recomiendo instalar [Docker Desktop](https://www.docker.com/products/docker-desktop)
  - Seguir los pasos del [blog de InluxDB](https://www.influxdata.com/blog/getting-started-with-c-and-influxdb/).
* Aplicación de consola .NET
  - Se puede depurar con Visual Studio 2022 o [Visual Studio Code](https://code.visualstudio.com/)
  - Crea una serie de datos temporales de granularidad horaria (un año) para 100 suministros. (El identificador es numérico, pero tambien hay lógica para crear CUPS aleatorios)
