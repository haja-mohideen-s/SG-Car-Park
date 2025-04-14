# SG Car Park
[SG Car Park](https://sgcarpark.azurewebsites.net/) is a simple web app used to find the available parking slots in and around Singapore. 

![SG Car Park](https://repository-images.githubusercontent.com/470565866/0fc45675-255b-494d-abeb-10b9193e2a32)

## Dependencies

To run this project locally, ensure you have the following dependencies installed:

1. **Docker**
   - [Download and install Docker](https://www.docker.com/products/docker-desktop)
   - Verify installation by running `docker --version` in your terminal.

2. **.NET SDK** (optional, for running outside docker)
   - [Download and install .NET SDK 8.0](https://dotnet.microsoft.com/download/dotnet/8.0)
   - Verify installation by running `dotnet --version` in your terminal.

3. **Git** (optional, for cloning the repository)
   - [Download and install Git](https://git-scm.com/)
   - Verify installation by running `git --version` in your terminal.

## Steps to Run Locally

Follow these steps to run the project locally:

1. Clone the repository (if not already cloned):
   ```bash
   git clone https://github.com/your-repo/CarParkSG.git
   cd CarParkSG
   ```

2. Navigate to the `docker` directory:
   ```bash
   cd docker
   ```

3. Run the build Docker shell script to build the Docker image:
   ```bash
   ./build_docker.sh <tag>
   ```
   Replace `<tag>` with the desired tag for the Docker image (e.g., `0.1`).

4. Run the Docker container:
   ```bash
   docker run -p 1234:8080 sg-car-park:<tag> -n carpark
   ```
   Replace `<tag>` with the tag you used in the previous step.

5. Open your browser and navigate to `http://localhost:1234` to access the application.

## Features

1. Car Parks tracker - Show how many parking lots are available near current location/any location
2. Location based checking - Search for a location and see all parking lots around that location
3. Filter - Search for car parks based on type, basement, night parking & free   
4. Directions - Get directions to selected car park based on google maps api

## Attribution
1. [OneMap](https://www.onemap.gov.sg/main/v2/) - Mapping provider - Read their license [here](https://www.onemap.gov.sg/legal/opendatalicence.html)
2. [Leaflet](https://leafletjs.com/) - Mobile friendly interactive mapping library.
3. [JSONata](https://jsonata.org/) - JSON query and transformation language.
4. [Leaflet.markercluster](https://github.com/Leaflet/Leaflet.markercluster) - Provides beautiful animated marker clustering functionality for Leaflet.
5. [Leaflet.awesome-markers](https://github.com/lennardv2/Leaflet.awesome-markers) - Colorful markers for Leaflet.
6. [Leaflet.Geosearch](https://smeijer.github.io/leaflet-geosearch/) - A geocoding library for Leaflet.

## License
MIT License

Copyright (c) [2022] [Haja Mohideen]

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.