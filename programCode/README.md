# Restaurant Reservierung

## Frontend
Zuerst den Entwicklungsserver starten:
`cd .\frontend\´
`cd .\app\´
`npm run dev
# oder
yarn dev
# oder
pnpm dev
# oder
bun dev´
Um mehr zu erfahren, wirf einen Blick auf die folgenden Ressourcen:

    React Dokumentation (https://react.dev/) Erfahre mehr über React

    TailwindCSS Dokumentation (https://tailwindcss.com/) Erfahre mehr über TailwindCSS

    Next.js Dokumentation (https://nextjs.org/docs) Erfahre mehr über die Funktionen und die API von Next.js

    Next.js lernen (https://nextjs.org/learn) Ein interaktives Tutorial zu Next.js

## Backend

### Datenbankkommunikation

Für die Kommunikation mit der Datenbank wird EF Core verwendet. Das Tool kann mit den folgenden Befehlen installiert werden:


`dotnet add package Microsoft.EntityFrameworkCore.Design --version 8.0.4`

`dotnet add package Pomelo.EntityFrameworkCore.MySql --version 8.0.0`

`dotnet tool install --global dotnet-ef`

Anschließend wird das Modell in der Anwendung von EF Core generiert, indem es die Struktur der Datenbank analysiert und daraus das Modell samt Beziehungen erstellt. Dies erfolgt mit dem folgenden Befehl:

`dotnet ef dbcontext scaffold "server=185.228.137.229;port=3306;database=RestaurantReservierung;user=***;password=***" Pomelo.EntityFrameworkCore.MySql --output-dir Models --context-dir Data --context AppDbContext --force`

Die Anmeldedaten stehen im Discord.
Wenn Änderungen an der Datenbank vorgenommen werden, wie zum Beispiel das Hinzufügen einer Spalte in einer Tabelle, muss das Modell neu generiert werden. Dafür wird ebenfalls der oben genannte Befehl verwendet.

### Metriken mit Prometheus

Prometheus dient der Erfassung von Metriken für die ASP.NET Core Web API. Die erfassten Metriken sind ausschließlich während der Laufzeit der Anwendung verfügbar. Nach dem Start der Anwendung können sie unter https://localhost:7038/metrics eingesehen werden.
