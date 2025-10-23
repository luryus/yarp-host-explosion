# YARP + MapStaticAssets Startup Slowness

This project demonstrates a weird behaviour (bug?) with YARP + MapStaticAssets

1. Run with `dotnet run`
2. Make a request: `curl http://localhost:5169/css/1.css`
3. **Observe the first request taking a while, and memory usage of the service rising very high.**